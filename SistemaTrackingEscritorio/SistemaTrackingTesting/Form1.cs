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
            MsgConexion con = new MsgConexion();
            con.Fecha = DateTime.Now;
            con.From = tbFrom.Text;
            con.To.Add(tbTo.Text);
            con.Mensaje = "conectar";
            client.SendToServer(con);
        }

        void client_Connect(object sender, Mensaje mensaje)
        {
            listBox1.BeginInvoke((Delegate)(new Action(()=>{listBox1.Items.Add(mensaje.ToString());})));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MsgLocalizacion m = new MsgLocalizacion();
            //m.Latitud = "3.3837398745";
            //m.Longitud = "3.334983470";
            
            MsgDBPeticion m = new MsgDBPeticion();
            m.CodigoPeticion = "AgregarCuentaAGrupo";
            m.ParamsCuenta.Add(new Cuenta() { Id = 36 });
            m.ParamsGrupo.Add((new Grupo() { Id = 34 }));
            /*m.ParamsGrupo.Add((new Grupo() { Id = 2 }));
            m.ParamsGrupo.Add((new Grupo() { Id = 3 }));
            m.ParamsGrupo.Add((new Grupo() { Id = 4 }));*/

            m.From = tbFrom.Text;
            m.To.Add(tbTo.Text);
            m.Fecha = DateTime.Now;
            client.SendToServer(m);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MsgDBPeticion m = new MsgDBPeticion();
            m.CodigoPeticion = "Login";
            m.ParamsCuenta.Add(new Cuenta() { Usuario = "pablo", Pass = "pablo" });

            m.From = tbFrom.Text;
            m.To.Add(tbTo.Text);
            m.Fecha = DateTime.Now;
            client.SendToServer(m);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MsgDBPeticion m = new MsgDBPeticion();
            m.CodigoPeticion = "CrearCuenta";
            m.ParamsCuenta.Add(new Cuenta() { Usuario = "pablo", Pass = "pablo", RecibeLocalizacion = 1 });

            m.From = tbFrom.Text;
            m.To.Add(tbTo.Text);
            m.Fecha = DateTime.Now;
            client.SendToServer(m);
        }
    }
}
