using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Mensajes;
using System.Net.Sockets;
using System.Threading;
using SistemaTrackingBiblioteca.Serializacion;

namespace ServidorTracking
{
    class CommunicationService
    {
        MessageDelivery delivery;

        Thread events;

        // Events: Connect
        public delegate void CommunicationEventHandler(object sender, IMensaje mensaje);
        public event CommunicationEventHandler Connect;
        protected virtual void OnConnect(IMensaje e)
        {
            if (Connect != null)
                Connect(this, e);
        }
        // Events: Disconnect
        public event CommunicationEventHandler Disconnect;
        protected virtual void OnDisconnect(IMensaje e)
        {
            if (Disconnect != null)
                Disconnect(this, e);
        }

        // Events: LocationChanged
        public event CommunicationEventHandler LocationChanged;
        protected virtual void OnLocationChanged(IMensaje e)
        {
            if (LocationChanged != null)
                LocationChanged(this, e);
        }

        // Constructor
        public CommunicationService(NetworkStream stream)
        {
            delivery = new MessageDelivery(stream);
            delivery.OpenDelivery();

            events = new Thread(incomingMessage);
            events.Start();
        }

        // Thread Method
        private void incomingMessage()
        {
            string str;
            IMensaje message;

            while (true)
            {
                str = delivery.RecieveMessage();
                message = SerializarcionJson.Deserializar<IMensaje>(str);

                MsgConexion menCon;
                MsgLocalizacion menLoc;

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

                    OnLocationChanged(menLoc);
                }
            }
        }

        public void SendToClient(IMensaje message)
        {
            string str = SerializarcionJson.Serializar<IMensaje>(message);

            delivery.SendMessage(str);
        }
    }
}
