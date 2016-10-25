using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Entidades
{
    //create table grupo
    //(
    //    idGrupo integer primary key,
    //    idAnfitrion integer foreign key references cuenta
    //);
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public Cuenta Anfitrion { get; set; }
        public List<Cuenta> Integrantes { get; set; }
    }
}
