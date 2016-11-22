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
        ServerClient client;

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
        public CommunicationService(NetworkStream stream, ServerClient client)
        {
            this.client = client;

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
            Mensaje message = null;

            while (client.Client.Connected)
            {
                try
                {
                    str = delivery.RecieveMessage();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (str != null)
                {
                    try
                    {
                        message = SerializarcionJson.Deserializar(str) as Mensaje;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    MsgConexion menCon;
                    MsgLocalizacion menLoc;
                    MsgDBPeticion menDB;

                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("RECIBIENDO: " + message.Tipo);

                        switch (message.Tipo)
                        {
                            case "MsgConexion":
                                Console.ForegroundColor = ConsoleColor.Green; break;
                            case "MsgLocalizacion":
                                Console.ForegroundColor = ConsoleColor.Blue; break;
                            case "MsgDBPeticion":
                                Console.ForegroundColor = ConsoleColor.Yellow; break;
                        }

                        Console.WriteLine(str);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

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
                        Historial h;

                        try
                        {
                            new Thread(() =>
                            {
                                MsgLocalizacion m = menLoc;
                                try
                                {
                                    foreach (string to in m.To)
                                    {
                                        h = new Historial();
                                        h.Fecha = m.Fecha;
                                        h.Lat = Convert.ToDecimal(m.Latitud.Replace('.', ','));
                                        h.Long = Convert.ToDecimal(m.Longitud.Replace('.', ','));
                                        h.Cuenta = dbcontrol.GetCuentaByUsuario(m.From);
                                        h.Grupo = dbcontrol.GetGrupoByNombre(to);

                                        dbcontrol.CreateHistorial(h);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }).Start();
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            OnLocationChanged(menLoc);
                        }
                    }
                    else if (message is MsgDBPeticion)
                    {
                        menDB = message as MsgDBPeticion;

                        if (menDB.CodigoPeticion == "Login")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
                            Cuenta cres;
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                cres = dbcontrol.Login(c);

                                res.ReturnCuenta.Add(cres);
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
                        else if (menDB.CodigoPeticion == "CrearCuenta")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
                            MsgDBRespuesta res = new MsgDBRespuesta();
                            Cuenta cres;

                            try
                            {
                                cres = dbcontrol.CreateCuenta(c);

                                res.ReturnCuenta.Add(cres);
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
                            Cuenta c = menDB.ParamsCuenta[0];
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                dbcontrol.DeleteCuenta(c.Usuario);
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
                            Grupo g = menDB.ParamsGrupo[0];
                            Grupo gres = new Grupo();
                            MsgDBRespuesta res = new MsgDBRespuesta();
                            MsgNotificacion not = new MsgNotificacion();

                            try
                            {
                                gres = dbcontrol.CreateGrupo(g);
                            }
                            catch (Exception e)
                            {
                                res.IsValido = false;
                                res.Errores.Add(e.Message);
                                not.IsValido = false;
                                not.Errores.Add(e.Message);
                            }
                            finally
                            {
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.ReturnGrupo.Add(g);

                                not.From = menDB.From;
                                not.Fecha = DateTime.Now;
                                not.Respuesta = res;
                                foreach (Cuenta c in gres.Integrantes)
                                {
                                    not.To.Add(c.Usuario);
                                }

                                OnDBRequested(res);
                                OnDBRequested(not);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorAnfitrion")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
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
                                res.ReturnGrupo.AddRange(g);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorIntegrante")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
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
                                res.ReturnGrupo.AddRange(g);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "AgregarCuentaAGrupo")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
                            Grupo g = menDB.ParamsGrupo[0];
                            Grupo gr = new Grupo();
                            MsgDBRespuesta res = new MsgDBRespuesta();
                            MsgNotificacion not = new MsgNotificacion();

                            try
                            {
                                gr = dbcontrol.AddCuentaToGrupo(c.Id, g.Id);

                                not.Fecha = DateTime.Now;
                                not.From = menDB.From;
                                not.To = menDB.To;
                                not.Respuesta = res;
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
                                res.ReturnGrupo.Add(gr);

                                not.Fecha = DateTime.Now;
                                not.From = menDB.From;
                                not.To = menDB.To;
                                not.Respuesta = res;

                                OnDBRequested(res);
                                OnDBRequested(not);
                            }
                        }
                        else if (menDB.CodigoPeticion == "BorrarCuentaDeGrupo")
                        {
                            Cuenta c = menDB.ParamsCuenta[0];
                            List<Grupo> gr = new List<Grupo>();
                            MsgDBRespuesta res = new MsgDBRespuesta();

                            try
                            {
                                foreach (Grupo g in menDB.ParamsGrupo)
                                {
                                    gr.Add(dbcontrol.DeleteCuentaFromGrupo(c.Id, g.Id));
                                }
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
                                res.ReturnGrupo.AddRange(gr);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "BorrarGrupo")
                        {
                            Grupo g = menDB.ParamsGrupo[0];
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
                            Grupo g = menDB.ParamsGrupo[0];
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
                                res.ReturnHistorial.AddRange(hr);

                                OnDBRequested(res);
                            }
                        }
                        else if (menDB.CodigoPeticion == "GetHistorialPorCuenta")
                        {
                            Cuenta c = menDB.ParamsCuenta[0] as Cuenta;
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
                                res.ReturnHistorial.AddRange(hr);

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
                                res.ReturnCuenta.AddRange(lr);

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
                    client.RemoveMe();
                }
            }
        }

        public void SendToClient(Mensaje message)
        {
            try
            {
                string str = SerializarcionJson.Serializar<Mensaje>(message);

                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("ENVIANDO: " + message.Tipo);

                    switch (message.Tipo)
                    {
                        case "MsgConexion":
                            Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                        case "MsgLocalizacion":
                            Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                        case "MsgDBRespuesta":
                            Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                    }

                    Console.WriteLine(str);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                delivery.SendMessage(str);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("== ERROR EN: " + this.GetType().Name + " == -" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
