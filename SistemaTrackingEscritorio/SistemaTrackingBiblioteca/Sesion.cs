using SistemaTrackingBiblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SistemaTrackingBiblioteca
{
    public class Sesion
    {
        public Cuenta Usuario { get; set; }
        
        public List<GMarkerGooglePers> Marcadores = new List<GMarkerGooglePers>();

        public ServerClient Server { get; set; }

        public bool IsConected { get; set; }


        public List<Cuenta> CuentasUsuario { get; set; }

        public System.Threading.CancellationTokenSource TokenSource { get; set; }

        public Form FormLogin { get; set; }

        public List<Grupo> Grupos { get; set; }

        public Form FormPrincipal { get; set; }


        public Form FormCrearGrupo { get; set; }

        public Form FormAgregarIntegrante { get; set; }
    }
}
