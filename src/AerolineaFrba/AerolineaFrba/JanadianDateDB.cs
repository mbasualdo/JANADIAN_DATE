using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
        return 0;
        }
    }
}
