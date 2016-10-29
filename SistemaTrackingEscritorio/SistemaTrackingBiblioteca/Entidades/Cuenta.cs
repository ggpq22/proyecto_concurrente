using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Entidades
{
    //create table cuenta
    //(
    //    idCuenta integer primary key,
    //    usuario varchar(50),
    //    pass varchar(50)
    //);
    public class Cuenta : DBEntidad
    {
        public string Usuario { get; set; }
        public string Pass { get; set; }
    }
}
