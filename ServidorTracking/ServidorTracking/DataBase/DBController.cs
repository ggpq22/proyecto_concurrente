using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Entidades;
using System.Data;

namespace ServidorTracking.DataBase
{
    class DBController
    {
        DBManager dbMan = new DBManager("pablo", "pablo");

        public Cuenta CreateCuenta(Cuenta cuenta)
        {
            DataTable data;
            data = dbMan.search("select * from cuenta where usuario = " + cuenta.Usuario);

            if (data.Rows.Count < 1)
            {
                throw new Exception("Ese usuario ya existe.");
            }
            else
            {
                int id;
                id = dbMan.execute("insert into cuenta(usuario, pass, state) values(" + cuenta.Usuario + ", " + cuenta.pass + ", 1)", QueryType.ID);
                data = dbMan.search("select * from cuenta where idCuenta = " + id);

                Cuenta c = new Cuenta();

                foreach(DataRow dt in data.Rows)
                {
                    c.IdCuenta = Convert.ToInt32(dt["idCuenta"]);
                    c.Usuario = Convert.ToString(dt["usuario"]);
                    c.pass = Convert.ToString(dt["pass"]);
                }

                return c;
            }
        }

        public void DeleteCuenta(int idCuenta)
        {
            dbMan.execute("update cuenta set state = 0 where idCuenta = " + idCuenta, QueryType.ID);
        }

        public Grupo CreateGrupo(Grupo grupo)
        {
            DataTable dataGrupo;
            DataTable dataAnfitrion;

            int id = dbMan.execute("insert into Grupo(idAnfitrion, state) values(" + grupo.Anfitrion.IdCuenta + ", 1)", QueryType.ID);

            dataGrupo = dbMan.search("select * from Grupo where idGrupo = " + id);
            dataAnfitrion = dbMan.search("select * from Cuenta where idCuenta = " + Convert.ToInt32(dataGrupo.Rows[1]["idAnfitrion"]));

            Grupo g = new Grupo();

            foreach (DataRow dt in dataGrupo.Rows)
            {
                g.IdGrupo = Convert.ToInt32(dt["idGrupo"]);
                g.Anfitrion.IdCuenta = Convert.ToInt32(dataAnfitrion.Rows[1]["idCuenta"]);
                g.Anfitrion.Usuario = Convert.ToString(dataAnfitrion.Rows[1]["usuario"]);
                g.Anfitrion.pass = Convert.ToString(dataAnfitrion.Rows[1]["pass"]);
            }

            return g;
        }

        public Grupo GetGrupo(int idGrupo)
        {
            DataTable dataGrupo, dataAnfitrion, dataIntegrantes;

            dataGrupo = dbMan.search("select * from grupo where idGrupo = " + idGrupo);
            dataAnfitrion = dbMan.search("select * from cuenta where idCuenta = " + Convert.ToInt32(dataGrupo.Rows[1]["idAnfitrion"]));
            dataIntegrantes = dbMan.search("select * from grupo_cuentas where idGrupo = " + idGrupo);

            Grupo g = new Grupo();

            foreach (DataRow dt in dataGrupo.Rows)
            {
                g.IdGrupo = Convert.ToInt32(dt["idGrupo"]);
                g.Anfitrion.IdCuenta = Convert.ToInt32(dataAnfitrion.Rows[1]["idCuenta"]);
                g.Anfitrion.Usuario = Convert.ToString(dataAnfitrion.Rows[1]["usuario"]);
                g.Anfitrion.pass = Convert.ToString(dataAnfitrion.Rows[1]["pass"]);
            }

            List<Cuenta> c = new List<Cuenta>();
            int i = 0;

            foreach (DataRow dt in dataIntegrantes.Rows)
            {
                c.Add(new Cuenta());
                c[i].IdCuenta = Convert.ToInt32(dt["idCuenta"]);
                c[i].Usuario = Convert.ToString(dt["usuario"]);
                c[i].pass = Convert.ToString(dt["pass"]);
            }

            g.Integrantes = c.ToArray();

            return g;
        }

        public Grupo[] GetGrupoByAnfitrion(int idAnfitrion);

        public Grupo AddCuentaToGrupo(int idIntegrante, int idGrupo);

        public Grupo DeleteCuentaFromGrupo(int idIntegrante);

        public Grupo DeleteGrupo(int idGrupo);

        public Historial[] GetHistorialByGrupo(int idGrupo);

        public Historial[] GetHistorialByCuenta(int idCuenta);

        public void CreateHistorial(Historial entry);
    }
}
