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
            string ip = "10.75.60.83";
=======
            string ip = "localhost";
>>>>>>> ceb3065609c63cc4e5c4690e336281683e6fcf0f
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
