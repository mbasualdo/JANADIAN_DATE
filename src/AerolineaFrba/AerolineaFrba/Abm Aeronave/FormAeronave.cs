using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class FormAeronave : Form
    {
        public FormAeronave()
        {
            InitializeComponent();
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

        private void linkBaja_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoSeleccionBajaAeronave();
            frm.Show(this);
        }

        private void linkModificacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoSeleccionAeronave();
            frm.Show(this);
        }

        private void linkListado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoAeronave();
            frm.Show(this);
        }

        private void linkAlta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new AltaAeronave();
            frm.Show(this);
        }
    }
}
