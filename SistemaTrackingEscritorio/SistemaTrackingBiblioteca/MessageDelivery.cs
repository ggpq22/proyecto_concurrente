using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca
{
    class MessageDelivery
    {
        StreamWriter writer;
        StreamReader reader;
        NetworkStream stream;
        TcpClient client;

        public TcpClient Client
        {
            get { return client; }
            set 
            {
                client = value;
                this.stream = client.GetStream();
            }
        }

        public MessageDelivery(TcpClient client)
        {
            this.client = client;
            this.stream = this.client.GetStream();
        }

        public void OpenDelivery()
        {
            try
            {
                writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.ASCII);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void CloseDelivery()
        {
            try
            {
                writer.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string RecieveMessage()
        {
            try
            {
                string line = reader.ReadLine();
                return line;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                writer.WriteLine(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}