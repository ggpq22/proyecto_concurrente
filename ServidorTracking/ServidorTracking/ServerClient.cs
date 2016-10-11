using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SistemaTrackingBiblioteca.Serializacion;

namespace ServidorTracking
{
    class ServerClient
    {
        string name;
        TcpClient client;
        MessageDelivery delivery;
        ConcurrentQueue<IMensaje> mensajes = new ConcurrentQueue<IMensaje>();

        public ServerClient(TcpClient client)
        {
            try 
	        {
                this.client = client;
                delivery = new MessageDelivery(this.client.GetStream());
	        }
	        catch (Exception)
	        {
		        throw;
	        }
        }

        public string RecieveFromClient()
        {
            try
            {
                return delivery.RecieveMessage();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void SendToClient(IMensaje message)
        {
            mensajes.Enqueue(message);
        }

        public void CloseClient()
        {
            client.Close();
        }
    }
}
