using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Serializacion
{
    public class MensajeConexion :IMensaje
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Mensaje { get; set; }

    }
}
