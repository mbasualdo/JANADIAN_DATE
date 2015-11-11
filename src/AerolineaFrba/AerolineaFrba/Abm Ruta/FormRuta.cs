﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class FormRuta : Form
    {
        public FormRuta()
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

        private void linkAlta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new AltaRuta();
            frm.Show(this);
        }

        private void linkListado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoRuta();
            frm.Show(this);
        }

        private void linkModificacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoSeleccionRuta();
            frm.Show(this);
        }

        private void linkBaja_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ListadoSeleccionBajaRuta();
            frm.Show(this);
        }
    }
}
