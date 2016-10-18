using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Mensajes;
using SistemaTrackingBiblioteca.Serializacion;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MsgConexion cnn = new MsgConexion()
            {
                To = "asd",
                From = "asd",
                Mensaje = "asd",
                Fecha = DateTime.Now
            };
            
        }
    }
}
