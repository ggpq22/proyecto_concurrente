using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Serializacion;
using SistemaTrackingBiblioteca.Mensajes;

namespace ServidorTracking
{
    class ServerClient
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //TODO: Serializacion y logica para los tipos de mensaje

        TcpClient client;
        CommunicationService service;
        MessageRouter router;
        ConcurrentQueue<Mensaje> mensajes = new ConcurrentQueue<Mensaje>();

        Thread sending;

        // Event Methods
        void service_Connect(object sender, Mensaje message)
        {
            MsgConexion msn = message as MsgConexion;
            this.name = msn.From;
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

        public ServerClient(TcpClient client, MessageRouter router)
        {
            this.client = client;
            this.router = router;
            service = new CommunicationService(this.client.GetStream(), this.client);
	        
            // Subscribe Events
            service.Connect += service_Connect;
            service.Disconnect += service_Disconnect;
            service.LocationChanged += service_LocationChanged;

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
