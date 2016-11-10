using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SistemaTrackingBiblioteca.Mensajes;


namespace SistemaTrackingBiblioteca.Serializacion
{
    public static class SerializarcionJson
    {
        public static string Serializar<Tipo>(Tipo clase)
        {
            return JsonConvert.SerializeObject(clase);
        }

        public static Object Deserializar(string json)
        {
            Mensaje msg = JsonConvert.DeserializeObject<Mensaje>(json);

            return DevolverMensaje(msg.Tipo, json);
        }

        #region helper

        public static Object DevolverMensaje(string tipo, string json)
        {
            Object obj = new Object();

            switch (tipo)
            {
                case "MsgConexion":
                    obj = JsonConvert.DeserializeObject<MsgConexion>(json);
                    break;
                case "MsgLocalizacion":
                    obj = JsonConvert.DeserializeObject<MsgLocalizacion>(json);
                    break;
                case "MsgDBPeticion":
                    obj = JsonConvert.DeserializeObject<MsgDBPeticion>(json);
                    break;
                case "MsgDBRespuesta":
                    obj = JsonConvert.DeserializeObject<MsgDBRespuesta>(json);
                    break;
                case "MsgDBNotificacion":
                    obj = JsonConvert.DeserializeObject<MsgNotificacion>(json);
                    break;
                default:
                    break;
            }

            return obj;
        }


        #endregion
    }
}
