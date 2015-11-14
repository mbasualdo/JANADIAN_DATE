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
    public partial class AltaAeronave : Form
    {
        public AltaAeronave()
        {
            InitializeComponent();
            List<String> fabricantes = JanadianDateDB.Instance.getFabricantes();

            foreach (String f in fabricantes)
            {
                comboFabricante.Items.Add(f);
            }
            List<String> tiposServicio = JanadianDateDB.Instance.getTiposServicio();

            foreach (String f in tiposServicio)
            {
                comboBoxTipoServicio.Items.Add(f);
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";
                if (textMatricula.Text == null || textMatricula.Text.Trim() == "")
                {
                    textoError += "El campo matricula es obligatorio\n";
                }
                if (textBoxModelo.Text == null || textBoxModelo.Text.Trim() == "")
                {
                    textoError += "El campo modelo es obligatorio\n";
                }
                if (numericUpDownKG.Text == null || numericUpDownKG.Text.Trim() == "")
                {
                    textoError += "El campo KG disponibles es obligatorio\n";
                }
                else
                {
                    if (numericUpDownKG.Value<=0.00M)
                    {
                        textoError += "El campo  KG disponibles no es valido\n";
                    }
                }
                if (numericUpDownPasillo.Text == null || numericUpDownPasillo.Text.Trim() == "")
                {
                    textoError += "El campo Cantidad de butacas pasillo es obligatorio\n";
                }
                else
                {
                    if (numericUpDownPasillo.Value <= 0.00M)
                    {
                        textoError += "El campo Cantidad de butacas pasillo es obligatorio\n";
                    }
                }
                if (numericUpDownVentanilla.Text == null || numericUpDownVentanilla.Text.Trim() == "")
                {
                    textoError += "El campo Cantidad de butacas ventanilla es obligatorio\n";
                }
                else
                {
                    if (numericUpDownVentanilla.Value <= 0.00M)
                    {
                        textoError += "El campo Cantidad de butacas ventanilla es obligatorio\n";
                    }
                }
                if (comboFabricante.Text == null || comboFabricante.Text.Trim() == "")
                {
                    textoError += "El campo Fabricante es obligatorio\n";
                }

                if (JanadianDateDB.Instance.getAeronaveByMatricula(textMatricula.Text) != null)
                {
                    textoError += "Ya existe una aeronave con esa matricula\n";
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
                JanadianDateDB.Instance.insertarAeronave(JanadianDateDB.RemoveSpecialCharacters(textMatricula.Text), JanadianDateDB.RemoveSpecialCharacters(textBoxModelo.Text), JanadianDateDB.RemoveSpecialCharacters(comboFabricante.SelectedItem.ToString()), JanadianDateDB.RemoveSpecialCharacters(comboBoxTipoServicio.SelectedItem.ToString()), numericUpDownKG.Value.ToString(),numericUpDownPasillo.Value.ToString(),numericUpDownVentanilla.Value.ToString());
                MessageBox.Show(null, "Se ha insertado correctamente la nueva aeronave", "Alta de Aeronave");
                limpiarForm();
                this.Close();
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void limpiarForm()
        {
            textMatricula.Text = "";
            textBoxModelo.Text = "";
            numericUpDownKG.Value = 0.00M;
            numericUpDownVentanilla.Value = 0.00M;
            numericUpDownPasillo.Value = 0.00M;
            comboFabricante.SelectedItem = null;
            comboBoxTipoServicio.SelectedItem = null;

        }
    }
}
