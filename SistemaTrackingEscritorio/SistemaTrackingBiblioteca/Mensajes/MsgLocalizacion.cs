using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Mensajes
{
    [JsonObject]
    class MsgLocalizacion : IMensaje
    {
        [JsonConstructor]
        public MsgLocalizacion() { }

        [JsonProperty]
        public string From { get; set; }
        
        [JsonProperty]
        public string To { get; set; }

        [JsonProperty]
        public DateTime Fecha { get; set; }
    }
}
