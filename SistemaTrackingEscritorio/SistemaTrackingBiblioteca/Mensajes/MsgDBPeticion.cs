using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SistemaTrackingBiblioteca.Entidades;

namespace SistemaTrackingBiblioteca.Mensajes
{
    public class MsgDBPeticion : Mensaje
    {
        public MsgDBPeticion()
        {
            base.Tipo = this.GetType().Name;
        }

        /// <summary>
        /// Codigos:
        /// <para>- "Login" = params: Cuenta</para>
        /// <para>- "CrearCuenta" = params: Cuenta</para>
        /// <para>- "BorrarCuenta" = params: Cuenta</para>
        /// <para>- "CrearGrupo" = params: Grupo</para>
        /// <para>- "GetGrupo" = params: Grupo</para>
        /// <para>- "GetGrupoPorAnfitrion" = params: Cuenta</para>
        /// <para>- "GetGrupoPorIntegrante" = params: Cuenta</para>
        /// <para>- "AgregarCuentaAGrupo" = params: Cuenta, Grupo</para>
        /// <para>- "BorrarCuentaDeGrupo" = params: Cuenta, Grupo</para>
        /// <para>- "BorrarGrupo" = params: Grupo</para>
        /// <para>- "GetHistorialPorGrupo" = params: Grupo</para>
        /// <para>- "GetHistorialPorCuenta" = params: Cuenta</para>
        /// <para>- "GetCuentas" = params:</para>
        /// </summary>
        public string CodigoPeticion { get; set; }

        public List<DBEntidad> Params = new List<DBEntidad>();

        /// <summary>
        /// True = Envia un mensaje de notificacion al destinatario para que confirme la operacion
        /// <para>False = La operacion se lleva a cabo sin confirmacion</para>
        /// </summary>
        public bool Notificacion = false;
    }
}
