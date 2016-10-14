using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Mensajes
{
    [JsonObject]
    public class MsgLocalizacion : Mensaje
    {
        public MsgLocalizacion()
        {
            base.Tipo = "MsgLocalizacion";
        }

        public string Longitud { get; set; }

        public string Latitud { get; set; }
    }
}
