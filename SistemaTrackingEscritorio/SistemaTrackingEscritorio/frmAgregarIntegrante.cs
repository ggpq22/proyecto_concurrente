using SistemaTrackingBiblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SistemaTrackingBiblioteca.Mensajes;
using SistemaTrackingBiblioteca.Entidades;

namespace Mapa
{
    public partial class frmAgregarIntegrante : Form
    {
        Sesion sesion;
        CancellationTokenSource tokenProgress;
        Thread TareaProgreso;

        List<Cuenta> usuarios;

        public frmAgregarIntegrante(Sesion sesion, Grupo grupo)
        {
            InitializeComponent();
            this.sesion = sesion;
            this.grupo = grupo;
            dgvGrupo.DataSource = null;
            dgvGrupo.DataSource = this.grupo.Integrantes;
            ConfigurarGrillaIntegrantes();
            AgregarEvento();
            ActivarBarra();
            PedirGrupos();
            lblNombreGrupo.Text = grupo.Nombre;

        }

        private void PedirGrupos()
        {
            MsgDBPeticion msg = new MsgDBPeticion()
            {
                CodigoPeticion = "GetCuentas",
                Fecha = DateTime.Now,
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },

            };

            sesion.Server.SendToServer(msg);
        }

        private void frmAgregarIntegrante_Load(object sender, EventArgs e)
        {

        }

        private void AgregarEvento()
        {
            this.sesion.Server.DBRespuesta += Server_DBRespuesta;
        }

        private void QuitarEvento()
        {
            this.sesion.Server.DBRespuesta -= Server_DBRespuesta;
        }

        void Server_DBRespuesta(object sender, SistemaTrackingBiblioteca.Mensajes.Mensaje mensaje)
        {
            var msg = mensaje as MsgDBRespuesta;

            switch (msg.CodigoPeticion)
            {
                case "GetCuentas":
                    if (msg.IsValido)
                    {
                        var retorno = new List<Cuenta>();
                        msg.ReturnCuenta = msg.ReturnCuenta.Where(x=> x.RecibeLocalizacion == 0).ToList();
                        foreach (var item in msg.ReturnCuenta)
                        {
                            bool esta = false;
                            foreach (var agregado in grupo.Integrantes)
                            {
                                if(item.Usuario == agregado.Usuario)
                                {
                                    esta = true;
                                }
                                                                                                
                            }

                            if (!esta)
                            {
                                retorno.Add(item);
                            }
                        }

                        usuarios = retorno;
                        dgvUsuario.DataSource = null;
                        if(dgvUsuario.InvokeRequired)
                        {
                            dgvUsuario.Invoke(new Action(() => { 
                                dgvUsuario.DataSource = retorno;
                                ConfigurarGrillaCandidatos();
                            }));
                        }
                        else
                        {
                            dgvUsuario.DataSource = retorno;
                            ConfigurarGrillaCandidatos();

                        }
                    }
                    else
                    {
                        usuarios = new List<Cuenta>();
                        dgvUsuario.DataSource = null;
                        dgvUsuario.DataSource = usuarios;
                    }
                    DesactivarBarra();
                    break;

                case "BorrarGrupo":
                    if (msg.IsValido)
                    {
                        CrearGrupo();
                    }
                    else
                    {
                        MessageBox.Show(msg.Errores[0]);
                    }
                    break;
                case "CrearGrupo":
                    if (msg.IsValido)
                    {
                        CerrarForm();
                    }
                    else
                    {
                        MessageBox.Show(msg.Errores[0]);
                    }
                    break;

                default:
                    break;
            }
        }

        private void CerrarForm()
        {
            DesactivarBarra();
            QuitarEvento();
            if (((frmPrincipal)sesion.FormPrincipal).dgvGruposAnfitrion.InvokeRequired)
            {
                ((frmPrincipal)sesion.FormPrincipal).dgvGruposAnfitrion.Invoke(new Action(() =>
                {
                    ((frmPrincipal)sesion.FormPrincipal).AsignarEventos();
                    ((frmPrincipal)sesion.FormPrincipal).BuscarGruposAnfitrion();
                    ((frmPrincipal)sesion.FormPrincipal).ActualizarGrupos();
                    ((frmPrincipal)sesion.FormPrincipal).Visible = true;
                    ((frmAgregarIntegrante)sesion.FormAgregarIntegrante).Close();
                }));
            }
            else
            {
                ((frmPrincipal)sesion.FormPrincipal).AsignarEventos();
                ((frmPrincipal)sesion.FormPrincipal).BuscarGruposAnfitrion();
                ((frmPrincipal)sesion.FormPrincipal).ActualizarGrupos();
                ((frmPrincipal)sesion.FormPrincipal).Visible = true;
                ((frmAgregarIntegrante)sesion.FormAgregarIntegrante).Close();
            }

            
        }

