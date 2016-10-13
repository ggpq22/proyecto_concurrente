using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Mensajes;
using SistemaTrackingBiblioteca.Serializacion;
using System.Threading;

namespace ServidorTracking
{
    class MessageRouter
    {
        List<ServerClient> clientes = new List<ServerClient>();
        ConcurrentQueue<IMensaje> mensajes = new ConcurrentQueue<IMensaje>();
        Thread routing;

        public MessageRouter()
        {
            routing = new Thread(route);

            routing.Start();
        }

        private void route()
        {
            while (true)
            {
                if (mensajes.Count > 0)
                {
                    IMensaje m;

                    while (mensajes.TryDequeue(out m))
                    {
                        foreach (ServerClient sc in clientes)
                        {
                            if (sc.Name == m.To)
                            {
                                sc.SendToClient(m);
                            }
                        }
                    }
                }
            }
        }

        public void AddClient(ServerClient client)
        {
            clientes.Add(client);
        }


    }
}
