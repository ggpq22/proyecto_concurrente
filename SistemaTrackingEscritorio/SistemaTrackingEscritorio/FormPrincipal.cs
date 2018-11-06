using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapa
{
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            this.BackgroundImage = global::Mapa.Properties.Resources.images__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
