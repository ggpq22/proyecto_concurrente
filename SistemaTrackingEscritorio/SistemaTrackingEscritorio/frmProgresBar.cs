using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapa
{
    public partial class frmProgresBar : Form
    {
        public CancellationTokenSource token { get; set; }

        public Thread tarea { get; set; }
        public frmProgresBar()
        {
            InitializeComponent();
            pbProgreso.Step = 1;

        }

        private void frmProgresBar_Load(object sender, EventArgs e)
        {
            token = new CancellationTokenSource();
            ParameterizedThreadStart p = (object o)=> {
                var t = (CancellationToken) o;
                int contador = 0;
                while (t.IsCancellationRequested)
                {
                    pbProgreso.Invoke(new Action(() =>
                    {
                        contador = contador == 100 ? 0 : 10 + contador;

                        pbProgreso.Value = contador;
                    }));
                    Thread.Sleep(500);
                }


            };
            
            tarea = new Thread(p);
            tarea.Start(token.Token);
        }
    }
}
