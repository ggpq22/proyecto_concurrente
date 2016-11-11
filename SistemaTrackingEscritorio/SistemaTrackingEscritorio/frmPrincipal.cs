using SistemaTrackingBiblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Threading;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Mensajes;

namespace Mapa
{
    public partial class frmPrincipal : Form
    {
        private Sesion sesion;
        private GMarkerGoogle marker;
        private GMapOverlay markerOverlay;
        private Label hiloPrincipal;
        private GMapOverlay overlay;

        public frmPrincipal()
        {
            InitializeComponent();
            ConfiguracionMapa();
        }


        public frmPrincipal(Sesion sesion)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.sesion = sesion;
            sesion.Server.DBRespuesta += Server_DBRespuesta;
            sesion.Server.LocationChanged += Server_LocationChanged;
            sesion.Server.Connect += Server_Connect;
            sesion.Server.Disconnect += Server_Disconnect;
            ConfiguracionMapa();

            
        }

        void Server_Disconnect(object sender, Mensaje mensaje)
        {
            
        }

        void Server_Connect(object sender, Mensaje mensaje)
        {
            
        }

        void Server_LocationChanged(object sender, Mensaje mensaje)
        {
            var msg = mensaje as MsgLocalizacion;

            var lat = Double.Parse( msg.Latitud);
            var lng = Double.Parse(msg.Longitud);

            //var marcador = gMapa.Overlays[0].Markers.FirstOrDefault<GMarkerGooglePers>(x => x.nombre);
            var marcador = gMapa.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == msg.From);
            if(marcador == null)
            {

                gMapa.Overlays[0].Markers.Add(new GMarkerGooglePers(new PointLatLng(lat, lng), GMarkerGoogleType.red, msg.From));
            }
            else
            {
                marcador.Position = new PointLatLng(lat,lng);
            }

        }

        void Server_DBRespuesta(object sender, Mensaje mensaje)
        {
            throw new NotImplementedException();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            hiloPrincipal = new Label();
        }

        void ConfiguracionMapa()
        {
            var mapa = new GMap.NET.WindowsForms.GMapControl();

            mapa.Bearing = 0F;
            mapa.CanDragMap = true;
            mapa.EmptyTileColor = System.Drawing.Color.Navy;
            mapa.GrayScaleMode = false;
            mapa.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            mapa.LevelsKeepInMemmory = 5;
            mapa.Location = new System.Drawing.Point(12, 70);
            mapa.MarkersEnabled = true;
            mapa.MaxZoom = 2;
            mapa.MinZoom = 2;
            mapa.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapa.Name = "gMapa";
            mapa.NegativeMode = false;
            mapa.PolygonsEnabled = true;
            mapa.RetryLoadTile = 0;
            mapa.RoutesEnabled = true;
            mapa.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            mapa.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            mapa.ShowTileGridLines = false;
            mapa.Size = new System.Drawing.Size(549, 328);
            mapa.TabIndex = 0;
            mapa.Zoom = 0D;

            mapa.DragButton = MouseButtons.Left;
            mapa.CanDragMap = true;
            mapa.MapProvider = GMapProviders.GoogleMap;
            mapa.Position = new PointLatLng(-33, -66);
            mapa.MinZoom = 0;
            mapa.MaxZoom = 24;
            mapa.Zoom = 9;
            mapa.AutoScroll = true;
            overlay = new GMapOverlay("Marcador");
            mapa.Overlays.Add(overlay);
            this.gMapa = mapa;
            this.markerOverlay = overlay;
            this.Controls.Add(this.gMapa);

        }


        void NuevaConexion()
        {
            MsgConexion msg = new MsgConexion()
            {
                From = sesion.Usuario.Usuario,
                Fecha = DateTime.Now,
                Mensaje = "conectar",
                
            };

            msg.To.Add(sesion.Usuario.Usuario);

            sesion.Server.SendToServer(msg);
        }

    }
}
