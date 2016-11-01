using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SistemaTrackingBiblioteca
{
    public class MarcadorGoogle : GMapMarker, ISerializable
    {
        public string Usuario { get; set; }

        private GMarkerGoogle innerMarker;

        public Font Font { get; set; }

        public MarcadorGoogle(PointLatLng p, string usuario, GMarkerGoogleType type)
            : base(p)
        {
            Font = new Font("Arial", 14);

            this.Usuario = usuario;

            this.innerMarker = new GMarkerGoogle(p, type);

        }

        public override void OnRender(Graphics g)
        {
            if (innerMarker != null)
            {
                innerMarker.OnRender(g);
            }

            g.DrawString(Usuario, Font, Brushes.Black, new PointF(0.0f, innerMarker.Size.Height));
        }

        public override void Dispose()
        {
            if (innerMarker != null)
            {
                innerMarker.Dispose();
                innerMarker = null;
            }

            base.Dispose();
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected MarcadorGoogle(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

    }
}
