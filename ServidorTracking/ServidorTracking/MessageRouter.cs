using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Serializacion;
using System.Threading;

namespace ServidorTracking
{
    class MessageRouter
    {
        List<ServerClient> clientes = new List<ServerClient>();
        ConcurrentQueue<IMensaje> mensajes = new ConcurrentQueue<IMensaje>();
        Thread routing;

        private void route()
        {
            while (true)
            {
                if (mensajes.Count > 0)
                {

                }
            }
        }
    }
}
