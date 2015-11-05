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
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
            }
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand cmd = new SqlCommand(String.Format("SELECT TOP 1 u.Id,u.Nombre as UsuarioNombre,u.Password,u.Intentos,u.Habilitado,r.Nombre as RolNombre FROM [GD2C2015].[JANADIAN_DATE].[Usuario] u,[GD2C2015].[JANADIAN_DATE].[Rol] r WHERE u.Nombre = '{0}' and r.Nombre LIKE '%Admin%'  ", username), con);
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
                        user = new Usuario(Convert.ToInt32(Fila["Id"]), Convert.ToString(Fila["UsuarioNombre"]), Convert.ToInt32(Fila["Intentos"]), Convert.ToString(Fila["RolNombre"]));
                    }


                }
                con.Close();


            return user;
        }



        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
