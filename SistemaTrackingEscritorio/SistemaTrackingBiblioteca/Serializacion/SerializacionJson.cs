using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Serializacion
{
    public static class SerializarcionJson
    {
        public static string Serializar<Tipo>(Tipo clase)
        {
            return JsonConvert.SerializeObject(clase);
        }

        public static Tipo Deserializar<Tipo>(string json)
        {
            return  JsonConvert.DeserializeObject<Tipo>(json);
        }
    }
}
