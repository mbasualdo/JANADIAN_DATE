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
    public partial class BajaAeronave : Form
    {
       private Aeronave aeronaveSel;

        public BajaAeronave()
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
        public BajaAeronave(Aeronave aeronaveSel)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.aeronaveSel = aeronaveSel;
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

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult1 = MessageBox.Show("Esta Seguro", "Baja Aeronave", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {

                    JanadianDateDB.Instance.bajaLogicaAeronave(aeronaveSel.getId);
                    DialogResult dialogResult = MessageBox.Show("Que desea hacer con los pasajes/paquetes reservados?. \n Los cancela -> Presione YES. \n reemplaza la aeronave por otra -> Presione NO", "Baja Aeronave", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        JanadianDateDB.Instance.cancelarPasajeYPaquetesDeAeronave(aeronaveSel.getId);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        JanadianDateDB.Instance.reemplazarAeronave(aeronaveSel.getId,aeronaveSel.getMatricula);
                    }

                    MessageBox.Show(null, String.Format("Se ha dado de baja correctamente la aeronave con Id {0}", aeronaveSel.getId), "Baja de Aeronave");
                    limpiarForm();
                    this.Close();
                }
            }
            catch (Exception exBaja)
            {
                Console.WriteLine(exBaja.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void BajaAeronave_Load(object sender, EventArgs e)
        {
            textMatricula.Text = aeronaveSel.getMatricula;
            textBoxModelo.Text = aeronaveSel.getModelo;
            numericUpDownKG.Value = aeronaveSel.getKGDisponibles;
            numericUpDownVentanilla.Value = aeronaveSel.getCantidadButacasVentanilla;
            numericUpDownPasillo.Value = aeronaveSel.getCantidadButacasPasillo;
            comboFabricante.SelectedItem = aeronaveSel.getFabricante;
            comboBoxTipoServicio.SelectedItem = aeronaveSel.getTipoServicio;
            dateTimeFechaReinicio.MinDate = JanadianDateDB.Instance.getFechaSistema();
        }

        private void buttonLimpiar2_Click(object sender, EventArgs e)
        {
            dateTimeFechaReinicio.ResetText();
        }

        private void buttonOutService_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";
                if (dateTimeFechaReinicio.Value == null)
                {
                    textoError += "El campo fecha de reinicio es obligatorio\n";
                }
                if (dateTimeFechaReinicio.Value.CompareTo(JanadianDateDB.Instance.getFechaSistema()) <= 0)
                {
                    textoError += "El campo fecha de reinicio debe ser mayor a la fecha actual\n";
                }
                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                JanadianDateDB.Instance.bajaFueraServicioAeronave(aeronaveSel.getId, dateTimeFechaReinicio.Value.ToShortDateString());
            DialogResult dialogResult = MessageBox.Show("Que desea hacer con los pasajes/paquetes reservados?. \n Los cancela -> Presione YES. \n reemplaza la aeronave por otra -> Presione NO", "Fuera de Servicio Aeronave", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                JanadianDateDB.Instance.cancelarPasajeYPaquetesDeAeronave(aeronaveSel.getId, dateTimeFechaReinicio.Value.ToShortDateString());
            }
            else if (dialogResult == DialogResult.No)
            {
                JanadianDateDB.Instance.reemplazarAeronave(aeronaveSel.getId, aeronaveSel.getMatricula, dateTimeFechaReinicio.Value);
            }

            MessageBox.Show(null, String.Format("Se ha puesto fuera de servicio correctamente la aeronave con Id {0}", aeronaveSel.getId), "Fuera de servicio Aeronave");
            limpiarForm();
            this.Close();
            }
            catch (Exception exAlta){
                Console.WriteLine(exAlta.ToString());
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }
    }
}
