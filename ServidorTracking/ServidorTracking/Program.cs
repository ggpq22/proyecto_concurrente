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
            Console.SetWindowSize(102, 35);

            string ip = "10.75.20.45";

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("== ERROR == -" + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });

            try
            {
                runningServer.Start();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("== ERROR == -" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
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
