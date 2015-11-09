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

        internal Usuario getUsuario(string username, string password)
        {
            Usuario user = null;
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
            return funcionalidades;
        }

        internal List<string> getFuncionalidadesInvitado()
        {
            List<String> funcionalidades = new List<String>();
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
            return funcionalidades;
        }
        internal DataTable getDataTableResults(DataGridView d,String query){
            con.Open();
            SqlCommand cmd = new SqlCommand(String.Format(query), con);
            DataTable dt = new DataTable();

            dt.TableName = "Tabla";
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        internal List<string> getFuncionalidades()
        {
            List<String> funcionalidades = new List<String>();
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
                throw (new NoResultsException("No hay Rol"));
            }
            foreach (DataRow Fila in dt.Rows)
            {
                funcionalidades.Add(Convert.ToString(Fila["Descripcion"]));
            }
            con.Close();
            return funcionalidades;
        }

        internal Rol getRolByname(string nombreRol)
        {
         Rol   rol = null;
            //
            // Open the SqlConnection.
            //
            con.Open();
            //
            // The following code uses an SqlCommand based on the SqlConnection.
            //
            SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 Id,Nombre,Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Rol] r WHERE r.Nombre = '{0}'  ",nombreRol), con);
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
            return rol;
        }

        internal void insertarRol(string p, List<string> list)
        {
            con.Open();
            SqlCommand insertRol = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Rol] (Nombre) VALUES ('{0}')", p), con);
            insertRol.ExecuteNonQuery();

            SqlCommand insertRol_Func = new SqlCommand(String.Format("INSERT INTO [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] (Rol,Funcionalidad) select r.Id,f.Id from  [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f ,  [GD2C2015].[JANADIAN_DATE].[Rol] r  where f.Descripcion in ('{0}') and r.Nombre='{1}'", string.Join("','", list), p), con);
            insertRol_Func.ExecuteNonQuery();
            con.Close();


        }

        internal List<String>   getFuncionalidadesByRol(int id)

        {
            List<String> funcionalidades = new List<String>();
            //
            // Open the SqlConnection.
            //
            con.Open();
            //
            // The following code uses an SqlCommand based on the SqlConnection.
            //
            SqlCommand cmd = new SqlCommand(String.Format("SELECT f.Descripcion FROM [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f,[GD2C2015].[JANADIAN_DATE].[Rol] r,[GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf WHERE r.Id=rf.Rol AND f.Id=rf.Funcionalidad and r.Id={0}",id), con);
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
            return funcionalidades;
        }
    }
}
