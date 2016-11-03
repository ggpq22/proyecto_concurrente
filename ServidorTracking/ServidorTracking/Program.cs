using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServidorTracking
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            string ip = "10.75.60.148";
=======
            string ip = "10.75.20.86";
>>>>>>> 64a722b7e94eb4ef2ee6158d99104c1ff8c04a2a
            int port = 8999;

            TcpServer server = new TcpServer(ip, port);
            MessageRouter router = new MessageRouter();

            Thread runningServer = new Thread(() => {

                server.StartServer();

                while(true)
                {
                    TcpClient tcpclient = server.AcceptClient();

                    ServerClient client = new ServerClient(tcpclient, router);

                    router.AddClient(client);
                }
            });

            runningServer.Start();

            Console.WriteLine("Server running...");
        }
    }
}
