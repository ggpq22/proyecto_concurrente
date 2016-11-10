using SistemaTrackingBiblioteca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class MsgDBRespuesta : Mensaje
    {
        public MsgDBRespuesta()
        {
            base.Tipo = this.GetType().Name;
        }

        /// <summary>
        /// Codigos:
        /// <para>- "Login" = return: Cuenta</para>
        /// <para>- "CrearCuenta" = return: Cuenta</para>
        /// <para>- "BorrarCuenta" = return: void</para>
        /// <para>- "CrearGrupo" = return: Grupo</para>
        /// <para>- "GetGrupo" = return: Grupo</para>
        /// <para>- "GetGrupoPorAnfitrion" = return: Grupo(s)</para>
        /// <para>- "GetGrupoPorIntegrante" = return: Grupo(s)</para>
        /// <para>- "AgregarCuentaAGrupo" = return: Grupo</para>
        /// <para>- "BorrarCuentaDeGrupo" = return: Grupo</para>
        /// <para>- "BorrarGrupo" = return: void</para>
        /// <para>- "GetHistorialPorGrupo" = return: Historial(s)</para>
        /// <para>- "GetHistorialPorCuenta" = return: Historial(s)</para>
        /// <para>- "GetCuentas" = return: Cuenta(s)</para>
        /// </summary>
        public string CodigoPeticion { get; set; }

        public List<Cuenta> ReturnCuenta = new List<Cuenta>();

        public List<Grupo> ReturnGrupo = new List<Grupo>();

        public List<Historial> ReturnHistorial = new List<Historial>();
    }
}
