using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Registro_Llegada_Destino
{
    public partial class FormLlegada : Form
    {
        public FormLlegada()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormLlegada_Load(object sender, EventArgs e)
        {
            List<Aeronave> aeronaves = JanadianDateDB.Instance.getAeronaves();

            foreach (Aeronave f in aeronaves)
            {
                comboAeronave.Items.Add(f.getMatricula);
            }
            List<String> ciudades = JanadianDateDB.Instance.getCiudades();

            foreach (String f in ciudades)
            {
                comboOrigen.Items.Add(f);
                comboDestino.Items.Add(f);
            }
           // dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Format = DateTimePickerFormat.Custom;

            this.dateTimeFechaLlegada.CustomFormat = "dd 'de' MMMM 'de' yyyy HH:mm:ss";
            //  this.dateTimeFecha.Width = 1000;
            this.dateTimeFechaLlegada.ShowUpDown = true;

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

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

private void limpiar()
{
          //  dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema();
            comboOrigen.SelectedItem = null;
            comboDestino.SelectedItem = null;
            comboAeronave.SelectedItem = null;
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
}

        private void validarCampos(out String textoError, out Viaje viaje)
        {
            textoError = "";

            if (comboOrigen.Text == null || comboOrigen.Text.Trim() == "")
            {
                textoError += "El campo Origen es obligatorio\n";
            }
            if (comboDestino.Text == null || comboDestino.Text.Trim() == "")
            {
                textoError += "El campo Destino es obligatorio\n";
            }
            if (comboAeronave.Text == null || comboAeronave.Text.Trim() == "")
            {
                textoError += "El campo Aeronave es obligatorio\n";
            }
            if (comboOrigen.SelectedItem != null && comboDestino.SelectedItem != null)
            {
                if (comboOrigen.SelectedItem.Equals(comboDestino.SelectedItem))
                {
                    textoError += "Origen y destino deben ser diferentes\n";

                }

            }
            else
            {
                textoError += "Origen y destino obligatorio\n";
            }

            if (dateTimeFechaLlegada.Value == null)
            {
                textoError += "El campo fecha de llegada es obligatorio\n";
            }
            //if (dateTimeFechaLlegada.Value.CompareTo(JanadianDateDB.Instance.getFechaSistema()) <= 0)
            //{
            //    textoError += "El campo fecha de llegada debe ser mayor a la fecha actual\n";
            //}

            viaje = null;

            if (comboAeronave.SelectedItem != null && comboOrigen.SelectedItem != null && comboDestino.SelectedItem != null && dateTimeFechaLlegada.Value != null)
            {

                Aeronave nave = JanadianDateDB.Instance.getAeronaveByMatricula(comboAeronave.SelectedItem.ToString());
                viaje = JanadianDateDB.Instance.getViaje(nave, comboOrigen.SelectedItem, comboDestino.SelectedItem);

                if (viaje == null)
                {
                    textoError += "No existe  viaje al que no le haya sido registrada la llegada para las condiciones ingresadas\nRevise si el viaje existe o si ya registro la llegada\n";

                }
                else if (viaje.getFechaLlegada.CompareTo(viaje.getFechaSalida)>0) {
                    textoError += "Ya se ha registrado la llegada de este viaje\n";

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError;
                Viaje viaje;
                validarCampos(out textoError, out viaje);

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("SELECT * from [JANADIAN_DATE].[Itinerario_Aeronave] WHERE Matricula='{0}' and Origen='{1}' and Destino='{2}' ", comboAeronave.SelectedItem.ToString(), comboOrigen.SelectedItem.ToString(), comboDestino.SelectedItem.ToString());
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");
                dataGridRol1.Columns.Clear();
                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);
                // Create a  button column
                DataGridViewButtonColumn columnSave = new DataGridViewButtonColumn();

                // Set column values
                columnSave.Name = "buttonSelection";
                columnSave.HeaderText = "Registrar";
                dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnSave);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridRol1.Columns["buttonSelection"].Index && e.RowIndex >= 0)
            {
                Aeronave nave = JanadianDateDB.Instance.getAeronaveByMatricula(comboAeronave.SelectedItem.ToString());
                Viaje viaje = JanadianDateDB.Instance.getViaje(nave, comboOrigen.SelectedItem, comboDestino.SelectedItem, Convert.ToDateTime(dataGridRol1.Rows[e.RowIndex].Cells["FechaSalida"].Value),Convert.ToDateTime(dataGridRol1.Rows[e.RowIndex].Cells["Fecha_Llegada_Estimada"].Value));

                if (dateTimeFechaLlegada.Value.CompareTo(viaje.getFechaSalida) <= 0)
                {
                    MessageBox.Show(null, "El campo fecha de llegada debe ser mayor a la fecha  de salida del viaje", "Registrar Llegada destino");
                    return;
                }

                JanadianDateDB.Instance.registrarLlegada(viaje, dateTimeFechaLlegada.Value);
                MessageBox.Show(null, "Se ha registrado la llegada", "Registrar Llegada destino");
                this.limpiar();
            }
        }

        }
    }
