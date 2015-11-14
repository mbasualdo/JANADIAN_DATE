using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AerolineaFrba.Excepciones;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AerolineaFrba
{
    public sealed class JanadianDateDB
    {
        private static readonly JanadianDateDB instance = new JanadianDateDB();
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GD2C2015"].ConnectionString);
        private readonly DateTime fechaSistema = Convert.ToDateTime(ConfigurationManager.AppSettings["DefaultDate"]);
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static JanadianDateDB()
        {
        }

        private JanadianDateDB()
        {
        }

        public static JanadianDateDB Instance
        {
            get
            {
                return instance;
            }
        }

        public SqlConnection GetDBConnection()
        {
            return con;
        }

        public DateTime getFechaSistema()
        {
            return this.fechaSistema;
        }

        internal Usuario getUsuario(string username, string password)
        {
            //this.fechaSistema.ToString();
            Usuario user = null;
            try{
            string hash = GetSha256FromString(password);
            //
            // Open the SqlConnection.
            //
            con.Open();
            //
            // The following code uses an SqlCommand based on the SqlConnection.
            //
            SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 u.Id,u.Nombre as UsuarioNombre,u.Password,u.Intentos,u.Habilitado,r.Nombre as RolNombre FROM [GD2C2015].[JANADIAN_DATE].[Usuario] u INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol] r ON (u.Rol=r.Id) WHERE u.Nombre = '{0}' and r.Nombre LIKE '%Admin%'  ", username), con);
            DataTable dt = new DataTable();

            dt.TableName = "Tabla";
            dt.Load(cmd.ExecuteReader());
            if (dt.Rows.Count == 0)
            {
                con.Close();
                throw (new NoResultsException("Usuario incorrecto"));
            }
            foreach (DataRow Fila in dt.Rows)
            {
                if (!Convert.ToBoolean(Fila["Habilitado"]))
                {
                    con.Close();
                    throw (new UnavailableException("Usuario no esta habilitado"));
                }
                else if (!Convert.ToString(Fila["Password"]).Equals(hash))
                {
                    SqlCommand update = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Usuario] SET Intentos = {0} WHERE Nombre = '{1}'", Convert.ToInt32(Fila["Intentos"]) + 1, username), con);
                    update.ExecuteNonQuery();
                    con.Close();
                    throw (new PasswordMismatchException("Contraseña incorrecta"));
                }
                else
                {
                    SqlCommand update = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Usuario] SET Intentos = {0} WHERE Nombre = '{1}'", 0, username), con);
                    update.ExecuteNonQuery();
                    user = new Usuario(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["UsuarioNombre"]), Convert.ToInt32(Fila["Intentos"]), Convert.ToString(Fila["RolNombre"]));
                }


            }
            con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }

            return user;
        }

        public static string GetSha256FromString(string strData)
        {
            var message = Encoding.ASCII.GetBytes(strData);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        internal List<string> getFuncionalidadesAdmin()
        {
            List<String> funcionalidades = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT f.Descripcion FROM [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf ON (rf.Funcionalidad=f.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol] r ON (rf.Rol=r.Id) WHERE r.Nombre LIKE '%Admin%'  "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Rol"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    funcionalidades.Add(Convert.ToString(Fila["Descripcion"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return funcionalidades;
        }

        internal List<string> getFuncionalidadesInvitado()
        {
            List<String> funcionalidades = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT f.Descripcion FROM [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf ON (rf.Funcionalidad=f.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol] r ON (rf.Rol=r.Id) WHERE r.Nombre NOT LIKE '%Admin%'  "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Rol"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    funcionalidades.Add(Convert.ToString(Fila["Descripcion"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return funcionalidades;
        }
        internal DataTable getDataTableResults(DataGridView d, String query)
        {
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(String.Format(query), con);

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return dt;
        }

        internal List<string> getFuncionalidades()
        {
            List<String> funcionalidades = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT f.Descripcion FROM [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Funcionalidades"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    funcionalidades.Add(Convert.ToString(Fila["Descripcion"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return funcionalidades;
        }

        internal Rol getRolByname(string nombreRol)
        {
            Rol rol = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 Id,Nombre,Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Rol] r WHERE r.Nombre = '{0}'  ", nombreRol), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return null;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    rol = new Rol(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Nombre"]), Convert.ToBoolean(Fila["Habilitado"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return rol;
        }

        internal void insertarRol(string p, List<string> list)
        {
            try
            {
                con.Open();
                SqlCommand insertRol = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Rol] (Nombre) VALUES ('{0}')", p), con);
                insertRol.ExecuteNonQuery();

                SqlCommand insertRol_Func = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] (Rol,Funcionalidad) select r.Id,f.Id from  [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f ,  [GD2C2015].[JANADIAN_DATE].[Rol] r  where f.Descripcion in ('{0}') and r.Nombre='{1}'", string.Join("','", list), p), con);
                insertRol_Func.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }

        }

        internal List<String> getFuncionalidadesByRol(int id)
        {
            List<String> funcionalidades = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT f.Descripcion FROM [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f,[GD2C2015].[JANADIAN_DATE].[Rol] r,[GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf WHERE r.Id=rf.Rol AND f.Id=rf.Funcionalidad and r.Id={0}", id), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Rol"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    funcionalidades.Add(Convert.ToString(Fila["Descripcion"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return funcionalidades;
        }

        internal void bajaLogicaRol(int idRol)
        {
            try
            {
                con.Open();
                SqlCommand updateRol = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Rol] SET Habilitado=0 WHERE Id ={0}", idRol), con);
                updateRol.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception eUpdate)
            {
                Console.WriteLine(eUpdate.ToString());
                con.Close();
                throw (new Exception());

            }
        }

        internal void modificarRol(Rol rolSel)
        {
            try
            {
                con.Open();
                SqlCommand updateRol = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Rol] SET Nombre='{0}',Habilitado={1} WHERE Id={2}", rolSel.getNombre, rolSel.getHabilitado ? "1" : "0", rolSel.getId), con);
                updateRol.ExecuteNonQuery();

                SqlCommand deleteOldRol_Func = new SqlCommand(String.Format("DELETE FROM [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad]  where Rol={0}", rolSel.getId), con);
                deleteOldRol_Func.ExecuteNonQuery();

                SqlCommand insertRol_Func = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] (Rol,Funcionalidad) select r.Id,f.Id from  [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f ,  [GD2C2015].[JANADIAN_DATE].[Rol] r  where f.Descripcion in ('{0}') and r.Id={1}", string.Join("','", rolSel.getFuncionalidades), rolSel.getId), con);
                insertRol_Func.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal List<string> getCiudades()
        {
            List<String> ciudades = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT c.Nombre FROM [GD2C2015].[JANADIAN_DATE].[Ciudad] c "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Ciudades"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    ciudades.Add(Convert.ToString(Fila["Nombre"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return ciudades;
        }

        internal List<string> getTiposServicio()
        {
            List<String> tiposServicio = new List<String>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT c.Nombre FROM [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] c "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Tipos Servicio"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    tiposServicio.Add(Convert.ToString(Fila["Nombre"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return tiposServicio;
        }

        internal Ruta getRutaBySameConditions(string origen, string destino, string tipoServicio)
        {
            Ruta ruta = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 r.Id, r.Codigo,r.Precio_BaseKG,r.Precio_BasePasaje,o.Nombre as Origen,d.Nombre as Destino,t.Nombre as Tipo_Servicio,r.Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Ruta] r INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] o on (r.Ciudad_Origen=o.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] d on (r.Ciudad_Destino=d.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t on (r.Tipo_Servicio=t.Id) WHERE o.Nombre = '{0}' AND d.Nombre = '{1}' AND t.Nombre = '{2}'  ", origen, destino, tipoServicio), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return null;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    ruta = new Ruta(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Origen"]), Convert.ToString(Fila["Destino"]), Convert.ToDecimal(Fila["Codigo"]), Convert.ToDouble(Fila["Precio_BaseKG"]), Convert.ToDouble(Fila["Precio_BasePasaje"]), Convert.ToString(Fila["Tipo_Servicio"]), Convert.ToBoolean(Fila["Habilitado"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return ruta;
        }

        internal void insertarRuta(string codigo, Decimal pBaseKG, Decimal pBasePasaje, string tipoServicio, string origen, string destino)
        {
            try
            {
                con.Open();
                SqlCommand insertRol = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Ruta] (Codigo,Precio_BaseKG ,Precio_BasePasaje,Ciudad_Origen,Ciudad_Destino,Tipo_Servicio) SELECT '{0}',{1:0.00},{2:0.00},o.Id,d.Id,t.Id FROM [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t,[GD2C2015].[JANADIAN_DATE].[Ciudad] o,[GD2C2015].[JANADIAN_DATE].[Ciudad] d WHERE o.Nombre='{3}' AND d.Nombre='{4}' AND t.Nombre='{5}'", codigo, pBaseKG.ToString().Replace(",", "."), pBasePasaje.ToString().Replace(",", "."), origen, destino, tipoServicio), con);
                insertRol.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal void bajaLogicaRuta(int idRuta)
        {
            try
            {
                con.Open();
                SqlCommand updateRol = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Ruta] SET Habilitado=0 WHERE Id ={0}", idRuta), con);
                updateRol.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception eUpdate)
            {
                Console.WriteLine(eUpdate.ToString());
                con.Close();
                throw (new Exception());

            }
        }

        internal void modificarRuta(Ruta rutaSel)
        {
            try
            {
                con.Open();
                int origen=0;
                int destino = 0;
                int tipoServ = 0;

                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 Id FROM [GD2C2015].[JANADIAN_DATE].[Ciudad]  WHERE Nombre='{0}'  ", rutaSel.getOrigen), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla1";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new Exception("No existe la ciudad Origen"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    origen = Convert.ToInt32(Fila["Id"]);
                }

                 cmd = new SqlCommand(String.Format("SELECT TOP 1 Id FROM [GD2C2015].[JANADIAN_DATE].[Ciudad]  WHERE Nombre='{0}'  ", rutaSel.getDestino), con);
                 dt = new DataTable();

                dt.TableName = "Tabla1";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new Exception("No existe la ciudad Destino"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    destino = Convert.ToInt32(Fila["Id"]);
                }

                cmd = new SqlCommand(String.Format("SELECT TOP 1 Id FROM [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio]  WHERE Nombre='{0}'  ", rutaSel.getTipoServicio), con);
                dt = new DataTable();

                dt.TableName = "Tabla1";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new Exception("No existe el tipo de servicio"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    tipoServ = Convert.ToInt32(Fila["Id"]);
                }

                SqlCommand updateRol = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Ruta]  SET Codigo={0},Habilitado={1},Ciudad_Origen={2},Ciudad_Destino={3},Tipo_Servicio={4},Precio_BaseKG={6:0.00},Precio_BasePasaje={7:0.00} WHERE Id={5}", rutaSel.getCodigo, rutaSel.getHabilitado ? "1" : "0", origen, destino, tipoServ, rutaSel.getId, rutaSel.getPrecio_BaseKG.ToString().Replace(",", "."), rutaSel.getPrecio_BasePasaje.ToString().Replace(",", ".")), con);
                updateRol.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal void modificarAeronave(Aeronave aeronaveSel)
        {
            try
            {
                con.Open();
                int fabricante = 0;
                int tipoServ = 0;

                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 Id FROM [GD2C2015].[JANADIAN_DATE].[Fabricante]  WHERE Nombre='{0}'  ", aeronaveSel.getFabricante), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla1";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new Exception("No existe el fabricante"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    fabricante = Convert.ToInt32(Fila["Id"]);
                }

                cmd = new SqlCommand(String.Format("SELECT TOP 1 Id FROM [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio]  WHERE Nombre='{0}'  ", aeronaveSel.getTipoServicio), con);
                dt = new DataTable();

                dt.TableName = "Tabla1";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new Exception("No existe el tipo de servicio"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    tipoServ = Convert.ToInt32(Fila["Id"]);
                }
                SqlCommand updateAeronave = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Aeronave]  SET Modelo='{0}',Habilitado={1},Matricula='{2}',KG_Disponibles={3},Tipo_Servicio={4},Fabricante={5},Cant_Butacas_Ventanilla={6},Cant_Butacas_Pasillo={7} WHERE Id={8}", aeronaveSel.getModelo, aeronaveSel.getHabilitado ? "1" : "0", aeronaveSel.getMatricula, aeronaveSel.getKGDisponibles, aeronaveSel.getTipoServicio, aeronaveSel.getFabricante, aeronaveSel.getCantidadButacasVentanilla, aeronaveSel.getCantidadButacasPasillo, aeronaveSel.getId), con);
                updateAeronave.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal List<string> getFabricantes()
        {

            List<String> fabricantes = new List<String>();

            try{
    //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT c.Nombre FROM [GD2C2015].[JANADIAN_DATE].[Fabricante] c "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Fabricantes"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    fabricantes.Add(Convert.ToString(Fila["Nombre"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return fabricantes;
}

        internal Aeronave getAeronaveByMatricula(string matricula)
        {
            Aeronave aeronave = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 a.Id,a.Matricula,a.Modelo ,	a.KG_Disponibles,	f.Nombre as Fabricante,	t.Nombre as Tipo_Servicio,	Fecha_Alta,	Baja_Fuera_Servicio,	Baja_Vida_Util,	Fecha_Baja_Definitiva,	Cant_Butacas_Ventanilla,	Cant_Butacas_Pasillo,Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Aeronave] a INNER JOIN [GD2C2015].[JANADIAN_DATE].[Fabricante] f  ON (f.Id=a.Fabricante) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t  ON (t.Id=a.Tipo_Servicio)   WHERE a.Matricula = '{0}'  ", matricula), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return null;
                }
                foreach (DataRow Fila in dt.Rows)
                {

                    aeronave = new Aeronave(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Matricula"]), Convert.ToString(Fila["Modelo"]), Convert.ToDecimal(Fila["KG_Disponibles"]), Convert.ToString(Fila["Fabricante"]), Convert.ToInt32(Fila["Cant_Butacas_Ventanilla"]), Convert.ToInt32(Fila["Cant_Butacas_Pasillo"]), Convert.ToString(Fila["Tipo_Servicio"]), Convert.ToBoolean(Fila["Habilitado"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return aeronave;
        }

        internal void insertarAeronave(string matricula, string modelo, string fabricante, string tipoServicio, decimal kg_disponibles, decimal butacasPasillo, decimal butacasVentanilla)
        {
            try
            {
                con.Open();
                SqlCommand insertAeronave = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Aeronave] (Matricula,Modelo ,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla,Fabricante,Tipo_Servicio,Fecha_Alta) SELECT '{0}','{1}',{2},{3},{4},f.Id,t.Id,'{7}' FROM [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t,[GD2C2015].[JANADIAN_DATE].[Fabricante] f WHERE f.Nombre='{5}'  AND t.Nombre='{6}'", matricula, modelo, kg_disponibles, butacasPasillo, butacasVentanilla, fabricante,tipoServicio,this.fechaSistema), con);
                insertAeronave.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal void bajaLogicaAeronave(int idAeronave)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Aeronave] SET Habilitado=0,Baja_Vida_Util=1,Fecha_Baja_Definitiva='{1}' WHERE Id ={0}", idAeronave,this.fechaSistema), con);
                updateAeronave.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception eUpdate)
            {
                Console.WriteLine(eUpdate.ToString());
                con.Close();
                throw (new Exception());

            }
        }

        internal void cancelarPasajeYPaquetesDeAeronave(int p)
        {
            throw new NotImplementedException();
        }

        internal void reemplazarAeronave(int p)
        {
            throw new NotImplementedException();
        }

        internal void bajaFueraServicioAeronave(int idAeronave, string dateOut)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Aeronave] SET Habilitado=0,Baja_Fuera_Servicio=1 WHERE Id ={0}", idAeronave), con);
                updateAeronave.ExecuteNonQuery();

                //creo una baja
                SqlCommand outOfServiceInsert = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Fuera_Servicio] (Fecha_Baja,Fecha_Reinicio,Aeronave) VALUES ('{0}','{1}',{2})", this.fechaSistema,dateOut,idAeronave), con);
                outOfServiceInsert.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception eUpdate)
            {
                Console.WriteLine(eUpdate.ToString());
                con.Close();
                throw (new Exception());

            }
        }

        internal void cancelarPasajeYPaquetesDeAeronave(int p1, string p2)
        {
            throw new NotImplementedException();
        }

        internal void reemplazarAeronave(int p1, string p2)
        {
            throw new NotImplementedException();
        }
    }
}
