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
        private Sesion sesion;

        public EjemploMapa()
        {
            InitializeComponent();
        }

        public EjemploMapa(Sesion sesion)
        {
            // TODO: Complete member initialization
            this.sesion = sesion;
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
            GMarkerGooglePers gmm = new GMarkerGooglePers(new PointLatLng(-33, -66), GMarkerGoogleType.green, "gonza");

            gmo.Markers.Add(gmm);

            gMapControl1.Overlays.Add(gmo);
            
        }

        void cliente_LocationChanged(object sender, Mensaje mensaje)
        {
            MsgLocalizacion localizacion = mensaje as MsgLocalizacion;
                var lat = Double.Parse(localizacion.Latitud);
                var lng = Double.Parse(localizacion.Longitud);

            var marker = gMapControl1.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == localizacion.From);

            if (marker != null)
            {
                marker = marker as GMarkerGooglePers;
                marker.Position = new PointLatLng(lat, lng);
            }
            else
            {
                var nuevo = new GMarkerGooglePers(new PointLatLng(lat, lng),GMarkerGoogleType.red,localizacion.From);
                gMapControl1.Overlays[0].Markers.Add(nuevo);
                sesion.Marcadores.Add(nuevo);
            }
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
            //cliente = new ServerClient(tbIp.Text, int.Parse(tbPuerto.Text));
            //cliente.Connect += cliente_Connect;
            //cliente.Disconnect += cliente_Disconnect;
            //cliente.LocationChanged += cliente_LocationChanged;

            //cliente.SendToServer(new MsgConexion()
            //{

            //    From = "escritorio",
            //    To = {"escritorio"},

            //    Fecha = DateTime.Now,
            //    Mensaje = "conectar"
            //});
        }

        private void tbIp_TextChanged(object sender, EventArgs e)
        {

        }

        private void EjemploMapa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("asd");
        }

        private void EjemploMapa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("asd");

        }
    }
}
