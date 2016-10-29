using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Entidades
{
    //create table servidor
    //(
    //    idServidor integer primary key,
    //    nombre varchar(50),
    //    ip varchar(50),
    //    puerto varchar(50)
    //);
    public class Servidor : DBEntidad
    {
        public string Nombre { get; set; }
        public string Ip { get; set; }
        public string Puerto { get; set; }
    }
}
