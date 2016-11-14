using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTracking
{
    class MessageDelivery
    {
        StreamWriter writer;
        StreamReader reader;
        NetworkStream stream;
        ServerClient client;

        public NetworkStream Stream
        {
          get { return stream; }
          set { stream = value; }
        }

        public MessageDelivery(NetworkStream stream, ServerClient client)
        {
            this.client = client;
            this.stream = stream;
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
                reader.DiscardBufferedData();
                return line;
            }
            catch (Exception e)
            {
                client.RemoveMe();
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
                client.RemoveMe();
                throw e;
            }
        }
    }
}