using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Entidades
{
    //create table historial
    //(
    //    idHistorial integer primary key,
    //    idGrupo integer foreign key references grupo,
    //    idCuenta integer foreign key references cuenta,
    //    fecha DateTime,
    //    lat decimal,
    //    long decimal
    //);

    class Historial
    {
        public int IdHistorial { get; set; }

        public int IdGrupo { get; set; }

        public int IdCuenta { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

    }
}
