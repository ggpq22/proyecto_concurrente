using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class MsgConexion : IMensaje
    {
        [JsonConstructor]
        public MsgConexion() { }

        [JsonProperty]
        public string From { get; set; }
        [JsonProperty]
        public string To { get; set; }
        [JsonProperty]
        public DateTime Fecha { get; set; }
        [JsonProperty]
        public string Mensaje { get; set; }

    }
}
