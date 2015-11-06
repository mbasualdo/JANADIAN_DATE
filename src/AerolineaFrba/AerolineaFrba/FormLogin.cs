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
using AerolineaFrba.Administrador;

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

                if(userLogin!=null){
                    FormAdminMenu frm = new FormAdminMenu(userLogin);
                    frm.Show(this);
                    this.Hide();
                }
            }
            catch (NoResultsException e1)
            {
                messageErrorLogin(e1);
                return;
            }
            catch (PasswordMismatchException e2)
            {
                messageErrorLogin(e2);
                return;
            }
            catch (UnavailableException e3)
            {
                messageErrorLogin(e3);
                return;
            }
            catch (Exception e4)
            {
                messageErrorLogin(e4);
                return;
            }
        }

        private void messageErrorLogin(Exception e1)
        {
            MessageBox.Show(null, e1.Message, "Login incorrecto");
            textBoxLoginUser.Text = "";
            textBoxLoginPassword.Text = "";
            return;
}

        private void linkLabelNoAdmin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInvitadoMenu frm = new FormInvitadoMenu();
            frm.Show(this);
            this.Hide();
        }

            
        }

}