using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Serializacion;
using System.Net.Sockets;
using System.Threading;

namespace ServidorTracking
{
    class CommunicationService
    {
        SerializacionJson<IMensaje> serializacion;
        MessageDelivery delivery;

        public delegate void CommunicationEventHandler(IMensaje message);

        Thread events;

        //public delegate void CommunicationEventHandler

        public CommunicationService(NetworkStream stream)
        {
            delivery = new MessageDelivery(stream);
            serializacion = new SerializacionJson<IMensaje>();
        }

        private void incomingMessage()
        {
            delivery.OpenDelivery();
            string str;
            IMensaje message;

            while (true)
            {
                str = delivery.RecieveMessage();
                message = serializacion.Deserializar(str);


            }
        }
    }
}
