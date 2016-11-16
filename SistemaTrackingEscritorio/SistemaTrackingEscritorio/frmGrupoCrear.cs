using SistemaTrackingBiblioteca;
using SistemaTrackingBiblioteca.Entidades;
using SistemaTrackingBiblioteca.Mensajes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Mapa
{
    public partial class frmGrupoCrear : Form
    {
        private Sesion sesion;

        private List<Cuenta> CuentasUsuarioBusqueda = new List<Cuenta>();

        private List<Cuenta> CuentaUsuarioParcial = new List<Cuenta>();

        private Grupo grupoNuevo = new Grupo();

        private Thread tareaProgreso;

        private CancellationTokenSource tokenProgreso;

        public frmGrupoCrear()
        {
            InitializeComponent();
        }

        public frmGrupoCrear(Sesion sesion)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.sesion = sesion;
            AsignarEventos();
        }

        private void frmGrupoCrear_Load(object sender, EventArgs e)
        {
            PedirCuentaDeUsuarios();

            btnCrearGrupo.Click += CrearGrupo;

            grupoNuevo = new Grupo()
            {
                Anfitrion = sesion.Usuario,
            };


            //CuentasUsuarioBusqueda = new List<Cuenta>(){
            //    new Cuenta(){ Usuario = "Mario", Id = 5, Pass = "asd", RecibeLocalizacion = 1},
            //    new Cuenta(){ Usuario = "Carlos",Id = 2, Pass = "asd", RecibeLocalizacion = 1},
            //    new Cuenta(){ Usuario = "Mario1", Id = 3, Pass = "asd", RecibeLocalizacion = 1},
            //    new Cuenta(){ Usuario = "Carlos1", Id = 4, Pass = "asd", RecibeLocalizacion = 1},
            //};

            dgvUsuarioBusqueda.DataSource = CuentasUsuarioBusqueda;
            ConfiguracionGrillaBusqueda();
        }

        private void CrearGrupo(object sender, EventArgs e)
        {

            grupoNuevo.Nombre = tbNombreGrupo.Text != string.Empty ? tbNombreGrupo.Text : null;

            if (grupoNuevo.Nombre == null)
            {
                MessageBox.Show("Ingrese el nombre del grupo");
                return;
            }

            grupoNuevo.Integrantes = ListaIntegrantesSeleccionados();

            if (grupoNuevo.Integrantes.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un integrante");
                return;
            }

            MsgDBPeticion msg = new MsgDBPeticion()
            {
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },
                Fecha = DateTime.Now,
                CodigoPeticion = "CrearGrupo",
                ParamsGrupo = new List<Grupo>() { grupoNuevo },
            };

            sesion.Server.SendToServer(msg);

            ActivarProgressBar();
            btnCrearGrupo.Enabled = false;

        }

        private void ActivarProgressBar()
        {
            tokenProgreso = new CancellationTokenSource();

            prProgreso.Visible = true;

            ParameterizedThreadStart param = (object o) =>
            {
                var contador = 0;
                var token = (CancellationToken)o;
                while (!token.IsCancellationRequested)
                {
                    contador = contador == 100 ? 0 : contador + 10;

                    prProgreso.Invoke(new Action(() => { prProgreso.Value = contador; }));

                    Thread.Sleep(500);
                }

            };

            tareaProgreso = new Thread(param);
            tareaProgreso.Start(tokenProgreso.Token);
        }

        private List<Cuenta> ListaIntegrantesSeleccionados()
        {
            List<Cuenta> retorno = new List<Cuenta>();

            for (int i = 0; i < dgvUsuarioBusqueda.SelectedRows.Count; i++)
            {
                Cuenta cuenta = new Cuenta()
                {
                    Usuario = dgvUsuarioBusqueda.SelectedRows[i].Cells[0].Value.ToString(),
                    Id = int.Parse(dgvUsuarioBusqueda.SelectedRows[i].Cells[3].Value.ToString()),
                    Pass = dgvUsuarioBusqueda.SelectedRows[i].Cells[1].Value.ToString(),
                    RecibeLocalizacion = int.Parse(dgvUsuarioBusqueda.SelectedRows[i].Cells[2].Value.ToString()),
                };

                retorno.Add(cuenta);
            }

            return retorno;
        }

        private void PedirCuentaDeUsuarios()
        {
            ActivarProgressBar();
            MsgDBPeticion msg = new MsgDBPeticion()
            {
                CodigoPeticion = "GetCuentas",
                From = sesion.Usuario.Usuario,
                Fecha = DateTime.Now,
            };


            msg.To.Add(sesion.Usuario.Usuario);

            sesion.Server.SendToServer(msg);
        }

        void Server_DBRespuesta(object sender, Mensaje mensaje)
        {
            var msg = mensaje as MsgDBRespuesta;
            tokenProgreso.Cancel();
            tareaProgreso.Join();
            if (prProgreso.InvokeRequired)
            {
                prProgreso.Invoke(new Action(() => { prProgreso.Visible = false; }));

            }
            else
            {
                prProgreso.Visible = false;
            }
            if (msg.CodigoPeticion.Equals("CrearGrupo"))
            {
                if (btnCrearGrupo.InvokeRequired)
                {
                btnCrearGrupo.Invoke(new Action(() => { btnCrearGrupo.Enabled = true; }));

                }
                else
                {
                    btnCrearGrupo.Enabled = true; 
                }
                try
                {
                    if (msg.IsValido)
                    {
                        MessageBox.Show("Se creo el grupo correctamente.");
                        sesion.form.Invoke(new Action(() => { sesion.form.Visible = true; }));
                        sesion.Grupos.Add(msg.ReturnGrupo[0]);
                        QuitarEventos();
                        if (((frmPrincipal)sesion.form).dgvGruposAnfitrion.InvokeRequired)
                        {
                            ((frmPrincipal)sesion.form).dgvGruposAnfitrion.Invoke(new Action(() =>
                            {
                                ((frmPrincipal)sesion.form).dgvGruposAnfitrion.DataSource = null;
                                ((frmPrincipal)sesion.form).dgvGruposAnfitrion.DataSource = sesion.Grupos;
                                ((frmPrincipal)sesion.form).ConfigurarGrillaGrupo();
                            }));
                        }
                        else
                        {
                            ((frmPrincipal)sesion.form).dgvGruposAnfitrion.DataSource = null;
                            ((frmPrincipal)sesion.form).dgvGruposAnfitrion.DataSource = sesion.Grupos;
                        }

                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => { this.Close(); }));
                        }
                        else
                        {
                            this.Close();

                        }

                    }
                    else
                    {
                        MessageBox.Show("Hubo un error.");
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else if (msg.CodigoPeticion.Equals("GetCuentas"))
            {
                try
                {
                    List<Cuenta> cuentas = msg.ReturnCuenta;
                    CuentasUsuarioBusqueda = cuentas.Where(x => x.RecibeLocalizacion == 0).ToList<Cuenta>();


                    Action action = new Action(() =>
                    {
                        dgvUsuarioBusqueda.DataSource = CuentasUsuarioBusqueda;
                        ConfiguracionGrillaBusqueda();
                    });
                    dgvUsuarioBusqueda.Invoke((Delegate)action);

                }
                catch (Exception)
                {
                }
            }

        }

        private void ConfiguracionGrillaBusqueda()
        {
            dgvUsuarioBusqueda.MultiSelect = true;
            dgvUsuarioBusqueda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarioBusqueda.Columns["Pass"].Visible = false;
            dgvUsuarioBusqueda.Columns["Id"].Visible = false;
            dgvUsuarioBusqueda.Columns["RecibeLocalizacion"].Visible = false;
        }

        private void frmGrupoCrear_FormClosing(object sender, FormClosingEventArgs e)
        {
            sesion.form.Visible = true;
            tokenProgreso.Cancel();
            tareaProgreso.Join();
            QuitarEventos();


        }

        private void AsignarEventos()
        {
            sesion.Server.DBRespuesta += Server_DBRespuesta;
        }

        private void QuitarEventos()
        {
            sesion.Server.DBRespuesta -= Server_DBRespuesta;
        }

    }
}
