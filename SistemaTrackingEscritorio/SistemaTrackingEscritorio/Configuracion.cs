using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace Configuracion.App
{
    static class Configuracion
    {
        public static string GetConfiguracion(string nombre)
        {
            return  ConfigurationManager.AppSettings[nombre];
        }
    }
}
