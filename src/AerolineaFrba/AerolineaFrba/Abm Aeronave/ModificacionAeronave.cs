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

    public partial class ModificacionAeronave : Form
    {
        private Aeronave aeronaveSel;

        public ModificacionAeronave()
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
        public ModificacionAeronave(Aeronave aeronaveSel)
        {
            // TODO: Complete member initialization
            this.aeronaveSel = aeronaveSel;
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
                else
                {
                    aeronaveSel.setMatricula(JanadianDateDB.RemoveSpecialCharacters(textMatricula.Text));
                }
                if (textBoxModelo.Text == null || textBoxModelo.Text.Trim() == "")
                {
                    textoError += "El campo modelo es obligatorio\n";
                }
                else
                {
                    aeronaveSel.setModelo(JanadianDateDB.RemoveSpecialCharacters(textBoxModelo.Text));
                }

                if (numericUpDownKG.Text == null || numericUpDownKG.Text.Trim() == "")
                {
                    textoError += "El campo Precio Base KG es obligatorio\n";
                }
                else if (numericUpDownKG.Value <= 0.00M)
                {
                    textoError += "El campo KG disponibles no es valido\n";
                }
                else
                {
                    aeronaveSel.setKG_Disponibles(Convert.ToDecimal(numericUpDownKG.Value));
                }
                if (numericUpDownPasillo.Text == null || numericUpDownPasillo.Text.Trim() == "")
                {
                    textoError += "El campo Cantidad de butacas pasillo es obligatorio\n";
                }
                else if (numericUpDownPasillo.Value <= 0.00M)
                {
                    textoError += "El campo Cantidad de butacas pasillo no es valido\n";
                }
                else
                {
                    aeronaveSel.setButacasPasillo(Convert.ToInt32(numericUpDownPasillo.Value));
                }
                if (numericUpDownVentanilla.Text == null || numericUpDownVentanilla.Text.Trim() == "")
                {
                    textoError += "El campo Cantidad de butacas ventanilla es obligatorio\n";
                }
                else if (numericUpDownVentanilla.Value <= 0.00M)
                {
                    textoError += "El campo Cantidad de butacas ventanilla no es valido\n";
                }
                else
                {
                    aeronaveSel.setButacasVentanilla(Convert.ToInt32(numericUpDownVentanilla.Value));
                }

                if (JanadianDateDB.Instance.getAeronaveByMatricula(textMatricula.Text) != null)
                {
                    textoError += "Ya existe una aeronave con esa matricula\n";
                }
                if (comboFabricante.Text == null || comboFabricante.Text.Trim() == "")
                {
                    textoError += "El campo Fabricante es obligatorio\n";
                }
                else
                {
                    aeronaveSel.setFabricante(comboFabricante.Text);
                }

                if (comboBoxTipoServicio.Text == null || comboBoxTipoServicio.Text.Trim() == "")
                {
                    textoError += "El campo Tipo de servicio es obligatorio\n";
                }
                else
                {
                    aeronaveSel.setTipoServicio(comboBoxTipoServicio.Text);
                }
                aeronaveSel.setHabilitado(checkBoxHabilitado.Checked);
                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.modificarAeronave(aeronaveSel);
                MessageBox.Show(null, String.Format("Se ha modificado correctamente la aeronave con Id {0}", aeronaveSel.getId), "Modificacion de Aeronave");
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
            checkBoxHabilitado.Checked = false;
        }

        private void ModificacionAeronave_Load(object sender, EventArgs e)
        {

            textMatricula.Text = aeronaveSel.getMatricula;
            textBoxModelo.Text = aeronaveSel.getModelo;
            numericUpDownKG.Value = aeronaveSel.getKGDisponibles;
            numericUpDownVentanilla.Value = aeronaveSel.getCantidadButacasVentanilla;
            numericUpDownPasillo.Value = aeronaveSel.getCantidadButacasPasillo;
            comboFabricante.SelectedItem = aeronaveSel.getFabricante;
            comboBoxTipoServicio.SelectedItem = aeronaveSel.getTipoServicio;
            checkBoxHabilitado.Checked = aeronaveSel.getHabilitado;
        }
    }
}
