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
                SqlCommand updateAeronave = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Aeronave]  SET Modelo='{0}',Matricula='{2}',KG_Disponibles={3},Tipo_Servicio={4},Fabricante={5},Cant_Butacas_Ventanilla={6},Cant_Butacas_Pasillo={7} WHERE Id={8}", aeronaveSel.getModelo, aeronaveSel.getHabilitado ? "1" : "0", aeronaveSel.getMatricula, aeronaveSel.getKGDisponibles, tipoServ, fabricante, aeronaveSel.getCantidadButacasVentanilla, aeronaveSel.getCantidadButacasPasillo, aeronaveSel.getId), con);
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

                    aeronave = new Aeronave(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Matricula"]), Convert.ToString(Fila["Modelo"]), Convert.ToDecimal(Fila["KG_Disponibles"]), Convert.ToString(Fila["Fabricante"]), Convert.ToInt32(Fila["Cant_Butacas_Ventanilla"]), Convert.ToInt32(Fila["Cant_Butacas_Pasillo"]), Convert.ToString(Fila["Tipo_Servicio"]));
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

        internal void cancelarPasajeYPaquetesDeAeronave(int idAeronave)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[inhabilitarPasajesAeronave] {0},null", idAeronave), con);
                updateAeronave.ExecuteNonQuery();
                SqlCommand updatePaqAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[inhabilitarPaquetesAeronave] {0},null", idAeronave), con);
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

        internal void reemplazarAeronave(int idAeronave,string matricula)
        {
            // sacar los datos de la aeronave

            Dictionary<string, object> dict = obtenerDatosAeronave(idAeronave);

            // buscar todas las aeronaves con las mismas caracteristicas

            List<Aeronave> aeronavesSimilares = obtenerAeronavesSimilares(Convert.ToString(dict["modelo"]), Convert.ToInt32(dict["fabricante"]), Convert.ToInt32(dict["tipoServicio"]), Convert.ToDecimal(dict["kgDisponibles"]), Convert.ToInt32(dict["butacasVentanilla"]), Convert.ToInt32(dict["butacasPasillo"]), idAeronave);
                
            //traigo todos los viajes a reemplazar
            List<Viaje> viajesAeronave = obtenerViajesFuturosAeronave(idAeronave);

            // verificar si estan disponibles las fechas de cada  recorrido programado
            // si hay disponible, se reemplaza la aeronave
            // si no hay disponible se crea una nueva aeronave con las mismas caracteristicas
            reemplazarOCrearNuevaAeronave(idAeronave, viajesAeronave, aeronavesSimilares, matricula);

        }

        private void reemplazarOCrearNuevaAeronave(int idAeronave, List<Viaje> viajesAeronave, List<Aeronave> aeronavesSimilares,string matricula)
        {
            // [JANADIAN_DATE].[Viajes_Fecha_Aeronave]@id int,@fecha datetime

            foreach(Viaje v in viajesAeronave){
                Boolean reemplazoEncontrado = false;
                try
                {
                    foreach(Aeronave otraNave in aeronavesSimilares){
                    int cantViajesFecha = getCantViajesFecha(otraNave,v.getFechaSalida);

                    if (cantViajesFecha == 0) {
                        migrarViajeAOtraAeronave(idAeronave,v, otraNave);
                        reemplazoEncontrado = true;
                    }
                    break;
                    }

                    if (!reemplazoEncontrado)
                    {
                        //crear nueva aeronave
                        // copiar butacas
                        // liberar las butacas_viajes y crear las butacas_viajes con las butacas nuevas
                        crearNuevaAeronaveParaSatisfacerViaje(idAeronave, matricula,v);

                        //se vuelve a pedir aeronaves similares ya que ahora se agrega la nueva
                        Dictionary<string, object> dict = obtenerDatosAeronave(idAeronave);
                        aeronavesSimilares = obtenerAeronavesSimilares(Convert.ToString(dict["modelo"]), Convert.ToInt32(dict["fabricante"]), Convert.ToInt32(dict["tipoServicio"]), Convert.ToDecimal(dict["kgDisponibles"]), Convert.ToInt32(dict["butacasVentanilla"]), Convert.ToInt32(dict["butacasPasillo"]), idAeronave);
                
                    }
                }
                catch (Exception e ){
                    e.ToString();
                    con.Close();
                    //crear nueva aeronave
                    // copiar butacas
                    // liberar las butacas_viajes y crear las butacas_viajes con las butacas nuevas
                    crearNuevaAeronaveParaSatisfacerViaje(idAeronave,matricula, v);

                    //se vuelve a pedir aeronaves similares ya que ahora se agrega la nueva
                    Dictionary<string, object> dict = obtenerDatosAeronave(idAeronave);
                    aeronavesSimilares = obtenerAeronavesSimilares(Convert.ToString(dict["modelo"]), Convert.ToInt32(dict["fabricante"]), Convert.ToInt32(dict["tipoServicio"]), Convert.ToDecimal(dict["kgDisponibles"]), Convert.ToInt32(dict["butacasVentanilla"]), Convert.ToInt32(dict["butacasPasillo"]), idAeronave);
                

                }
            }

        }

        private void crearNuevaAeronaveParaSatisfacerViaje(int idAeronave,string matricula, Viaje v)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[crearNuevaAeronave] {0},'{1}',{2}", idAeronave,matricula, v.getId), con);
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

        public int getCantViajesFecha(Aeronave otraNave,DateTime fechaSalida)
        {
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //

                SqlCommand cmd = new SqlCommand(String.Format("SELECT [GD2C2015].[JANADIAN_DATE].[Viajes_Fecha_Aeronave] ({0},'{1}') as Cant", otraNave.getId, fechaSalida), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return 1;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    int count = Convert.ToInt32(Fila["Cant"]);
                    con.Close();

                    return count;

                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                exAlta.ToString();
                con.Close();
                return 1;

            }
            return 1;
        }

        private void migrarViajeAOtraAeronave(int idAeronave, Viaje v, Aeronave otraNave)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[reemplazarAeronaveViaje] {0},{1},{2}", idAeronave,v.getId,otraNave.getId), con);
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

        private List<Viaje> obtenerViajesFuturosAeronave(int idAeronave)
        {
            List<Viaje> viajes = new List<Viaje>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT [Id],[FechaSalida],[Fecha_Llegada_Estimada],[FechaLlegada],[Aeronave],[Ruta]  FROM [GD2C2015].[JANADIAN_DATE].[Viaje] WHERE Aeronave={0} AND  DATEDIFF(SECOND,'{1}',[FechaSalida]) >0", idAeronave, this.fechaSistema), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return viajes;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    Viaje viaje = new Viaje(Convert.ToInt32(Fila["Id"]), Convert.ToInt32(Fila["Aeronave"]), Convert.ToInt32(Fila["Ruta"]), Convert.ToDateTime(Fila["FechaSalida"]), Convert.ToDateTime(Fila["FechaLlegada"]), Convert.ToDateTime(Fila["Fecha_Llegada_Estimada"]));
                    viajes.Add(viaje);
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));
            }

            return viajes;
        }

        private List<Aeronave> obtenerAeronavesSimilares(string modelo, int fabricante, int tipoServicio,Decimal kgDisponibles,int butacasVentanilla,int butacasPasillo, int idAeronave)
        {
            List<Aeronave> aeronavesSimilares = new List<Aeronave>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT a.Id,a.Matricula,a.Modelo,a.KG_Disponibles,f.Nombre as Fabricante,t.Nombre as Tipo_Servicio,Fecha_Alta,Baja_Fuera_Servicio,Baja_Vida_Util,Fecha_Baja_Definitiva,Cant_Butacas_Ventanilla,Cant_Butacas_Pasillo,Habilitado FROM JANADIAN_DATE.Aeronave a INNER JOIN [GD2C2015].[JANADIAN_DATE].[Fabricante] f  ON (f.Id=a.Fabricante) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t  ON (t.Id=a.Tipo_Servicio)  WHERE a.ID<>{0} AND a.Fabricante={1} AND a.Tipo_Servicio={2} AND a.Modelo='{3}' AND a.Habilitado=1 and KG_Disponibles>={4} and Cant_Butacas_Ventanilla>={5} and Cant_Butacas_Pasillo>={6}", idAeronave, fabricante, tipoServicio, modelo,kgDisponibles,butacasVentanilla,butacasPasillo), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return aeronavesSimilares;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    Aeronave aeronave = new Aeronave(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Matricula"]), Convert.ToString(Fila["Modelo"]), Convert.ToDecimal(Fila["KG_Disponibles"]), Convert.ToString(Fila["Fabricante"]), Convert.ToInt32(Fila["Cant_Butacas_Ventanilla"]), Convert.ToInt32(Fila["Cant_Butacas_Pasillo"]), Convert.ToString(Fila["Tipo_Servicio"]));
                    aeronavesSimilares.Add(aeronave);
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));
            }

            return aeronavesSimilares;
        }

        private Dictionary<String, object> obtenerDatosAeronave(int idAeronave)
        {

            Dictionary<String, object>  dic = new Dictionary<String, object>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 a.Modelo,a.Fabricante,a.Tipo_Servicio,a.KG_Disponibles,a.Cant_Butacas_Ventanilla,a.Cant_Butacas_Pasillo FROM [GD2C2015].[JANADIAN_DATE].[Aeronave] a  WHERE a.Id = {0}  ", idAeronave), con);
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
                    dic.Add("modelo", Convert.ToString(Fila["Modelo"]));
                    dic.Add("fabricante", Convert.ToInt32(Fila["Fabricante"]));
                    dic.Add("tipoServicio", Convert.ToInt32(Fila["Tipo_Servicio"]));
                    dic.Add("kgDisponibles", Convert.ToInt32(Fila["KG_Disponibles"]));
                    dic.Add("butacasVentanilla", Convert.ToInt32(Fila["Cant_Butacas_Ventanilla"]));
                    dic.Add("butacasPasillo", Convert.ToInt32(Fila["Cant_Butacas_Pasillo"]));

                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));
            }

            return dic;
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

        internal void cancelarPasajeYPaquetesDeAeronave(int idAeronave, string fechaMaxima)
        {
            try
            {
                con.Open();
                SqlCommand updateAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[inhabilitarPasajesAeronave] {0},'{1}'", idAeronave, fechaMaxima), con);
                updateAeronave.ExecuteNonQuery();
                SqlCommand updatePaqAeronave = new SqlCommand(String.Format("EXEC  [GD2C2015].[JANADIAN_DATE].[inhabilitarPaquetesAeronave] {0},'{1}'", idAeronave, fechaMaxima), con);
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

        internal void reemplazarAeronave(int idAeronave,string matricula, DateTime fechaReinicio)
        {
            // sacar los datos de la aeronave

            Dictionary<string, object> dict = obtenerDatosAeronave(idAeronave);

            // buscar todas las aeronaves con las mismas caracteristicas

            List<Aeronave> aeronavesSimilares = obtenerAeronavesSimilares(Convert.ToString(dict["modelo"]), Convert.ToInt32(dict["fabricante"]), Convert.ToInt32(dict["tipoServicio"]), Convert.ToDecimal(dict["kgDisponibles"]), Convert.ToInt32(dict["butacasVentanilla"]), Convert.ToInt32(dict["butacasPasillo"]), idAeronave);

            //traigo todos los viajes a reemplazar
            List<Viaje> viajesAeronave = obtenerViajesIntervaloAeronave(idAeronave, fechaReinicio);

            // verificar si estan disponibles las fechas de cada  recorrido programado
            // si hay disponible, se reemplaza la aeronave
            // si no hay disponible se crea una nueva aeronave con las mismas caracteristicas
            reemplazarOCrearNuevaAeronave(idAeronave, viajesAeronave, aeronavesSimilares, matricula);
        }

        private List<Viaje> obtenerViajesIntervaloAeronave(int idAeronave, DateTime fechaReinicio)
        {
            List<Viaje> viajes = new List<Viaje>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT [Id],[FechaSalida],[Fecha_Llegada_Estimada],[FechaLlegada],[Aeronave],[Ruta]  FROM [GD2C2015].[JANADIAN_DATE].[Viaje] WHERE Aeronave={0} AND  DATEDIFF(SECOND,'{1}',[FechaSalida]) >=0 AND DATEDIFF(SECOND,'{2}',ISNULL([FechaLlegada],[Fecha_Llegada_Estimada])) <= 0", idAeronave, this.fechaSistema, fechaReinicio), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return viajes;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    Viaje viaje = new Viaje(Convert.ToInt32(Fila["Id"]), Convert.ToInt32(Fila["Aeronave"]), Convert.ToInt32(Fila["Ruta"]), Convert.ToDateTime(Fila["FechaSalida"]), Convert.ToDateTime(Fila["FechaLlegada"]), Convert.ToDateTime(Fila["Fecha_Llegada_Estimada"]));
                    viajes.Add(viaje);
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));
            }

            return viajes;
        }

        internal void habilitarAeronavesQueSalenDelFueraDeServicio()
        {
            List<int> aeronavesFueraDeServicio = new List<int>();
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
               // and a.Baja_Vida_Util=0 
                SqlCommand cmd = new SqlCommand(String.Format("SELECT Distinct a.Id,fs.Fecha_Reinicio FROM [GD2C2015].[JANADIAN_DATE].[Aeronave] a INNER JOIN [GD2C2015].[JANADIAN_DATE].[Fuera_Servicio] fs ON (a.Id=fs.Aeronave) WHERE a.Habilitado=0 and a.Baja_Fuera_Servicio=1 and a.Baja_Vida_Util=0  order by fs.Fecha_Reinicio desc "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    Console.WriteLine("No hay aeronaves a habilitar");
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    if (Convert.ToDateTime(Fila["Fecha_Reinicio"]).CompareTo(this.fechaSistema) >= 0)
                    {
                        aeronavesFueraDeServicio.Add(Convert.ToInt32(Fila["Id"]));
                    }
                    else if (!aeronavesFueraDeServicio.Contains(Convert.ToInt32(Fila["Id"])))
                    {
                        SqlCommand updateAeronave = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Aeronave] SET Habilitado=1,Baja_Fuera_Servicio=0 WHERE Id ={0}", Convert.ToInt32(Fila["Id"])), con);
                        updateAeronave.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal List<Ruta> getRutas()
        {
            List<Ruta> rutas = new List<Ruta>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT r.Id, r.Codigo,r.Precio_BaseKG,r.Precio_BasePasaje,o.Nombre as Origen,d.Nombre as Destino,t.Nombre as Tipo_Servicio,r.Habilitado  FROM [GD2C2015].[JANADIAN_DATE].[Ruta] r INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] o on (r.Ciudad_Origen=o.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] d on (r.Ciudad_Destino=d.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t on (r.Tipo_Servicio=t.Id) WHERE r.Habilitado=1 "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Rutas"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    rutas.Add(new Ruta(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Origen"]), Convert.ToString(Fila["Destino"]), Convert.ToDecimal(Fila["Codigo"]), Convert.ToDouble(Fila["Precio_BaseKG"]), Convert.ToDouble(Fila["Precio_BasePasaje"]), Convert.ToString(Fila["Tipo_Servicio"]), Convert.ToBoolean(Fila["Habilitado"])));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return rutas;
        }

        internal List<Aeronave> getAeronaves()
        {
            List<Aeronave> aeronaves = new List<Aeronave>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT a.Id,a.Matricula,a.Modelo ,	a.KG_Disponibles,	f.Nombre as Fabricante,	t.Nombre as Tipo_Servicio,	Fecha_Alta,	Baja_Fuera_Servicio,	Baja_Vida_Util,	Fecha_Baja_Definitiva,	Cant_Butacas_Ventanilla,	Cant_Butacas_Pasillo,Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Aeronave] a INNER JOIN [GD2C2015].[JANADIAN_DATE].[Fabricante] f  ON (f.Id=a.Fabricante) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t  ON (t.Id=a.Tipo_Servicio) WHERE a.Habilitado=1  "), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Aeronaves"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    aeronaves.Add(new Aeronave(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Matricula"]), Convert.ToString(Fila["Modelo"]), Convert.ToDecimal(Fila["KG_Disponibles"]), Convert.ToString(Fila["Fabricante"]), Convert.ToInt32(Fila["Cant_Butacas_Ventanilla"]), Convert.ToInt32(Fila["Cant_Butacas_Pasillo"]), Convert.ToString(Fila["Tipo_Servicio"])));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return aeronaves;
        }
        internal void insertarViaje(Viaje v,Aeronave a,Ruta r)
        {
            try
            {
                con.Open();
                SqlCommand insertRol = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Viaje] (FechaSalida,Fecha_Llegada_Estimada,Aeronave,Ruta) VALUES ('{0}','{1}',{2},{3})", v.getFechaSalida,v.getFechaLlegadaEstimada,a.getId,r.getId), con);
                insertRol.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }

        }

        internal Viaje getViaje(Aeronave nave, object origen, object destino, DateTime salida, DateTime llegadaEstimada)
        {
            Viaje viaje = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 v.[Id],[FechaSalida],[Fecha_Llegada_Estimada],[FechaLlegada],[Aeronave],[Ruta]  FROM [GD2C2015].[JANADIAN_DATE].[Viaje] v INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ruta] r ON (v.Ruta=r.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c ON (r.Ciudad_Origen=c.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c2 ON (r.Ciudad_Destino=c2.Id) WHERE v.Aeronave={0} AND c.Nombre='{1}' AND c2.Nombre='{2}'   AND v.FechaSalida='{3}'  AND v.Fecha_Llegada_Estimada='{4}'   ", nave.getId, origen.ToString(), destino.ToString(), salida, llegadaEstimada), con);
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
                    viaje = new Viaje(Convert.ToInt32(Fila["Id"]), Convert.ToInt32(Fila["Aeronave"]), Convert.ToInt32(Fila["Ruta"]), Convert.ToDateTime(Fila["FechaSalida"]), Convert.ToDateTime(Fila["FechaLlegada"]), Convert.ToDateTime(Fila["Fecha_Llegada_Estimada"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return viaje;
        }

        internal Viaje getViaje(Aeronave nave, object origen, object destino)
        {
            Viaje viaje = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 v.[Id],[FechaSalida],[Fecha_Llegada_Estimada],[FechaLlegada],[Aeronave],[Ruta]  FROM [GD2C2015].[JANADIAN_DATE].[Viaje] v INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ruta] r ON (v.Ruta=r.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c ON (r.Ciudad_Origen=c.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c2 ON (r.Ciudad_Destino=c2.Id) WHERE v.Aeronave={0} AND c.Nombre='{1}' AND c2.Nombre='{2}'   ", nave.getId, origen.ToString(), destino.ToString()), con);
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
                    viaje = new Viaje(Convert.ToInt32(Fila["Id"]), Convert.ToInt32(Fila["Aeronave"]), Convert.ToInt32(Fila["Ruta"]), Convert.ToDateTime(Fila["FechaSalida"]), Convert.ToDateTime(Fila["FechaLlegada"]), Convert.ToDateTime(Fila["Fecha_Llegada_Estimada"]));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return viaje;
        }

        internal void registrarLlegada(Viaje viaje,DateTime llegada)
        {
            try
            {
                con.Open();
                SqlCommand updateViaje = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Viaje] SET FechaLlegada='{0}' WHERE Id ={1}", llegada, viaje.getId), con);
                updateViaje.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception eUpdate)
            {
                Console.WriteLine(eUpdate.ToString());
                con.Close();
                throw (new Exception());

            }
        }

        internal Cliente getCliente(decimal dni)
        {
            Cliente cliente = null;
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 [Id],[Dni],[Nombre],[Apellido],[Dir],[Telefono],[Mail],[Fecha_Nac] FROM [JANADIAN_DATE].[Cliente] WHERE Dni={0:0}", dni), con);
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
                    cliente = new Cliente(Convert.ToInt32(Fila["Id"]), Convert.ToDecimal(Fila["Dni"]), Convert.ToString(Fila["Nombre"]), Convert.ToString(Fila["Apellido"]), Convert.ToString(Fila["Dir"]), Convert.ToDecimal(Fila["Telefono"]),Convert.ToString(Fila["Mail"]), Convert.ToDateTime(Fila["Fecha_Nac"]) );
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return cliente;
        }

        internal List<Producto> getProductos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT [Id],[Nombre],[Stock],[Millas_Necesarias] FROM [GD2C2015].[JANADIAN_DATE].[Producto]  WHERE Stock>0"), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    throw (new NoResultsException("No hay Productos"));
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    productos.Add(new Producto(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["Nombre"]), Convert.ToInt32(Fila["Stock"]), Convert.ToInt32(Fila["Millas_Necesarias"])));
                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
            return productos;
        }

        internal int getMillasTotalesDisponibles(Cliente c)
        {
            //  	SELECT SUM ([Cantidad])FROM [GD2C2015].[JANADIAN_DATE].[Millas] WHERE Cliente=1 AND DATEDIFF(YEAR,Fecha,CURRENT_TIMESTAMP) =0
            try
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //

                SqlCommand cmd = new SqlCommand(String.Format("SELECT [GD2C2015].[JANADIAN_DATE].[Millas_Disponibles] ({0}) as Cant ", c.getId), con);
                DataTable dt = new DataTable();

                dt.TableName = "Tabla";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    con.Close();
                    return 1;
                }
                foreach (DataRow Fila in dt.Rows)
                {
                    int count = Convert.ToInt32(Fila["Cant"]);
                    con.Close();

                    return count;

                }
                con.Close();
            }
            catch (Exception exAlta)
            {
                exAlta.ToString();
                con.Close();
                return 0;

            }
            return 0;
        }

        internal void canjearMillas(Cliente cliente, Producto producto, decimal cantidad)
        {
            try
            {
                //canjear millas
                con.Open();
                SqlCommand insertCanje = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Canje] (Fecha,Cantidad,Cliente,Motivo,Producto) VALUES ('{0}',{1},{2},'{3}',{4}) ", this.fechaSistema, cantidad * producto.getMillasNecesarias, cliente.getId, "Canje de " + (cantidad * producto.getMillasNecesarias).ToString() + " millas por " + cantidad.ToString() + " " + producto.getNombre, producto.getId), con);
                insertCanje.ExecuteNonQuery();

                //descontar stock
                SqlCommand updateStock= new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Producto] SET Stock=(Stock-{0}) WHERE Id={1}", cantidad,producto.getId), con);
                updateStock.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception exAlta)
            {
                con.Close();
                throw (new Exception(exAlta.ToString()));

            }
        }

        internal DateTime generarFechaInicialSemestre(object p, DateTime dateTime)
        {
            string date = dateTime.Year.ToString();

            if(p.ToString().Equals("ENE-JUN")){
                date += "-01-01";
            }else{
                date += "-07-01";
            }
            DateTime time = DateTime.Parse(date);

            return time;
        }

        internal DateTime generarFechaFinalSemestre(object p, DateTime dateTime)
        {
            string date = dateTime.Year.ToString();

            if (p.ToString().Equals("ENE-JUN"))
            {
                date += "-06-30 23:59:59";
            }
            else
            {
                date += "-12-31 23:59:59";
            }
            DateTime time = DateTime.Parse(date);

            return time;
        }
    }
}
