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

    public class Historial : DBEntidad
    {
        public Grupo Grupo { get; set; }
        public Cuenta Cuenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

    }
}
