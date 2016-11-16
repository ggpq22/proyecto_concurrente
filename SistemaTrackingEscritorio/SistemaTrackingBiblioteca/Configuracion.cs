using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace SistemaTrackingBiblioteca
{
    public static class Configuracion
    {
        public static string GetConfiguracion(string nombre)
        {
            return  ConfigurationManager.AppSettings[nombre];
        }

        public static void GrabarEnDisco(Exception ex)
        {
            var path = ConfigurationManager.AppSettings["DirectorioLog"];
            path = String.Format("{0}log{1}.txt",path,DateTime.Now.ToString("ddMMyyyyhhmm"));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(ex.Message);
            }
        }

        public static string GetConnectionString(string nombre){

            return ConfigurationManager.ConnectionStrings[nombre].ConnectionString;

        }
    }
}
