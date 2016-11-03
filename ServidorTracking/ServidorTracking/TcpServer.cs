using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTracking
{
    class TcpServer
    {
        IPAddress ipAddress;

        public IPAddress IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        TcpListener listener;
        /// <summary>
        /// Constructor de TcpServer
        /// </summary>
        /// <param name="ipAddress">Direccion Ip</param>
        /// <param name="port">Puerto</param>
        public TcpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
        }

        public void StartServer()
        {
            try
            {
                listener = new TcpListener(ipAddress, port);
                listener.Start();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void StopServer()
        {
            try
            {
                listener.Stop();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public TcpClient AcceptClient()
        {
            try
            {
                return listener.AcceptTcpClient();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
