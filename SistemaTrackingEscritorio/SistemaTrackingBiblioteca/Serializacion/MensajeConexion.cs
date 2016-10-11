using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Serializacion
{
    public class MensajeConexion :IMensaje
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Mensaje { get; set; }

        public string Serializar()
        {

            return "";
        }


        public void Deserealizar(Object o)
        {

        }
    }
}
