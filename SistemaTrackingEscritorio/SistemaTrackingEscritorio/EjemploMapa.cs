using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Mapa
{
    public partial class EjemploMapa : Form
    {
        public EjemploMapa()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(37.583, -1.133);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            GMapOverlay gmo = new GMapOverlay("marker");
            GMapMarker gmm = new GMarkerGoogle(new PointLatLng(37.583, -1.133), GMarkerGoogleType.green);
            gmo.Markers.Add(gmm);
            gMapControl1.Overlays.Add(gmo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gMapControl1.Overlays[0].Markers[0].Position = new PointLatLng(37.800, -1.133);
        }
    }
}
