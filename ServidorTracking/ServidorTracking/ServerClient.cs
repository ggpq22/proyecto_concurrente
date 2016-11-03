using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Serializacion;
using SistemaTrackingBiblioteca.Mensajes;
using ServidorTracking.DataBase;

namespace SistemaTrackingBiblioteca
{
    class ServerClient
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int getsLocations;

        public int GetsLocations
        {
            get { return getsLocations; }
            set { getsLocations = value; }
        }

        //TODO: Serializacion y logica para los tipos de mensaje

        TcpClient client;
        CommunicationService service;
        MessageRouter router;
        DBController dbCon;
        ConcurrentQueue<Mensaje> mensajes = new ConcurrentQueue<Mensaje>();

        Thread sending;

        // Event Methods
        void service_Connect(object sender, Mensaje message)
        {
            MsgConexion msn = message as MsgConexion;
            this.name = msn.From;
            this.getsLocations = dbCon.GetCuentaByUsuario(msn.From).RecibeLocalizacion;
            router.RouteMessage(msn);
        }
        void service_Disconnect(object sender, Mensaje message)
        {
            MsgConexion msn = message as MsgConexion;
            router.RouteMessage(msn);
            CloseClient();
            router.RemoveClient(this);
        }
        void service_LocationChanged(object sender, Mensaje message)
        {
            MsgLocalizacion msn = message as MsgLocalizacion;
            router.RouteMessage(msn);
        }
        void service_DBRecuested(object sender, Mensaje message)
        {
            if (message is MsgDBRespuesta)
            {
                MsgDBRespuesta msn = message as MsgDBRespuesta;

                if (msn.CodigoPeticion == "CrearGrupo" || msn.CodigoPeticion == "AgregarCuentaAGrupo" || msn.CodigoPeticion == "BorrarCuentaDeGrupo" || msn.CodigoPeticion == "BorrarGrupo")
                {
                    router.UpdateGrupos(dbCon.GetAllGrupos());
                }
            }
            router.RouteMessage(message);
        }

        public ServerClient(TcpClient client, MessageRouter router)
        {
            this.client = client;
            this.router = router;
            dbCon = new DBController("pbarco", "12345Pablo");
            service = new CommunicationService(this.client.GetStream(), this.client);
	        
            // Subscribe Events
            service.Connect += service_Connect;
            service.Disconnect += service_Disconnect;
            service.LocationChanged += service_LocationChanged;
            service.DBRequested += service_DBRecuested;

            sending = new Thread(sendMessages);
            sending.Start();
        }

        public void SendToClient(Mensaje message)
        {
            mensajes.Enqueue(message);
        }

        public void CloseClient()
        {
            client.Close();
            service.CloseCommunications();
        }

        private void sendMessages()
        {
            while (true)
            {
                if (mensajes.Count > 0)
                {
                    Mensaje m;

                    while (mensajes.TryDequeue(out m))
                    {
                        service.SendToClient(m);
                    }
                }
            }
        }
    }
}
