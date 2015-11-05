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
using AerolineaFrba.Excepciones;
using AerolineaFrba.Abm_Rol;
using AerolineaFrba.Compra;

namespace AerolineaFrba
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


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
            try
            {


                if (textBoxLoginUser.Text == "")
                {
                    MessageBox.Show(null, "Debe ingresar el nombre de usuario", "Login incorrecto");
                    return;
                }
                if (textBoxLoginPassword.Text == "")
                {
                    MessageBox.Show(null, "Debe ingresar la clave del usuario", "Login incorrecto");
                    return;
                }

                Usuario userLogin = JanadianDateDB.Instance.getUsuario(textBoxLoginUser.Text, textBoxLoginPassword.Text);

            }
            catch (NoResultsException e1)
            {
                MessageBox.Show(null, e1.Message, "Login incorrecto");
                return;
            }
            catch (PasswordMismatchException e2)
            {
                MessageBox.Show(null, e2.Message, "Login incorrecto");
                return;
            }
            catch (UnavailableException e3)
            {
                MessageBox.Show(null, e3.Message, "Login incorrecto");
                return;
            }
            catch (Exception e4)
            {
                MessageBox.Show(null, e4.Message, "Login incorrecto");
                return;
            }
        }
        FormCompra frm = new FormCompra();
    }
}
