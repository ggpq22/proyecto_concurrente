using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTrackingBiblioteca.Entidades;
using System.Data;
using System.Collections.Concurrent;

namespace ServidorTracking.DataBase
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

            try
            {
                data = dbMan.search("select * from cuenta where state = 1 and usuario = '" + cuenta.Usuario + "' and pass = '" + cuenta.Pass + "'");

                if (data.Rows.Count < 1)
                {
                    throw new Exception("Su combinacion de Usuario y Contraseña no existen.");
                }
                else
                {
                    c = GetCuentaById(Convert.ToInt32(data.Rows[0]["idCuenta"]));
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return c;
        }

        public Cuenta CreateCuenta(Cuenta cuenta)
        {
            DataTable data;
            Cuenta c;
            int id;

            try
            {
                data = dbMan.search("select * from cuenta where state = 1 and usuario = '" + cuenta.Usuario + "'");
            
                if (data.Rows.Count > 0)
                {
                    throw new Exception("Ese usuario ya existe.");
                }
                else
                {
                    id = dbMan.execute("insert into cuenta(usuario, pass, recibeLocalizaciones, state) values('" + cuenta.Usuario + "', '" + cuenta.Pass + "', " + cuenta.RecibeLocalizacion + ", 1)", QueryType.INSERT);

                    c = GetCuentaById(id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return c;
        }

        public void DeleteCuenta(string usuario)
        {
            try
            {
                dbMan.execute("update cuenta set state = 0 where usuario = '" + usuario + "'", QueryType.UPDATE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cuenta GetCuentaById(int idCuenta)
        {
            DataTable data;
            Cuenta c = new Cuenta();

            try
            {
                data = dbMan.search("select * from cuenta where state = 1 and idCuenta = " + idCuenta);
            
                foreach (DataRow dt in data.Rows)
                {
                    c.Id = Convert.ToInt32(dt["idCuenta"]);
                    c.Usuario = Convert.ToString(dt["usuario"]);
                    c.Pass = Convert.ToString(dt["pass"]);
                    c.RecibeLocalizacion = Convert.ToInt32(dt["recibeLocalizaciones"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
                
            return c;
        }

        public Cuenta GetCuentaByUsuario(string nombreUsuario)
        {
            DataTable data;
            Cuenta c = new Cuenta();

            try
            {
                data = dbMan.search("select * from cuenta where state = 1 and usuario = '" + nombreUsuario + "'");
            
                foreach (DataRow dt in data.Rows)
                {
                    c.Id = Convert.ToInt32(dt["idCuenta"]);
                    c.Usuario = Convert.ToString(dt["usuario"]);
                    c.Pass = Convert.ToString(dt["pass"]);
                    c.RecibeLocalizacion = Convert.ToInt32(dt["recibeLocalizaciones"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return c;
        }

        public Grupo CreateGrupo(Grupo grupo)
        {
            DataTable data;
            Grupo g;
            int id;

            try
            {
                data = dbMan.search("select * from grupo where state = 1 and nombre = '" + grupo.Nombre + "'");

                if (data.Rows.Count > 0)
                {
                    throw new Exception("Ese grupo ya existe.");
                }
                else
                {
                    id = dbMan.execute("insert into grupo(nombre, idAnfitrion, state) values('" + grupo.Nombre + "', " + grupo.Anfitrion.Id + ", 1)", QueryType.INSERT);

                    foreach (Cuenta c in grupo.Integrantes)
                    {
                        AddCuentaToGrupo(c.Id, id);
                    }

                    g = GetGrupoById(id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return g;
        }

        public Grupo GetGrupoById(int idGrupo)
        {
            DataTable dataGrupo, dataIntegrantes;
            Grupo g = new Grupo();
            List<Cuenta> c = new List<Cuenta>();

            try
            {
                dataGrupo = dbMan.search("select * from grupo where state = 1 and idGrupo = " + idGrupo);
                dataIntegrantes = dbMan.search("select * from grupo_cuentas where state = 1 and idGrupo = " + idGrupo);
            
                foreach (DataRow dt in dataGrupo.Rows)
                {
                    g.Id = Convert.ToInt32(dt["idGrupo"]);
                    g.Nombre = Convert.ToString(dt["nombre"]);
                    g.Anfitrion = GetCuentaById(Convert.ToInt32(dataGrupo.Rows[0]["idAnfitrion"]));
                }
            
                foreach (DataRow dt in dataIntegrantes.Rows)
                {
                    c.Add(GetCuentaById(Convert.ToInt32(dt["idCuenta"])));
                }

                g.Integrantes = c;
            }
            catch (Exception e)
            {
                throw e;
            }

            return g;
        }

        public Grupo GetGrupoByNombre(string nombreGrupo)
        {
            DataTable dataGrupo, dataIntegrantes;
            Grupo g = new Grupo();
            List<Cuenta> c = new List<Cuenta>();

            try
            {
                dataGrupo = dbMan.search("select * from grupo where state = 1 and nombre = '" + nombreGrupo + "'");
                dataIntegrantes = dbMan.search("select * from grupo_cuentas where state = 1 and idGrupo = " + Convert.ToString(dataGrupo.Rows[0]["idGrupo"]));
            
                foreach (DataRow dt in dataGrupo.Rows)
                {
                    g.Id = Convert.ToInt32(dt["idGrupo"]);
                    g.Nombre = Convert.ToString(dt["nombre"]);
                    g.Anfitrion = GetCuentaById(Convert.ToInt32(dataGrupo.Rows[0]["idAnfitrion"]));
                }
            
                foreach (DataRow dt in dataIntegrantes.Rows)
                {
                    c.Add(GetCuentaById(Convert.ToInt32(dt["idCuenta"])));
                }

                g.Integrantes = c;
            }
            catch (Exception e)
            {
                throw e;
            }

            return g;
        }

        public List<Grupo> GetGrupoByAnfitrion(int idAnfitrion)
        {
            DataTable dataGrupo;
            List<Grupo> g = new List<Grupo>();

            try
            {
                dataGrupo = dbMan.search("select * from grupo where state = 1 and idAnfitrion = " + idAnfitrion);

                foreach (DataRow dr in dataGrupo.Rows)
                {
                    g.Add(GetGrupoById(Convert.ToInt32(dr["idGrupo"])));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return g;
        }

        public List<Grupo> GetGrupoByIntegrante(int idIntegrante)
        {
            DataTable dataGrupo;
            List<Grupo> g = new List<Grupo>();

            try
            {
                dataGrupo = dbMan.search("select * from grupo_cuentas where state = 1 and idCuenta = " + idIntegrante);

                foreach (DataRow dr in dataGrupo.Rows)
                {
                    g.Add(GetGrupoById(Convert.ToInt32(dr["idGrupo"])));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return g;
        }

        public Grupo AddCuentaToGrupo(int idIntegrante, int idGrupo)
        {
            int id;
            Grupo g;

            try
            {
                dbMan.execute("insert into grupo_cuentas(idGrupo, idCuenta, state) values(" + idGrupo + ", " + idIntegrante + ", 1)", QueryType.UPDATE);
            
                g = GetGrupoById(idGrupo);
            }
            catch (Exception e)
            {
                throw e;
            }
                
            return g;
        }
        
        public Grupo DeleteCuentaFromGrupo(int idIntegrante, int idGrupo)
        {
            Grupo grupo;

            try
            {
                dbMan.execute("update grupo_cuentas set state = 0 where idGrupo = " + idGrupo + " and idCuenta = " + idIntegrante, QueryType.UPDATE);

                grupo = GetGrupoById(idGrupo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return grupo;
        }

        public void DeleteGrupo(int idGrupo)
        {
            try
            {
                dbMan.execute("update grupo set state = 0 where idGrupo = " + idGrupo, QueryType.UPDATE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Historial> GetHistorialByGrupo(int idGrupo)
        {
            DataTable data;
            List<Historial> list = new List<Historial>();

            try
            {
                data = dbMan.search("select * from historial where state = 1 and idGrupo = " + idGrupo);

                foreach (DataRow row in data.Rows)
                {
                    list.Add(new Historial());
                    list[list.Count - 1].Id = Convert.ToInt32(row["idHistorial"]);
                    list[list.Count - 1].Grupo = GetGrupoById(Convert.ToInt32(row["idGrupo"]));
                    list[list.Count - 1].Fecha = Convert.ToDateTime(row["fecha"]);
                    list[list.Count - 1].Cuenta = GetCuentaById(Convert.ToInt32(row["idCuenta"]));
                    list[list.Count - 1].Lat = Convert.ToInt32(row["lat"]);
                    list[list.Count - 1].Long = Convert.ToInt32(row["long"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<Historial> GetHistorialByCuenta(int idCuenta)
        {
            DataTable data;
            List<Historial> list = new List<Historial>();

            try
            {
                data = dbMan.search("select * from historial where state = 1 and idCuenta = " + idCuenta);

                foreach (DataRow row in data.Rows)
                {
                    list.Add(new Historial());
                    list[list.Count - 1].Id = Convert.ToInt32(row["idHistorial"]);
                    list[list.Count - 1].Grupo = GetGrupoById(Convert.ToInt32(row["idGrupo"]));
                    list[list.Count - 1].Fecha = Convert.ToDateTime(row["fecha"]);
                    list[list.Count - 1].Cuenta = GetCuentaById(Convert.ToInt32(row["idCuenta"]));
                    list[list.Count - 1].Lat = Convert.ToInt32(row["lat"]);
                    list[list.Count - 1].Long = Convert.ToInt32(row["long"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public void CreateHistorial(Historial entry)
        {
            string query;
            try
            {
                query = "insert into historial(idGrupo, idCuenta, fecha, lat, long, state) values(" + entry.Grupo.Id + ", " + entry.Cuenta.Id + ", '" + entry.Fecha.ToString("s") + "', " + entry.Lat.ToString().Replace(',', '.') + ", " + entry.Long.ToString().Replace(',', '.') + ", 1)";
                dbMan.execute(query, QueryType.INSERT);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Grupo> GetAllGrupos()
        {
            DataTable data;
            List<Grupo> gl = new List<Grupo>();

            try
            {
                data = dbMan.search("select idGrupo from grupo");

                foreach (DataRow dt in data.Rows)
                {
                    gl.Add(GetGrupoById(Convert.ToInt32(dt["idGrupo"])));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return gl;
        }

        public List<Cuenta> GetAllCuentas()
        {
            DataTable data;
            List<Cuenta> cl = new List<Cuenta>();

            try
            {
                data = dbMan.search("select idCuenta from cuenta");

                foreach (DataRow dt in data.Rows)
                {
                    cl.Add(GetCuentaById(Convert.ToInt32(dt["idCuenta"])));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return cl;
        }
    }
}
