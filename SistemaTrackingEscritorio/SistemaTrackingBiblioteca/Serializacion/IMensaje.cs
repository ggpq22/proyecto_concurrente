using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Serializacion
{
    public interface IMensaje
    {
        string From { get; set; }
        string To { get; set; }
        DateTime fecha { get; set; }
    }
}
