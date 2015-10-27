using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace AerolineaFrba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GD2C2015"].ConnectionString;
//            Console.Write(conStr);
//            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(conStr);
//           cn.Open();
//            cn.ToString();

	//
	// In a using statement, acquire the SqlConnection as a resource.
	//
	using (SqlConnection con = new SqlConnection(connectionString))
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
		    Console.WriteLine("{0} {1} {2}",
			reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
		}
	}
    }
        }
    }
}
