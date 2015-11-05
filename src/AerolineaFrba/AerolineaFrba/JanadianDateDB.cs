using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        public int getCompras() { 
        using (SqlConnection con = JanadianDateDB.Instance.GetDBConnection())
	{
	    //
	    // Open the SqlConnection.
	    //
	    con.Open();
	    //
	    // The following code uses an SqlCommand based on the SqlConnection.
	    //
        using (SqlCommand command = new SqlCommand("SELECT TOP 2 * FROM [GD2C2015].[JANADIAN_DATE].[Compra]", con))
	    using (SqlDataReader reader = command.ExecuteReader())
	    {
		while (reader.Read())
		{
		 //   Console.WriteLine("{0} {1} {2}",
      //      reader.GetString(0), reader.GetString(4), reader.GetString(6));

            return reader.GetInt32(0);
		}
	}
            con.Close();
    }
        return 0;
        }


        internal int getUsuario(string username, string password)
        {
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
              
                using (SqlCommand command = new SqlCommand(String.Format("SELECT TOP 1 * FROM [GD2C2015].[JANADIAN_DATE].[Usuario] WHERE Nombre = '{0}' AND Password = '{1}'",username,hash), con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                           Console.WriteLine("{0} {1} {2}",
                              reader.GetString(0), reader.GetString(4), reader.GetString(6));

                        return reader.GetInt32(0);
                    }
                }
                con.Close();
            }
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
