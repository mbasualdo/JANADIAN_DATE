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
    public partial class AltaRuta : Form
    {
        public AltaRuta()
        {
            InitializeComponent();
            List<String> ciudades = JanadianDateDB.Instance.getCiudades();

            foreach (String f in ciudades)
            {
                comboOrigen.Items.Add(f);
                comboDestino.Items.Add(f);
            }
            List<String> tiposServicio = JanadianDateDB.Instance.getTiposServicio();

            foreach (String f in tiposServicio)
            {
                comboBoxTipoServicio.Items.Add(f);
            }
        }

        private void AltaRuta_Load(object sender, EventArgs e)
        {

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void limpiarForm()
        {
            textCodigo.Text = "";
            textCodigo.Text = "";
            textBoxKG.Text = "";
            textBoxPasaje.Text = "";
            comboOrigen.SelectedItem = null;
            comboDestino.SelectedItem = null;
            comboBoxTipoServicio.SelectedItem = null;

        }


        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";
                if (textCodigo.Text == null || textCodigo.Text.Trim() == "")
                {
                    textoError += "El campo codigo es obligatorio\n";
                }
                if (textBoxKG.Text == null || textBoxKG.Text.Trim() == "")
                {
                    textoError += "El campo Precio Base KG es obligatorio\n";
                }
                if (textBoxPasaje.Text == null || textBoxPasaje.Text.Trim() == "")
                {
                    textoError += "El campo Precio Base Pasaje es obligatorio\n";
                }
                if (comboOrigen.Text == null || comboOrigen.Text.Trim() == "")
                {
                    textoError += "El campo Origen es obligatorio\n";
                }
                if (comboDestino.Text == null || comboDestino.Text.Trim() == "")
                {
                    textoError += "El campo Destino es obligatorio\n";
                }
                if (comboOrigen.Text.Equals(comboDestino.Text))
                {
                    textoError += "Origen y Destino no deben ser iguales\n";

                }
                if (JanadianDateDB.Instance.getRutaBySameConditions(comboOrigen.SelectedItem.ToString(),comboDestino.SelectedItem.ToString(),comboBoxTipoServicio.SelectedItem.ToString()) != null)
                {
                    textoError += "Ya existe una ruta para los mismos destinos y tipo de servicio\n";
                }
                if (comboBoxTipoServicio.Text == null || comboBoxTipoServicio.Text.Trim() == "")
                {
                    textoError += "El campo Tipo de servicio es obligatorio\n";
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.insertarRuta(JanadianDateDB.RemoveSpecialCharacters(textCodigo.Text), JanadianDateDB.RemoveSpecialCharacters(textBoxKG.Text), JanadianDateDB.RemoveSpecialCharacters(textBoxPasaje.Text), comboBoxTipoServicio.Text.ToString(), comboOrigen.Text.ToString(), comboDestino.Text.ToString());
                MessageBox.Show(null, "Se ha insertado correctamente la nueva ruta", "Alta de Ruta");
                limpiarForm();
                this.Close();
            }
            catch
            {
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }
    }
}
