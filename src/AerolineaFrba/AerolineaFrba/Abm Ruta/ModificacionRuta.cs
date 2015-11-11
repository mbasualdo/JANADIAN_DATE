using System;
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
    public partial class ModificacionRuta : Form
    {
        private Ruta rutaSel;
        public ModificacionRuta()
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
        public ModificacionRuta(Ruta rutaSel)
        {
            // TODO: Complete member initialization
            this.rutaSel = rutaSel;
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
            checkBoxHabilitado.Checked = false;
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
                else {
                    rutaSel.setCodigo(Convert.ToDecimal(JanadianDateDB.RemoveSpecialCharacters(textCodigo.Text)));
                }
                if (textBoxKG.Text == null || textBoxKG.Text.Trim() == "")
                {
                    textoError += "El campo Precio Base KG es obligatorio\n";
                }
                                else {
                    rutaSel.setPrecio_BaseKG(Convert.ToDouble(JanadianDateDB.RemoveSpecialCharacters(textBoxKG.Text)));
                }
                if (textBoxPasaje.Text == null || textBoxPasaje.Text.Trim() == "")
                {
                    textoError += "El campo Precio Base Pasaje es obligatorio\n";
                }
                                else {
                    rutaSel.setPrecio_BasePasaje(Convert.ToDouble(JanadianDateDB.RemoveSpecialCharacters(textBoxPasaje.Text)));
                }
                if (comboOrigen.Text == null || comboOrigen.Text.Trim() == "")
                {
                    textoError += "El campo Origen es obligatorio\n";
                }
                                else {
                    rutaSel.setOrigen(comboOrigen.Text);
                }
                if (comboDestino.Text == null || comboDestino.Text.Trim() == "")
                {
                    textoError += "El campo Destino es obligatorio\n";
                }
                                else {
                    rutaSel.setDestino(comboDestino.Text);
                }
                if (comboOrigen.Text.Equals(comboDestino.Text))
                {
                    textoError += "Origen y Destino no deben ser iguales\n";

                }

                if (comboBoxTipoServicio.Text == null || comboBoxTipoServicio.Text.Trim() == "")
                {
                    textoError += "El campo Tipo de servicio es obligatorio\n";
                }
                                else {
                    rutaSel.setTipoServicio(comboBoxTipoServicio.Text);
                }
                rutaSel.setHabilitado(checkBoxHabilitado.Checked);
                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.modificarRuta(rutaSel);
                MessageBox.Show(null, String.Format("Se ha modificado correctamente la ruta con Id {0}", rutaSel.getId), "Modificacion de Ruta");
                limpiarForm();
                this.Close();
            }
            catch (Exception erM)
            {
                erM.ToString();
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void ModificacionRuta_Load(object sender, EventArgs e)
        {
            textCodigo.Text = rutaSel.getCodigo.ToString();
            textBoxKG.Text = rutaSel.getPrecio_BaseKG.ToString();
            textBoxPasaje.Text = rutaSel.getPrecio_BasePasaje.ToString();
            comboOrigen.SelectedItem = rutaSel.getOrigen;
            comboDestino.Text = rutaSel.getDestino;
            comboBoxTipoServicio.Text = rutaSel.getTipoServicio;
            checkBoxHabilitado.Checked = rutaSel.getHabilitado;
        }
    }
}
