using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServidorTracking;

namespace Mapa
{
    static class Program
    {


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var ip = Configuracion.App.Configuracion.GetConfiguracion("IpServidor");
            //var pueto = Configuracion.App.Configuracion.GetConfiguracion("PuertoConfiguracion");
            //ServerClient servidor = new ServerClient(ip, int.Parse(pueto));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

    }
}
