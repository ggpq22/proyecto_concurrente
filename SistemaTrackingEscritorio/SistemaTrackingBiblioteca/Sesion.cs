using SistemaTrackingBiblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaTrackingBiblioteca
{
    public class Sesion
    {
        public Cuenta Usuario { get; set; }
        
        public List<GMarkerGooglePers> Marcadores = new List<GMarkerGooglePers>();

        public ServerClient Server { get; set; }

        public bool IsConected { get; set; }

    }
}
