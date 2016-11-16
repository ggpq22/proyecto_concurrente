using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Mensajes;
using SistemaTrackingBiblioteca.Serializacion;
using System.Threading;
using SistemaTrackingBiblioteca.Entidades;
using ServidorTracking.DataBase;

namespace ServidorTracking
{
    class MessageRouter
    {
        List<ServerClient> clientes = new List<ServerClient>();
        List<Grupo> grupos = new List<Grupo>();
        DBController dbCon;
        ConcurrentQueue<Mensaje> mensajes = new ConcurrentQueue<Mensaje>();
        CancellationToken cancelToken;
        Thread routing;

        CancellationToken token;

        public CancellationToken Token
        {
            get { return token; }
            set { token = value; }
        }

        public MessageRouter(CancellationToken cancellationToken)
        {
            cancelToken = cancellationToken;
            dbCon = new DBController("pbarco", "12345Pablo");

            try
            {
                grupos = dbCon.GetAllGrupos();
                routing = new Thread(route);

                routing.Start();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("== ERROR == -" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //este hilo se morfa la compu
        private void route()
        {
            while(!cancelToken.IsCancellationRequested)
            {
                if (mensajes.Count > 0)
                {
                    Mensaje m;

                    while (mensajes.TryDequeue(out m))
                    {
                        foreach (string to in m.To)
                        {
                            if (m is MsgLocalizacion)
                            {
                                foreach (Grupo g in grupos)
                                {
                                    if (g.Nombre == to)
                                    {
                                        List<Cuenta> cList = g.Integrantes;
                                        cList.Add(g.Anfitrion);

                                        foreach (Cuenta c in cList)
                                        {
                                            foreach (ServerClient sc in clientes)
                                            {
                                                if (sc.Name == c.Usuario)
                                                {
                                                    if (sc.GetsLocations == 1)
                                                    {
                                                        sc.SendToClient(m);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (ServerClient sc in clientes)
                            {
                                if (sc.Name == to)
                                {
                                    sc.SendToClient(m);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(500);
            }
            CloseRouter();
        }

        public void AddClient(ServerClient client)
        {
            clientes.Add(client);
        }

        public void RemoveClient(ServerClient client)
        {
            clientes.Remove(client);
        }

        public void UpdateGrupos(List<Grupo> grupos)
        {
            this.grupos = grupos;
        }

        public void RouteMessage(Mensaje message)
        {
            mensajes.Enqueue(message);
        }

        public void CloseRouter()
        {
            foreach (ServerClient sc in clientes)
            {
                sc.RemoveMe();
            }
        }

        public void WaitToFinish()
        {
            routing.Join();
        }
    }
}
