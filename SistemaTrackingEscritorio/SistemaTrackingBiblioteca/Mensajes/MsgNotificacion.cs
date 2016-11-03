using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class MsgNotificacion : Mensaje
    {
        public MsgNotificacion()
        {
            base.Tipo = this.GetType().Name;
        }

        public MsgDBPeticion Peticion { get; set; }
    }
}
