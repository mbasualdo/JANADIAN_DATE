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

	//
	// In a using statement, acquire the SqlConnection as a resource.
	//

            int cod = JanadianDateDB.Instance.getCompras();
            cod.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AerolineaFrba.Compra.Form1 frm = new AerolineaFrba.Compra.Form1();
            frm.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelIniciarSesion_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxLoginUser.Text =="")
            {
                MessageBox.Show(null, "Debe ingresar el nombre de usuario", "Login incorrecto");
                return;
            }
            if (textBoxLoginPassword.Text == "")
            {
                MessageBox.Show(null, "Debe ingresar la clave del usuario", "Login incorrecto");
                return;
            }

            int cod = JanadianDateDB.Instance.getUsuario(textBoxLoginUser.Text, textBoxLoginPassword.Text);


        }
    }
}
