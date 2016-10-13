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
            string ip = "127.0.0.1";
            int port = 8999;

            TcpServer server = new TcpServer(ip, port);
            MessageRouter router = new MessageRouter();

            Thread runningServer = new Thread(() => { 
                while(true)
                {
                    server.StartServer();

                    ServerClient client = new ServerClient(server.AcceptClient(), router);

                    router.AddClient(client);
                }
            });

            runningServer.Start();

            Console.WriteLine("Server running...");
        }
    }
}
