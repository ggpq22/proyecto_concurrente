using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SistemaTrackingBiblioteca
{
    class TcpCliente
    {

        int port = 8999;
        TcpClient client;
        public TcpCliente()
        {

        }
        public void Start()
        {
            Console.WriteLine("Iniciando cliente...");
            client = new TcpClient("localhost", port);

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            Console.Write("Escriba y presione Enter para enviar: ");
            while (client.Connected)
            {
                string lineToSend = Console.ReadLine();
                Console.WriteLine("Enviando el servidor: " + lineToSend);
                
                writer.WriteLine(lineToSend);
                string lineReceived = reader.ReadLine();
                Console.WriteLine("El servidor respondio: " + lineReceived);
            }
            writer.Close();
            reader.Close();
            stream.Close();
            client.Close();
        }
    }
}
