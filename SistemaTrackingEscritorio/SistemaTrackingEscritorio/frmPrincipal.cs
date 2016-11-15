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
using SistemaTrackingBiblioteca.Entidades;

namespace Mapa
{
    public partial class frmPrincipal : Form
    {
        private CancellationTokenSource tokenProgress;
        private Sesion sesion;
        private GMapOverlay markerOverlay;
        private GMapOverlay overlay;
        private Thread TareaProgreso;

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
            sesion.Server.Connect += Server_Connect;
            sesion.Server.DBRespuesta += Server_DBRespuesta;
            sesion.Server.Disconnect += Server_Disconnect;
            sesion.Server.LocationChanged += Server_LocationChanged;

            ConfiguracionMapa();
            new Thread(ConectarServidor).Start();
            //sesion.Progress = new frmProgresBar();
            //sesion.Progress.Show();
            tokenProgress = new CancellationTokenSource();
            ParameterizedThreadStart p = (object o) =>
            {
                var token = (CancellationToken)o;
                var contador = 0;
                while (!token.IsCancellationRequested)
                {
                    pbProgreso.Invoke(new Action(() =>
                    {
                        contador = contador == 100 ? 0 : contador + 10;
                        pbProgreso.Value = contador;
                    }));

                    Thread.Sleep(500);
                }
            };

            TareaProgreso = new Thread(p);
            pbProgreso.Visible = true;
            TareaProgreso.Start(tokenProgress.Token);

            

        }

        void Server_Disconnect(object sender, Mensaje mensaje)
        {

        }

        void Server_Connect(object sender, Mensaje mensaje)
        {
            new Thread(BuscarGruposAnfitrion).Start();
        }

