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

        // Constructor
        public CommunicationService(NetworkStream stream, TcpClient client)
        {
            delivery = new MessageDelivery(stream, client);
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
                message = SerializarcionJson.Deserializar(str) as Mensaje;
                // TODO: Descerializar a Mensaje, checkear el tipo y descerializar a el tipo que corresponde
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

        public void SendToClient(Mensaje message)
        {
            string str = SerializarcionJson.Serializar<Mensaje>(message);

            delivery.SendMessage(str);
        }

        public void CloseCommunications()
        {
            delivery.CloseDelivery();
        }
    }
}
