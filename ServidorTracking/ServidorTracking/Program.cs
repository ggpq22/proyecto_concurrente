using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;

namespace ServidorTracking
{
    class Program
    {
        static void Main(string[] args)
        {

            string ip = "10.75.60.137";

            int port = 8999;

            TcpServer server = new TcpServer(ip, port);
            MessageRouter router = new MessageRouter();

            Thread runningServer = new Thread(() => {

                try
                {
                    server.StartServer();

                    while(true)
                    {
                        TcpClient tcpclient = server.AcceptClient();
                        Console.WriteLine("Cliente nuevo");

                        ServerClient client = new ServerClient(tcpclient, router);

                        router.AddClient(client);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("== ERROR == -" + e.Message);
                }
            });

            try
            {
                runningServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("== ERROR == -" + e.Message);
            }

            Console.WriteLine("Server running...");

            string command;

            while (true)
            {
                command = Console.ReadLine();

                if (command == "shutdown")
                {
                    //Console.Beep();
                    Environment.Exit(0);
                }
            }
        }
    }
}
