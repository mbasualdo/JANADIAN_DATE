using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class FormRol : Form
    {

        public FormRol()
        {
            InitializeComponent();
        }

        private void FormRol_Load(object sender, EventArgs e)
        {


        }

        private void d_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backClick();
        }

        private void backClick()
        {
            this.Owner.Show();
            this.Hide();
        }

        private void labelBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void labelFuncionalidades_Click(object sender, EventArgs e)
        {

        }

        private void linkAlta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new AltaRol();
            frm.Show(this);
        }

        private void linkListado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoRol();
            frm.Show(this);
        }

        private void linkModificacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ModificacionRol();
            frm.Show(this);
        }

        private void linkBaja_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new BajaRol();
            frm.Show(this);
        }

    }
}
