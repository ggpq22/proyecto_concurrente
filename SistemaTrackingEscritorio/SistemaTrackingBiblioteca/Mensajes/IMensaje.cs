using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class Mensaje
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
    }
}
