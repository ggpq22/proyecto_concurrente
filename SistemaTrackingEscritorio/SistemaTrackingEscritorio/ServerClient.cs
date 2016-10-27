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

        TcpClient client;
        CommunicationService service;
        ConcurrentQueue<Mensaje> mensajesSalida = new ConcurrentQueue<Mensaje>();

        Thread sending;

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

        // Event Methods
        void service_Connect(object sender, Mensaje message)
        {
            MsgConexion msn = message as MsgConexion;
            this.name = msn.From;
            OnConnect(msn);
        }
        void service_Disconnect(object sender, Mensaje message)
        {
            MsgConexion msn = message as MsgConexion;
            OnDisconnect(msn);
            CloseClient();
        }
        void service_LocationChanged(object sender, Mensaje message)
        {
            MsgLocalizacion msn = message as MsgLocalizacion;
            OnLocationChanged(msn);
        }

        public ServerClient(TcpClient client)
        {
            this.client = client;
            service = new CommunicationService(this.client);
	        
            // Subscribe Events
            service.Connect += service_Connect;
            service.Disconnect += service_Disconnect;
            service.LocationChanged += service_LocationChanged;

            sending = new Thread(sendMessages);
            sending.Start();
        }

        public void SendToServer(Mensaje message)
        {
            mensajesSalida.Enqueue(message);
        }

        public void CloseClient()
        {
            service.CloseCommunications();
            client.Close();
        }

        private void sendMessages()
        {
            while (true)
            {
                if (mensajesSalida.Count > 0)
                {
                    Mensaje m;

                    while (mensajesSalida.TryDequeue(out m))
                    {
                        service.SendToServer(m);
                    }
                }
            }
        }
    }
}
