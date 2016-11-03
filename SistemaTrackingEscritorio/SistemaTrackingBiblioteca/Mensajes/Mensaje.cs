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
        public List<string> To = new List<string>();
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public bool IsValido = true;
        public List<string> Errores = new List<string>();
    }
}