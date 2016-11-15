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

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 35);

            string ip = GetLocalIPAddress();

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

                Console.WriteLine("Server running on IP: " + ip + ", PORT: " + port + ".");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("== ERROR == -" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

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
