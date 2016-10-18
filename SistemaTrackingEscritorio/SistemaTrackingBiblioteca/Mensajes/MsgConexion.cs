using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class MsgConexion : Mensaje
    {
        [JsonConstructor]
        public MsgConexion() {
            base.Tipo = this.GetType().Name;
        }

        public string Mensaje { get; set; }
        
    }
}
