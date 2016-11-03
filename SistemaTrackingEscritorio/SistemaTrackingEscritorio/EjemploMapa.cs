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
using SistemaTrackingBiblioteca;
using SistemaTrackingBiblioteca.Mensajes;

namespace Mapa
{
    public partial class EjemploMapa : Form
    {
        
        ServerClient cliente;

        public EjemploMapa()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.BingMap;
            gMapControl1.Position = new PointLatLng(-33,-66);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 12;
            gMapControl1.AutoScroll = true;

            GMapOverlay gmo = new GMapOverlay("marker");
            GMapMarker gmm = new GMarkerGoogle(new PointLatLng(-33, -66), GMarkerGoogleType.green);

            //MarcadorGoogle marca = new MarcadorGoogle(new PointLatLng(-33, -66), "lllll", GMarkerGoogleType.green);
            //MarcadorGoogle marc = new MarcadorGoogle(new PointLatLng(-34, -66), "ttrrqwrq", GMarkerGoogleType.green);

            gmo.Markers.Add(new GMarkerGooglePers(new PointLatLng(-33, -66), GMarkerGoogleType.green, "mario"));
            //gmo.Markers.Add(marc);

            gMapControl1.Overlays.Add(gmo);
            
        }

        void cliente_LocationChanged(object sender, Mensaje mensaje)
        {
            MsgLocalizacion localizacion = mensaje as MsgLocalizacion;

            var marker = gMapControl1.Overlays[0].Markers[0] as MarcadorGoogle;            
            
            marker.NuevoPunto(new PointLatLng(-33,-66));
        }

        void cliente_Disconnect(object sender, Mensaje mensaje)
        {
        }

        void cliente_Connect(object sender, Mensaje mensaje)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gMapControl1.Overlays[0].Markers[0].Position = new PointLatLng(37.800, -1.133);
            //var marker = gMapControl1.Overlays[0].Markers[0] as MarcadorGoogle;
            var marker = gMapControl1.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == "mario");
            marker.Position = new PointLatLng(-34, -67);
            //marker.NuevoPunto(new PointLatLng(37.800, -1.133));

            //gMapControl1.Overlays[0].Markers[0] = marker;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            cliente = new ServerClient(tbIp.Text, int.Parse(tbPuerto.Text));
            cliente.Connect += cliente_Connect;
            cliente.Disconnect += cliente_Disconnect;
            cliente.LocationChanged += cliente_LocationChanged;

            cliente.SendToServer(new MsgConexion()
            {

                From = "Escritorio",
                To = {"Escritorio"},

                Fecha = DateTime.Now,
                Mensaje = "conectar"
            });
        }

        private void tbIp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
