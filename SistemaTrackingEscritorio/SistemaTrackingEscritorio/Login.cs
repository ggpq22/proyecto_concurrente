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
    public partial class Login : Form
    {
        ServerClient server = null;
        bool conectado;
        Guid guid;


        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblNuevaCuenta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var respuesta = ConectarServidor();

            if (!respuesta)
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
            msg.Params.Add(cuenta);

            server.SendToServer(msg);

        }

        private bool ConectarServidor()
        {
            if (server != null)
            {
                return true;
            }
            var ip = Configuracion.GetConfiguracion("IpServidor");
            var puerto = Configuracion.GetConfiguracion("PuertoServidor");

            try
            {

                server = new ServerClient(ip,int.Parse(puerto));
            }
            catch (Exception)
            {
                return false;
            }

            server.Connect += server_Connect;
            server.Disconnect += server_Disconnect;

            var msg = new MsgConexion()
            {
                From = guid.ToString(),
                Mensaje = "conectar",
                Fecha = DateTime.Now,

            };

            msg.To.Add(guid.ToString());

            server.SendToServer(msg);

            return true;

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
            msg.Params.Add(cuenta);

            server.SendToServer(msg);

        }

        private void Login_Load(object sender, EventArgs e)
        {
            guid = System.Guid.NewGuid();


            #region conectar
            Thread isConectado = new Thread(() =>
            {
                

                while (true)
                {
                    if (!conectado)
                    {
                        new Task(new Action(() =>{
                            try
                            {
                                var ip = Configuracion.GetConfiguracion("IpServidor");
                                var puerto = Configuracion.GetConfiguracion("PuertoServidor");
                                var retorno = new ServerClient(ip, int.Parse(puerto));
                                server = retorno;
                                conectado = true;
                                server.Connect +=server_Connect;
                                server.Disconnect +=server_Disconnect;

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
                        })).Start();
                    }

                    Thread.Sleep(5000);
                }
            });

            isConectado.Start();

            #endregion conectar


        }
    }
}
