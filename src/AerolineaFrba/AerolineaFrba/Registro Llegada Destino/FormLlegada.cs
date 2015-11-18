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
            dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Format = DateTimePickerFormat.Custom;

            this.dateTimeFechaLlegada.CustomFormat = "dd 'de' MMMM 'de' yyyy hh:mm:ss";
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
            dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema();
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema();
            comboOrigen.SelectedItem = null;
            comboDestino.SelectedItem = null;
            comboAeronave.SelectedItem = null;
}

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
              try
            {
                String textoError = "";

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
                else {
                    textoError += "Origen y destino obligatorio\n";
                }

                if (dateTimeFechaLlegada.Value == null)
                {
                    textoError += "El campo fecha de llegada es obligatorio\n";
                }
                if (dateTimeFechaLlegada.Value.CompareTo(JanadianDateDB.Instance.getFechaSistema()) <= 0)
                {
                    textoError += "El campo fecha de llegada debe ser mayor a la fecha actual\n";
                }

                Viaje viaje;
                if (comboAeronave.SelectedItem != null && comboOrigen.SelectedItem != null && comboDestino.SelectedItem != null && dateTimeFechaLlegada.Value !=null)
                {

                 Aeronave nave = JanadianDateDB.Instance.getAeronaveByMatricula(comboAeronave.SelectedItem.ToString());
                 viaje = JanadianDateDB.Instance.getViaje(nave,comboOrigen.SelectedItem,comboDestino.SelectedItem, dateTimeFechaLlegada.Value);
                 
                 if (viaje == null)
                 {
                     textoError += "No existe el viaje para las condiciones ingresadas\n";

                 }
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

           //     JanadianDateDB.Instance.insertarViaje(new Viaje(0, ((Aeronave)comboAeronave.SelectedItem).getId, ((Ruta)comboRuta.SelectedItem).getId, dateTimeFechaSalida.Value, dateTimeFechaLlegada.Value, dateTimeFechaLlegada.Value), ((Aeronave)comboAeronave.SelectedItem), ((Ruta)comboRuta.SelectedItem));
                MessageBox.Show(null, "Se ha registrado la llegada", "Registrar Llegada destino");
                this.limpiar();
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        }
    }
