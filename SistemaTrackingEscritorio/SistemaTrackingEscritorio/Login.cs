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
    public delegate void delMostrarForm();

    public partial class Login : Form
    {
        ServerClient server = null;
        bool conectado;
        Guid guid;
        CancellationTokenSource cancelar;
        Thread isConected;
        frmPrincipal frm;
        delMostrarForm DelMostrarForm;
        private Sesion sesion;


        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblNuevaCuenta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            };

            MsgDBPeticion msg = new MsgDBPeticion()
            {
                From = guid.ToString(),
                Fecha = DateTime.Now,
                CodigoPeticion = "CrearCuenta",
            };

            msg.To.Add(guid.ToString());
            msg.ParamsCuenta.Add(cuenta);

            server.SendToServer(msg);

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
            };

            msg.To.Add(guid.ToString());
            msg.ParamsCuenta.Add(cuenta);

            server.SendToServer(msg);

        }

        private void Login_Load(object sender, EventArgs e)
        {
            guid = System.Guid.NewGuid();
            cancelar = new CancellationTokenSource();
            CancellationToken token = cancelar.Token;
            DelMostrarForm += MostrarForm;

            delHilo del = hilo;
            isConected = new Thread(hilo);


            isConected.Start(token);

            frm = new frmPrincipal();


        }

        void hilo(object t)
        {
            var token = (CancellationToken)t;
            while (!token.IsCancellationRequested)
            {
                if (!conectado)
                {
                    var tar = new Task(new Action(() =>
                    {
                        try
                        {
                            var ip = Configuracion.GetConfiguracion("IpServidor");
                            var puerto = Configuracion.GetConfiguracion("PuertoServidor");
                            var retorno = new ServerClient(ip, int.Parse(puerto), token);
                            server = retorno;
                            conectado = true;
                            server.Connect += server_Connect;
                            server.Disconnect += server_Disconnect;
                            server.DBRespuesta += server_DBRespuesta;

                            var msg = new MsgConexion()
                            {
                                From = guid.ToString(),
                                Mensaje = "conectar",
                                Fecha = DateTime.Now

                            };
                            msg.To.Add(guid.ToString());

                            server.SendToServer(msg);

                        }
                        catch (Exception) { }
                    }));

                    tar.Start();

                    
                }

                Thread.Sleep(5000);
            }
        }

        void server_DBRespuesta(object sender, Mensaje mensaje)
        {
            var msg = mensaje as MsgDBRespuesta;
            if (msg.CodigoPeticion.Equals("Login"))
            {
                if (msg.IsValido)
                {
                    sesion = new Sesion()
                    {
                        Usuario = msg.ReturnCuenta[0],
                        Server = server,
                    };
                    cancelar.Cancel();
                    isConected.Join();

                    tbUsuario.Invoke(DelMostrarForm);
                }
                else
                {
                    MessageBox.Show("Login incorrecto");
                }
            }
            else if(msg.CodigoPeticion.Equals("CrearCuenta"))
            {
                if (msg.IsValido)
                {
                    MessageBox.Show("Se creo correctamente");
                }
                else
                {
                    MessageBox.Show("No se creo la cuenta");
                }
            }
        }


        void MostrarForm()
        {
            sesion.Server.Connect -= server_Connect;
            sesion.Server.Disconnect -= server_Disconnect;
            sesion.Server.DBRespuesta -= server_DBRespuesta;
            frm = new frmPrincipal(sesion);
            frm.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancelar.Cancel();
            isConected.Join();

        }

    }
}
