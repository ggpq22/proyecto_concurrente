﻿using System;
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
            string ip = "10.75.60.51";
=======
            string ip = "10.75.60.145";
>>>>>>> 38e1284ff7dedbe86798f7807ba5ada42595118a
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