        internal void ActivarBarra()
        {
            btnAgregar.Enabled = false;
            btnCrear.Enabled = false;
            btnSacar.Enabled = false;
            tokenProgress = new CancellationTokenSource();
            ParameterizedThreadStart p = (object o) =>
            {
                var token = (CancellationToken)o;
                var contador = 0;
                while (!token.IsCancellationRequested)
                {
                    if (pbProgreso.InvokeRequired)
                    {
                        pbProgreso.Invoke(new Action(() =>
                        {
                            contador = contador == 100 ? 0 : contador + 10;
                            pbProgreso.Value = contador;
                        }));

                    }
                    else
                    {
                        contador = contador == 100 ? 0 : contador + 10;
                        pbProgreso.Value = contador;
                    }

                    Thread.Sleep(500);
                }
            };

            TareaProgreso = new Thread(p);
            if (pbProgreso.InvokeRequired)
            {
                pbProgreso.Invoke(new Action(() => { pbProgreso.Visible = true; }));
            }
            else
            {
                pbProgreso.Visible = true;
            }
            TareaProgreso.Start(tokenProgress.Token);
        }

        internal void DesactivarBarra()
        {
            if (btnAgregar.InvokeRequired)
            {
                btnAgregar.Invoke(new Action(() => {
                    btnAgregar.Enabled = true;
                    btnCrear.Enabled = true;
                    btnSacar.Enabled = true;
                }));
            }
            else
            {
                btnAgregar.Enabled = true;
                btnCrear.Enabled = true;
                btnSacar.Enabled = true;
            }
            
            tokenProgress.Cancel();
            TareaProgreso.Join();
            if (pbProgreso.InvokeRequired)
            {
                pbProgreso.Invoke(new Action(() => { pbProgreso.Visible = false; }));
            }
            else
            {
                pbProgreso.Visible = false;
            }
        }

        public Grupo grupo { get; set; }

        private void frmAgregarIntegrante_FormClosing(object sender, FormClosingEventArgs e)
        {
            sesion.FormPrincipal.Visible = true;
            QuitarEvento();
            DesactivarBarra();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para agregar");
                return;
            }
            var nombre = dgvUsuario.SelectedRows[0].Cells["usuario"].Value.ToString();
            var cuenta = usuarios.SingleOrDefault(x => x.Usuario == nombre);
            usuarios = usuarios.Where(x => x.Usuario != cuenta.Usuario).ToList();

            grupo.Integrantes.Add(cuenta);

            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            dgvGrupo.DataSource = null;
            dgvGrupo.DataSource = grupo.Integrantes;
            dgvUsuario.DataSource = null;
            dgvUsuario.DataSource = usuarios;
            ConfigurarGrillaCandidatos();
            ConfigurarGrillaIntegrantes();
        }

        private void btnSacar_Click(object sender, EventArgs e)
        {
            if (dgvGrupo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para sacar");
                return;
            }
            
            var nombre = dgvGrupo.SelectedRows[0].Cells["usuario"].Value.ToString();
            var cuenta = grupo.Integrantes.SingleOrDefault(x => x.Usuario == nombre);
            grupo.Integrantes = grupo.Integrantes.Where(x => x.Usuario != cuenta.Usuario).ToList();

            usuarios.Add(cuenta);

            ActualizarGrilla();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ActivarBarra();
            MsgDBPeticion msg = new MsgDBPeticion()
            {
                CodigoPeticion = "BorrarGrupo",
                Fecha = DateTime.Now,
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },
                ParamsGrupo = new List<Grupo>() { grupo },

            };
            sesion.Server.SendToServer(msg);
        }

        private void CrearGrupo()
        {
            MsgDBPeticion msg = new MsgDBPeticion()
            {
                CodigoPeticion = "CrearGrupo",
                Fecha = DateTime.Now,
                From = sesion.Usuario.Usuario,
                To = new List<string>() { sesion.Usuario.Usuario },
                ParamsGrupo = new List<Grupo>() { grupo },

            };

            sesion.Server.SendToServer(msg);
        }


        private void ConfigurarGrillaIntegrantes()
        {
            dgvGrupo.MultiSelect = false;
            dgvGrupo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrupo.Columns["Id"].Visible = false;
            dgvGrupo.Columns["Pass"].Visible = false;
            dgvGrupo.Columns["RecibeLocalizacion"].Visible = false;
            dgvGrupo.Columns["Usuario"].Width = dgvGrupo.Width;
        
        }

        private void ConfigurarGrillaCandidatos()
        {
            dgvUsuario.MultiSelect = false;
            dgvUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuario.Columns["Id"].Visible = false;
            dgvUsuario.Columns["Pass"].Visible = false;
            dgvUsuario.Columns["RecibeLocalizacion"].Visible = false;
            dgvUsuario.Columns["Usuario"].Width = dgvUsuario.Width;

        }
    }
}
