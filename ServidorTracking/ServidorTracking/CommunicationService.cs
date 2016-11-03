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
        String latitud = null, longitud = null;
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
            delivery.OpenDelivery();

            events = new Thread(incomingMessage);
            events.Start();
        }

        // Thread Method
        private void incomingMessage()
        {
            string str;
            Mensaje message;

            while (true)
            {
                str = delivery.RecieveMessage();
                if (str != null)
                {
                    message = SerializarcionJson.Deserializar(str) as Mensaje;
                    // TODO: Descerializar a Mensaje, checkear el tipo y descerializar a el tipo que corresponde
                    MsgConexion menCon;
                    MsgLocalizacion menLoc;
                    MsgDBPeticion menDB;

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

                        foreach (string to in menLoc.To)
                        {
                            Historial h = new Historial();
                            h.Fecha = menLoc.Fecha;
                            h.Lat = Convert.ToDecimal(menLoc.Latitud.Replace('.', ','));
                            h.Long = Convert.ToDecimal(menLoc.Longitud.Replace('.', ','));
                            h.Cuenta = dbcontrol.GetCuentaByUsuario(menLoc.From);
                            h.Grupo = dbcontrol.GetGrupoByNombre(to);

                            dbcontrol.CreateHistorial(h);
                        }

                        OnLocationChanged(menLoc);
                    }
                    else if (message is MsgDBPeticion)
                    {
                        menDB = message as MsgDBPeticion;

                        if(menDB.CodigoPeticion == "Login")
                        {
                            Cuenta c =  menDB.Params[0] as Cuenta;
                            Cuenta cres = dbcontrol.Login(c);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.Add(cres);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "CrearCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            Cuenta cres = dbcontrol.CreateCuenta(c);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.Add(cres);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "BorrarCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            dbcontrol.DeleteCuenta(c.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "CrearGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            g = dbcontrol.CreateGrupo(g);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.Add(g);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorAnfitrion")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            List<Grupo> g = dbcontrol.GetGrupoByAnfitrion(c.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.AddRange(g);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "GetGrupoPorIntegrante")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            List<Grupo> g = dbcontrol.GetGrupoByIntegrante(c.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.AddRange(g);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "AgregarCuentaAGrupo")
                        {
                            if (!menDB.Notificacion)
                            {
                                Cuenta c = menDB.Params[0] as Cuenta;
                                Grupo g = menDB.Params[1] as Grupo;
                                Grupo gr = dbcontrol.AddCuentaToGrupo(c.Id, g.Id);

                                MsgDBRespuesta res = new MsgDBRespuesta();
                                res.From = menDB.From;
                                res.To = menDB.To;
                                res.Fecha = DateTime.Now;
                                res.CodigoPeticion = menDB.CodigoPeticion;
                                res.Return.Add(gr);

                                OnDBRequested(res);
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
                            Grupo gr = dbcontrol.DeleteCuentaFromGrupo(c.Id, g.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.Add(gr);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "BorrarGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            dbcontrol.DeleteGrupo(g.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "GetHistorialPorGrupo")
                        {
                            Grupo g = menDB.Params[0] as Grupo;
                            List<Historial> hr = dbcontrol.GetHistorialByGrupo(g.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.AddRange(hr);

                            OnDBRequested(res);
                        }
                        else if (menDB.CodigoPeticion == "GetHistorialPorCuenta")
                        {
                            Cuenta c = menDB.Params[0] as Cuenta;
                            List<Historial> hr = dbcontrol.GetHistorialByCuenta(c.Id);

                            MsgDBRespuesta res = new MsgDBRespuesta();
                            res.From = menDB.From;
                            res.To = menDB.To;
                            res.Fecha = DateTime.Now;
                            res.CodigoPeticion = menDB.CodigoPeticion;
                            res.Return.AddRange(hr);

                            OnDBRequested(res);
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
            string str = SerializarcionJson.Serializar<Mensaje>(message);
            Console.WriteLine(str);
            delivery.SendMessage(str);
        }

        public void CloseCommunications()
        {
            delivery.CloseDelivery();
        }
    }
}
