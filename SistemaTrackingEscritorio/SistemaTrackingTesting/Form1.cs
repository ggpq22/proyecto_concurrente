using SistemaTrackingBiblioteca;
using SistemaTrackingBiblioteca.Mensajes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaTrackingTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ServerClient client;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new ServerClient(tbIp.Text, Convert.ToInt32(tbPort.Text));
            client.Connect += client_Connect;
        }

        void client_Connect(object sender, Mensaje mensaje)
        {
            listBox1.BeginInvoke((Delegate)(new Action(()=>{listBox1.Items.Add(mensaje.ToString());})));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MsgLocalizacion m = new MsgLocalizacion();
            m.From = tbFrom.Text;
            m.To.Add(tbTo.Text);
            m.Latitud = "3.3";
            m.Longitud = "3.3";

            client.SendToServer(m);
        }
    }
}
