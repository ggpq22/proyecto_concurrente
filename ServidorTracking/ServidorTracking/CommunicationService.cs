using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Mensajes;
using System.Net.Sockets;
using System.Threading;
using SistemaTrackingBiblioteca.Serializacion;
using ServidorTracking.DataBase;
using SistemaTrackingBiblioteca.Entidades;

namespace ServidorTracking
{
    class CommunicationService
    {
        MessageDelivery delivery;
        DBController dbcontrol;

        Thread events;
        
        // Events: Connect
        public delegate void CommunicationEventHandler(object sender, Mensaje mensaje);
        public event CommunicationEventHandler Connect;
        protected virtual void OnConnect(Mensaje e)
        {
            if (Connect != null)
                Connect(this, e);
        }
        // Events: Disconnect
        public event CommunicationEventHandler Disconnect;
        protected virtual void OnDisconnect(Mensaje e)
        {
            if (Disconnect != null)
                Disconnect(this, e);
        }

        // Events: LocationChanged
        public event CommunicationEventHandler LocationChanged;
        protected virtual void OnLocationChanged(Mensaje e)
        {
            if (LocationChanged != null)
                LocationChanged(this, e);
        }

        // Events: DBRequested
        public event CommunicationEventHandler DBRequested;
        protected virtual void OnDBRequested(Mensaje e)
        {
            if (DBRequested != null)
                DBRequested(this, e);
        }

        // Constructor
        public CommunicationService(NetworkStream stream, TcpClient client)
        {
            delivery = new MessageDelivery(stream, client);
            dbcontrol = new DBController("pbarco", "12345Pablo");
            try
            {
                delivery.OpenDelivery();

                events = new Thread(incomingMessage);
                events.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Thread Method
        private void incomingMessage()
        {
            string str = null;
            Mensaje message;

            while (true)
            {
                try
                {
                    str = delivery.RecieveMessage();
                }
                catch (Exception e)
                {
                    Console.WriteLine("== ERROR == -" + e.Message);
                }

                if (str != null)
                {
                    try
                    {
                        message = SerializarcionJson.Deserializar(str) as Mensaje;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    
                    MsgConexion menCon;
                    MsgLocalizacion menLoc;
                    MsgDBPeticion menDB;
                    Console.WriteLine(str);
                    if (message is MsgConexion)
                    {
                        menCon = message as MsgConexion;

                        if (menCon.Mensaje == "conectar")
                        {
                            OnConnect(menCon);
                        }
                        else if (menCon.Mensaje == "desconectar")
                        {
                            OnDisconnect(menCon);
                        }
                    }
                    else if (message is MsgLocalizacion)
                    {
                        menLoc = message as MsgLocalizacion;
                        Historial h = new Historial();

                        try
                        {
                            foreach (string to in menLoc.To)
                            {
                                h.Fecha = menLoc.Fecha;
                                h.Lat = Convert.ToDecimal(menLoc.Latitud.Replace('.', ','));
                                h.Long = Convert.ToDecimal(menLoc.Longitud.Replace('.', ','));
                                h.Cuenta = dbcontrol.GetCuentaByUsuario(menLoc.From);
                                h.Grupo = dbcontrol.GetGrupoByNombre(to);

                                dbcontrol.CreateHistorial(h);
                            }
                        }
                        catch (Exception e)
                        {
                            menLoc.IsValido = false;
                            menLoc.Errores.Add(e.Message);
                        }
                        finally
                        {
                            OnLocationChanged(menLoc);
                        }
                    }
                    else if (message is MsgDBPeticion)
                    {
                        menDB = message as MsgDBPeticion;

                        if(menDB.CodigoPeticion == "Login")
                        {
                            Cuenta c =  menDB.Params[0] as Cuenta;
                            Cuenta cres;
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                cres = dbcontrol.Login(c);

                                res.Return.Add(cres);
                            }
                            catch(Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "CrearCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            MsgDBRespuesta res = new MsgDBRespuesta();
                            Cuenta cres;
                            
                            try
                            {
                                cres = dbcontrol.CreateCuenta(c);

                                res.Return.Add(cres);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "BorrarCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                dbcontrol.DeleteCuenta(c.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "CrearGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            Grupo gres;
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                gres = dbcontrol.CreateGrupo(g);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.Add(g);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorAnfitrion")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            List<Grupo> g = new List<Grupo>();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                g = dbcontrol.GetGrupoByAnfitrion(c.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.AddRange(g);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorIntegrante")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            MsgDBRespuesta res = new MsgDBRespuesta();
                            List<Grupo> g = new List<Grupo>();

                            try
                            {
                                g = dbcontrol.GetGrupoByIntegrante(c.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.AddRange(g);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "AgregarCuentaAGrupo")
                        {
                            if (!menDB.Notificacion)
                            {
                                Cuenta c = menDB.Params[0] as Cuenta;
                                Grupo g = menDB.Params[1] as Grupo;
                                Grupo gr = new Grupo();
                                MsgDBRespuesta res = new MsgDBRespuesta();

                                try
                                {
                                    gr = dbcontrol.AddCuentaToGrupo(c.Id, g.Id);
                                }
                                catch (Exception e)
                                {
                                    res.IsValido = false;
                                    res.Errores.Add(e.Message);
                                }
                                finally
                                {
                                    res.From = menDB.From;
                                    res.To = menDB.To;
                                    res.Fecha = DateTime.Now;
                                    res.CodigoPeticion = menDB.CodigoPeticion;
                                    res.Return.Add(gr);

                                    OnDBRequested(res);
                                }
                            }
                            else
                            {
                                MsgNotificacion not = new MsgNotificacion();
                                not.Fecha = DateTime.Now;
                                not.From = menDB.From;
                                not.To = menDB.To;
                                not.Peticion = menDB;

                                OnDBRequested(not);
                            }
                        }
                        else if (menDB.CodigoPeticion == "BorrarCuentaDeGrupo")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            Grupo g = menDB.Params[1] as Grupo;
                            Grupo gr = new Grupo();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                gr = dbcontrol.DeleteCuentaFromGrupo(c.Id, g.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.Add(gr);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "BorrarGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                dbcontrol.DeleteGrupo(g.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetHistorialPorGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            List<Historial> hr = new List<Historial>();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                hr = dbcontrol.GetHistorialByGrupo(g.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.AddRange(hr);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetHistorialPorCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            List<Historial> hr = new List<Historial>();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                hr = dbcontrol.GetHistorialByCuenta(c.Id);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.AddRange(hr);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetCuentas")
                        {
                            List<Cuenta> lr = new List<Cuenta>();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                lr = dbcontrol.GetAllCuentas();
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.AddRange(lr);

                                OnDBRequested(res);
                            }
                        }
                        else
                        {
                            menDB.IsValido = false;
                            menDB.Errores.Add("Este codigo de peticion no es valido.");

                            OnDBRequested(menDB);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("llega null");
                }
            }
        }

        public void SendToClient(Mensaje message)
        {
            try
            {
                string str = SerializarcionJson.Serializar<Mensaje>(message);
                Console.WriteLine(str);
                delivery.SendMessage(str);
            }
            catch (Exception e)
            {
                Console.WriteLine("== ERROR == -" + e.Message);
            }
        }

        public void CloseCommunications()
        {
            try
            {
                delivery.CloseDelivery();
            }
            catch (Exception e)
            {
                Console.WriteLine("== ERROR == -" + e.Message);
            }
        }
    }
}
