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
using ServidorTracking;
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
            gMapControl1.MapProvider = GMapProviders.GoogleChinaMap;
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

        void cliente_LocationChanged(object sender, Mensaje mensaje)
        {
            MsgLocalizacion localizacion = mensaje as MsgLocalizacion;

            gMapControl1.Overlays[0].Markers[0].Position = new PointLatLng()
            {
                Lat = Double.Parse(localizacion.Latitud),
                Lng = Double.Parse(localizacion.Longitud)
            };

        }

        void cliente_Disconnect(object sender, Mensaje mensaje)
        {
        }

        void cliente_Connect(object sender, Mensaje mensaje)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gMapControl1.Overlays[0].Markers[0].Position = new PointLatLng(37.800, -1.133);
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
                To = "Escritorio",
                Fecha = DateTime.Now,
                Mensaje = "conectar"
            });
        }
    }
}
