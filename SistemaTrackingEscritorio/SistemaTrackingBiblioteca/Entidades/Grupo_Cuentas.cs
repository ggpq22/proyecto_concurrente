using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Entidades
{
    //create table grupo_cuentas
    //(
    //    idGrupo integer foreign key references grupo,
    //    idCuenta integer foreign key references cuenta
    //);
    public class Grupo_Cuentas : DBEntidad
    {
        public int IdCuenta { get; set; }

    }
}
