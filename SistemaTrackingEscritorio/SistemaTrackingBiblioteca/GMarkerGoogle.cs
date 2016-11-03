using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Drawing;

namespace SistemaTrackingBiblioteca
{
    public class GMarkerGooglePers : GMarkerGoogle 
    {
        public string nombre { get; set; }

        public GMarkerGooglePers(PointLatLng p, GMarkerGoogleType t, string nombre) :base(p,t)
        {
            this.nombre = nombre;
        }

        public override void OnRender(System.Drawing.Graphics g)
        {
            base.OnRender(g);

            Font Font = new Font("Arial", 14);
            g.DrawString(nombre, Font, Brushes.Black,LocalPosition.X-10,LocalPosition.Y+30);
        }
    }
}
