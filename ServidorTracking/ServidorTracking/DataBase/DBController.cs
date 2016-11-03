using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Entidades;
using System.Data;

namespace SistemaTrackingBiblioteca.DataBase
{
    class DBController
    {
        DBManager dbMan;

        public DBController(string usuario, string password)
        {
            dbMan = new DBManager(usuario, password);
        }

        public Cuenta Login(Cuenta cuenta)
        {
            DataTable data;
            Cuenta c;

            data = dbMan.search("select * from cuenta where state = 1 and usuario = " + cuenta.Usuario + " and pass = " + cuenta.Pass);

            if (data.Rows.Count < 1)
            {
                throw new Exception("Su combinacion de Usuario y Contraseña no existen.");
            }
            else
            {
                c = GetCuenta(Convert.ToInt32(data.Rows[0]["idCuenta"]));
                return c;
            }
        }

        public Cuenta CreateCuenta(Cuenta cuenta)
        {
            DataTable data;
            data = dbMan.search("select * from cuenta where state = 1 and usuario = " + cuenta.Usuario);

            if (data.Rows.Count < 1)
            {
                throw new Exception("Ese usuario ya existe.");
            }
            else
            {
                int id;
                id = dbMan.execute("insert into cuenta(usuario, pass, state) values(" + cuenta.Usuario + ", " + cuenta.Pass + ", 1)", QueryType.INSERT);

                Cuenta c = GetCuenta(id);

                return c;
            }
        }

        public void DeleteCuenta(int idCuenta)
        {
            dbMan.execute("update cuenta set state = 0 where idCuenta = " + idCuenta, QueryType.UPDATE);
        }

        public Cuenta GetCuenta(int idCuenta)
        {
            DataTable data;

            data = dbMan.search("select * from cuenta where state = 1 and idCuenta = " + idCuenta);

            Cuenta c = new Cuenta();

            foreach (DataRow dt in data.Rows)
            {
                c.Id = Convert.ToInt32(dt["idCuenta"]);
                c.Usuario = Convert.ToString(dt["usuario"]);
                c.Pass = Convert.ToString(dt["pass"]);
            }

            return c;
        }

        public Grupo CreateGrupo(Grupo grupo)
        {
            int id = dbMan.execute("insert into Grupo(idAnfitrion, state) values(" + grupo.Anfitrion.Id + ", 1)", QueryType.INSERT);

            foreach (Cuenta c in grupo.Integrantes)
            {
                AddCuentaToGrupo(c.Id, id);
            }

            Grupo g = GetGrupo(id);

            return g;
        }

        public Grupo GetGrupo(int idGrupo)
        {
            DataTable dataGrupo, dataIntegrantes;

            dataGrupo = dbMan.search("select * from grupo where state = 1 and idGrupo = " + idGrupo);
            dataIntegrantes = dbMan.search("select * from grupo_cuentas where state = 1 and idGrupo = " + idGrupo);

            Grupo g = new Grupo();

            foreach (DataRow dt in dataGrupo.Rows)
            {
                g.Id = Convert.ToInt32(dt["idGrupo"]);
                g.Anfitrion = GetCuenta(Convert.ToInt32(dataGrupo.Rows[1]["idAnfitrion"]));
            }

            List<Cuenta> c = new List<Cuenta>();

            foreach (DataRow dt in dataIntegrantes.Rows)
            {
                c.Add(GetCuenta(Convert.ToInt32(dt["idCuenta"])));
            }

            g.Integrantes = c;

            return g;
        }

        public List<Grupo> GetGrupoByAnfitrion(int idAnfitrion)
        {
            DataTable dataGrupo;

            dataGrupo = dbMan.search("select * from grupo where state = 1 and idAnfitrion = " + idAnfitrion);

            List<Grupo> g = new List<Grupo>();

            foreach (DataRow dr in dataGrupo.Rows)
            {
                g.Add(GetGrupo(Convert.ToInt32(dr["idGrupo"])));
            }

            return g;
        }

        public List<Grupo> GetGrupoByIntegrante(int idIntegrante)
        {
            DataTable dataGrupo;

            dataGrupo = dbMan.search("select * from grupo_cuentas where state = 1 and idCuenta = " + idIntegrante);

            List<Grupo> g = new List<Grupo>();

            foreach (DataRow dr in dataGrupo.Rows)
            {
                g.Add(GetGrupo(Convert.ToInt32(dr["idGrupo"])));
            }

            return g;
        }

        public Grupo AddCuentaToGrupo(int idIntegrante, int idGrupo)
        {
            int id = dbMan.execute("insert into grupo_cuentas(idGrupo, idCuenta, state) values(" + idGrupo + ", " + idIntegrante + ", 1)", QueryType.INSERT);

            Grupo g = GetGrupo(id);

            return g;
        }
        
        public Grupo DeleteCuentaFromGrupo(int idIntegrante, int idGrupo)
        {
            dbMan.execute("delete from grupo_cuentas where idGrupo = " + idGrupo + " and idCuenta = " + idIntegrante, QueryType.UPDATE);

            Grupo grupo = GetGrupo(idGrupo);

            return grupo;
        }

        public void DeleteGrupo(int idGrupo)
        {
            dbMan.execute("delete from grupo where idGrupo = " + idGrupo, QueryType.UPDATE);
        }

        public List<Historial> GetHistorialByGrupo(int idGrupo)
        {
            DataTable data;

            data = dbMan.search("select * from historial where state = 1 and idGrupo = " + idGrupo);

            List<Historial> list = new List<Historial>();

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Historial());
                list[list.Count - 1].Id = Convert.ToInt32(row["idHistorial"]);
                list[list.Count - 1].Grupo = GetGrupo(Convert.ToInt32(row["idGrupo"]));
                list[list.Count - 1].Fecha = Convert.ToDateTime(row["fecha"]);
                list[list.Count - 1].Cuenta = GetCuenta(Convert.ToInt32(row["idCuenta"]));
                list[list.Count - 1].Lat = Convert.ToInt32(row["lat"]);
                list[list.Count - 1].Long = Convert.ToInt32(row["long"]);
            }

            return list;
        }

        public List<Historial> GetHistorialByCuenta(int idCuenta)
        {
            DataTable data;

            data = dbMan.search("select * from historial where state = 1 and idCuenta = " + idCuenta);

            List<Historial> list = new List<Historial>();

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Historial());
                list[list.Count - 1].Id = Convert.ToInt32(row["idHistorial"]);
                list[list.Count - 1].Grupo = GetGrupo(Convert.ToInt32(row["idGrupo"]));
                list[list.Count - 1].Fecha = Convert.ToDateTime(row["fecha"]);
                list[list.Count - 1].Cuenta = GetCuenta(Convert.ToInt32(row["idCuenta"]));
                list[list.Count - 1].Lat = Convert.ToInt32(row["lat"]);
                list[list.Count - 1].Long = Convert.ToInt32(row["long"]);
            }

            return list;
        }

        public void CreateHistorial(Historial entry)
        {
            dbMan.execute("insert into historial(idGrupo, idCuenta, fecha, lat, long, state) values(" + entry.Grupo.Id + ", " + entry.Cuenta.Id + ", "+entry.Fecha+", "+entry.Lat+", "+entry.Long+", 1)", QueryType.INSERT);
        }
    }
}