        void Server_LocationChanged(object sender, Mensaje mensaje)
        {
            var msg = mensaje as MsgLocalizacion;

            var lat = Double.Parse(msg.Latitud);
            var lng = Double.Parse(msg.Longitud);
            var esta = sesion.CuentasUsuario.FirstOrDefault(x => x.Usuario == mensaje.From);
            if (esta == null)
            {
                return;
            }

            var marcador = mapa.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == msg.From);
            if (marcador == null)
            {

                mapa.Overlays[0].Markers.Add(new GMarkerGooglePers(new PointLatLng(lat, lng), GMarkerGoogleType.red, msg.From));
            }
            else
            {
                marcador.Position = new PointLatLng(lat, lng);
            }

        }

        void Server_DBRespuesta(object sender, Mensaje mensaje)
        {
            var msg = mensaje as MsgDBRespuesta;
            if (msg.CodigoPeticion.Equals("GetGrupoPorAnfitrion"))
            {
                sesion.Grupos = msg.ReturnGrupo;
                dgvGruposAnfitrion.Invoke(new Action(() =>
                {
                    dgvGruposAnfitrion.DataSource = sesion.Grupos;
                    ConfigurarGrillaGrupo();
                }));
                tokenProgress.Cancel();
                TareaProgreso.Join();
                pbProgreso.Invoke(new Action(() => { pbProgreso.Visible = false; }));
                //((frmProgresBar)sesion.Progress).token.Cancel();
                //((frmProgresBar)sesion.Progress).tarea.Join();
                //sesion.FormPrincipal.Visible = true;

            }

        }

        private void ConfigurarGrillaGrupo()
        {
            dgvGruposAnfitrion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGruposAnfitrion.MultiSelect = false;
            dgvGruposAnfitrion.Columns["Id"].Visible = false;
            dgvGruposAnfitrion.Columns["Anfitrion"].Visible = false;
            //dgvGruposAnfitrion.Columns["Integrantes"].Visible = false;
            dgvGruposAnfitrion.CellClick += dgvGruposAnfitrion_SelectionChan;
            dgvGruposAnfitrion.Columns["Nombre"].Width = dgvGruposAnfitrion.Width;
        }


        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void ConectarServidor()
        {
            var ip = Configuracion.GetConfiguracion("IpServidor");
            var puerto = Configuracion.GetConfiguracion("PuertoServidor");

            MsgConexion msg = new MsgConexion()
            {
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },
                Fecha = DateTime.Now,
                Mensaje = "conectar",

            };

            sesion.Server.SendToServer(msg);
        }

        void ConfiguracionMapa()
        {
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
            this.markerOverlay = overlay;
            this.Controls.Add(mapa);

        }

        private void btnGrupos_Click(object sender, EventArgs e)
        {
            sesion.form = this;

            sesion.Server.Connect -= Server_Connect;
            sesion.Server.DBRespuesta -= Server_DBRespuesta;
            sesion.Server.Disconnect -= Server_Disconnect;
            sesion.Server.LocationChanged -= Server_LocationChanged;

            frmGrupoCrear frm = new frmGrupoCrear(sesion);
            frm.Show();
            this.Visible = false;
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            sesion.FormLogin.Visible = true;
        }


        private void BuscarGruposAnfitrion()
        {
            MsgDBPeticion msg = new MsgDBPeticion()
            {
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },
                CodigoPeticion = "GetGrupoPorAnfitrion",
                Fecha = DateTime.Now,
                ParamsCuenta = new List<Cuenta>() { sesion.Usuario },
            };

            sesion.Server.SendToServer(msg);
        }

        private void dgvGruposAnfitrion_SelectionChan(object sender, EventArgs e)
        {
            try
            {
                var nombre = dgvGruposAnfitrion.SelectedRows[0].Cells["Nombre"].Value.ToString();

                sesion.CuentasUsuario = sesion.Grupos.FirstOrDefault(x => x.Nombre == nombre).Integrantes;
                dgvUsuariosGrupo.DataSource = sesion.CuentasUsuario;
                mapa.Overlays[0].Markers.Clear();
                ConfigurarGrillaIntegrantes();
            }
            catch (Exception ex) { }
        }

        private void ConfigurarGrillaIntegrantes()
        {
            dgvUsuariosGrupo.MultiSelect = false;
            dgvUsuariosGrupo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuariosGrupo.Columns["Id"].Visible = false;
            dgvUsuariosGrupo.Columns["Pass"].Visible = false;
            dgvUsuariosGrupo.Columns["RecibeLocalizacion"].Visible = false;
            dgvUsuariosGrupo.Columns["Usuario"].Width = dgvUsuariosGrupo.Width;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            new Thread(() => {
                MsgLocalizacion msg = new MsgLocalizacion()
                {
                    From = "test2",
                    To = new List<string>() { "prueba"},
                     Fecha = DateTime.Now,
                      Latitud = "-66",
                      Longitud = "-33",
                      
                };
                
                MsgLocalizacion msg1 = new MsgLocalizacion()
                {
                    From = "a@a.a",
                    To = new List<string>() { "prueba"},
                     Fecha = DateTime.Now,
                      Latitud = "-33",
                      Longitud = "-66",
                      
                };

                while (true)
                {
                    PruebaLocalicacion(msg);
                    PruebaLocalicacion(msg1);
                    Thread.Sleep(1000);
                    msg.Latitud = (int.Parse(msg.Latitud) + 1).ToString();
                    msg.Longitud = (int.Parse(msg.Longitud) + 1).ToString();
                    msg1.Latitud = (int.Parse(msg1.Latitud) + 1).ToString();
                    msg1.Longitud = (int.Parse(msg1.Longitud) + 1).ToString();
                }
            }).Start();
        }

        void PruebaLocalicacion(Mensaje mensaje)
        {
            var msg = mensaje as MsgLocalizacion;

            var lat = Double.Parse(msg.Latitud);
            var lng = Double.Parse(msg.Longitud);
            var esta = sesion.CuentasUsuario.FirstOrDefault(x => x.Usuario == mensaje.From) ;
            if (esta == null)
            {
                return;
            }

            var marcador = mapa.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == msg.From);
            if (marcador == null)
            {

                mapa.Overlays[0].Markers.Add(new GMarkerGooglePers(new PointLatLng(lat, lng), GMarkerGoogleType.red, msg.From));
            }
            else
            {
                marcador.Position = new PointLatLng(lat, lng);
            }

        }

        private void dgvUsuariosGrupo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var usuario = dgvUsuariosGrupo.SelectedRows[0].Cells["Usuario"].Value.ToString();

            var marcador = mapa.Overlays[0].Markers.FirstOrDefault(x => ((GMarkerGooglePers)x).nombre == usuario);

            if (marcador == null)
            {
                return;
            }

            mapa.Position = marcador.Position;
            mapa.Zoom = 9;
        }

    }
}
