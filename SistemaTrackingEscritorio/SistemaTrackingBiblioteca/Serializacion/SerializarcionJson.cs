using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaTrackingBiblioteca.Serializacion
{
    public class SerializarcionJson<T>
    {
        public string Serializar(Object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented);
        }

        public T Deserializar(string json)
        {
            return  JsonConvert.DeserializeObject<T>(json);
        }
    }
}
