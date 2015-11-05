using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AerolineaFrba.Excepciones;

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
            Usuario user=null;
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
            }
            using (SqlConnection con = JanadianDateDB.Instance.GetDBConnection())
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //

                using (SqlCommand command = new SqlCommand(String.Format("SELECT TOP 1 u.Id,u.Nombre,u.Password,u.Intentos,u.Habilitado,r.Nombre FROM [GD2C2015].[JANADIAN_DATE].[Usuario] u,[GD2C2015].[JANADIAN_DATE].[Rol] r WHERE u.Nombre = '{0}' and r.Nombre LIKE '%Admin%'  ", username), con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows){
                        con.Close();
                        throw (new NoResultsException("No hay usuarios"));
                    }
                        
                    while (reader.Read())
                    {
                        if (!reader.GetString(2).Equals(hash))
                        {
                            SqlCommand update = new SqlCommand(String.Format("UPDATE [GD2C2015].[JANADIAN_DATE].[Usuario] SET Intentos = {0} WHERE Nombre = '{1}'", reader.GetInt32(3) + 1, username), con);
                            update.ExecuteNonQuery();
                            con.Close();
                            throw (new PasswordMismatchException("No coincide el password"));
                        }
                        else if(reader.GetBoolean(4)){
                            user = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetString(5));
                        }else{
                            con.Close();
                            throw (new UnavailableException("No esta habilitado"));
                        }
                    }
                }
                con.Close();
            }

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
