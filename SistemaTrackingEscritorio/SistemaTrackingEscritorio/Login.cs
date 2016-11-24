using SistemaTrackingBiblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaTrackingBiblioteca.Mensajes;
using SistemaTrackingBiblioteca.Entidades;
using System.Threading;
using System.Threading.Tasks;

namespace Mapa
{
    public delegate void delHilo(object token);
    public delegate void delThread();

    public partial class Login : Form
    {
        bool conectado;
        Guid guid;
        CancellationTokenSource cancelar;
        Thread isConected;
        frmPrincipal frm;
        delThread DelMostrarForm;
        private Sesion sesion;

        public Login()
        {
            InitializeComponent();
        }

        private void lblNuevaCuenta_LinkClicked(object sender, EventArgs e)
        {
            if (!conectado)
            {
                MessageBox.Show("Hay problemas de coneccion");
                return;
            }

            var cuenta = new Cuenta()
            {
                Usuario = tbUsuario.Text,
                Pass = tbPassword.Text,
                RecibeLocalizacion = 1,
            };

            MsgDBPeticion msg = new MsgDBPeticion()
            {
                From = guid.ToString(),
                Fecha = DateTime.Now,
                CodigoPeticion = "CrearCuenta",
            };

            msg.To.Add(guid.ToString());
            msg.ParamsCuenta.Add(cuenta);

            sesion.Server.SendToServer(msg);

        }

        void server_Disconnect(object sender, Mensaje mensaje)
        {
            conectado = false;
        }

        void server_Connect(object sender, Mensaje mensaje)
        {
            conectado = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!conectado)
            {
                MessageBox.Show("Hay problemas de coneccion");
                return;
            }

            btnLogin.Enabled = false;
            btnCrearCuenta.Enabled = false;

            var cuenta = new Cuenta()
            {
                Usuario = tbUsuario.Text,
                Pass = tbPassword.Text,
            };

            MsgDBPeticion msg = new MsgDBPeticion()
            {
                From = guid.ToString(),
                Fecha = DateTime.Now,
                CodigoPeticion = "Login",
                To = new List<string>() { guid.ToString() },
                ParamsCuenta = new List<Cuenta>() { cuenta },
            };

            sesion.Server.SendToServer(msg);

        }

        private void Login_Load(object sender, EventArgs e)
        {
            guid = System.Guid.NewGuid();
            DelMostrarForm += MostrarForm;
            sesion = new Sesion();
            LanzarHiloConexion();
        }

        public void LanzarHiloConexion()
        {
            cancelar = new CancellationTokenSource();
            delHilo del = hilo;
            isConected = new Thread(hilo);
            isConected.Start(cancelar.Token);
        }

        void hilo(object t)
        {
            var token = (CancellationToken)t;
            while (!token.IsCancellationRequested)
            {
                if (!conectado)
                {
                    try
                    {
                        var ip = Configuracion.GetConfiguracion("IpServidor");
                        var puerto = Configuracion.GetConfiguracion("PuertoServidor");
                        var retorno = new ServerClient(ip, int.Parse(puerto));
                        sesion.Server = retorno;
                        sesion.Server.Connect += server_Connect;
                        sesion.Server.Disconnect += server_Disconnect;
                        sesion.Server.DBRespuesta += server_DBRespuesta;

                        var msg = new MsgConexion()
                        {
                            From = guid.ToString(),
                            Mensaje = "conectar",
                            Fecha = DateTime.Now

                        };
                        msg.To.Add(guid.ToString());

                        sesion.Server.SendToServer(msg);
                        break;
                    }
                    catch (Exception ex)
                    {
                    }
                }

                Thread.Sleep(5000);
            }
        }

        void server_DBRespuesta(object sender, Mensaje mensaje)
        {
            if (btnCrearCuenta.InvokeRequired)
            {
                btnCrearCuenta.Invoke(new Action(() =>
                {
                    btnCrearCuenta.Enabled = true;
                    btnLogin.Enabled = true;
                }));
            }
            else
            {
                btnCrearCuenta.Enabled = true;
                btnLogin.Enabled = true;
            }

            var msg = mensaje as MsgDBRespuesta;
            if (msg.CodigoPeticion.Equals("Login"))
            {
                if (msg.IsValido)
                {
                    sesion.Usuario = msg.ReturnCuenta[0];
                    cancelar.Cancel();
                    isConected.Join();
                    tbUsuario.Invoke(DelMostrarForm);
                }
                else
                {
                    MessageBox.Show(msg.Errores[0]);
                }
            }
            else if (msg.CodigoPeticion.Equals("CrearCuenta"))
            {
                if (msg.IsValido)
                {
                    MessageBox.Show("Se creo correctamente");
                }
                else
                {
                    MessageBox.Show(msg.Errores[0]);
                }
            }
        }

        void MostrarForm()
        {
            sesion.Server.Connect -= server_Connect;
            sesion.Server.Disconnect -= server_Disconnect;
            sesion.Server.DBRespuesta -= server_DBRespuesta;
            sesion.FormLogin = this;
            sesion.FormPrincipal = new frmPrincipal(sesion);
            sesion.FormPrincipal.Show();
            this.Visible = false;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            MsgConexion msg = new MsgConexion()
            {
                From = guid.ToString(),
                To = new List<string>() { guid.ToString() },
                Mensaje = "desconectar",
                Fecha = DateTime.Now,

            };

            if (sesion.Server != null)
            {
                sesion.Server.SendToServer(msg);
                sesion.Server.client.Close();
            }

            cancelar.Cancel();
            isConected.Join();

        }

        public void Reconexion()
        {
            conectado = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                MsgConexion msg = new MsgConexion()
                {
                    Fecha = DateTime.Now,
                    From = guid.ToString() + i,
                    To = new List<string>() { guid.ToString() + i },
                    Mensaje = "conectar"
                };
                sesion.Server.SendToServer(msg);
            }
        }
    }
}
