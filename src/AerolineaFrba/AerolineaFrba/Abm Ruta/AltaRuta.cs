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
            List<String> tiposServicio = JanadianDateDB.Instance.getTposServicio();

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
            comboOrigen.Text = "";
            comboDestino.Text = "";
            comboBoxTipoServicio.Text = "";

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
                if (JanadianDateDB.Instance.getRutaBySameConditions(comboOrigen.SelectedItem.ToString(),comboDestino.SelectedItem.ToString(),comboBoxTipoServicio.SelectedItem.ToString()) != null)
                {
                    textoError += "Ya existe una ruta para los mismos destinos y tipo de servicio\n";
                }
                if (listBoxFuncionalidades.SelectedItems.Count == 0)
                {
                    textoError += "El campo Funcionalidades es obligatorio\n";
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.insertarRol(JanadianDateDB.RemoveSpecialCharacters(textCodigo.Text), listBoxFuncionalidades.SelectedItems.Cast<string>().ToList());
                MessageBox.Show(null, "Se ha insertado correctamente el nuevo Rol", "Alta de Rol");
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
