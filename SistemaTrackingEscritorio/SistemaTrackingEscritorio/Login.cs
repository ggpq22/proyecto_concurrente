using ServidorTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Configuracion.App;

namespace Mapa
{
    public partial class Login : Form
    {
        ServerClient server = null;
        
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblNuevaCuenta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ConectarServidor();
        }


        private void ConectarServidor()
        {
            if (server == null)
            {
                return;
            }
            var ip = Configuracion.App.Configuracion.GetConfiguracion("IpServidor");
            var puerto = Configuracion.App.Configuracion.GetConfiguracion("PuertoServidor");


            server = new ServerClient(ip, int.Parse(puerto));

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ConectarServidor();
        }
    }
}
