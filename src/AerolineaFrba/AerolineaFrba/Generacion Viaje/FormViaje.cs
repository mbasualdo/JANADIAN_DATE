using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Generacion_Viaje
{
    public partial class FormViaje : Form
    {
        public FormViaje()
        {
            InitializeComponent();
            List<Ruta> rutas = JanadianDateDB.Instance.getRutas();

            foreach (Ruta f in rutas)
            {
                comboRuta.Items.Add(f);
            }
            List<Aeronave> aeronaves = JanadianDateDB.Instance.getAeronaves();

            foreach (Aeronave f in aeronaves)
            {
                comboAeronave.Items.Add(f);
            }

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

        private void FormViaje_Load(object sender, EventArgs e)
        {
            dateTimeFechaSalida.MinDate = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaSalida.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaSalida.Format = DateTimePickerFormat.Custom;

            this.dateTimeFechaSalida.CustomFormat = "dd 'de' MMMM 'de' yyyy HH:mm:ss";
            
          //  this.dateTimeFecha.Width = 1000;
            this.dateTimeFechaSalida.ShowUpDown = true;

            dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaLlegada.Format = DateTimePickerFormat.Custom;

            this.dateTimeFechaLlegada.CustomFormat = "dd 'de' MMMM 'de' yyyy HH:mm:ss";

            //  this.dateTimeFecha.Width = 1000;
            this.dateTimeFechaLlegada.ShowUpDown = true;
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            dateTimeFechaSalida.MinDate = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaSalida.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaLlegada.MinDate = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaLlegada.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            comboRuta.SelectedItem = null;
            comboAeronave.SelectedItem = null;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";

                if (comboRuta.Text == null || comboRuta.Text.Trim() == "")
                {
                    textoError += "El campo Ruta es obligatorio\n";
                }
                if (comboAeronave.Text == null || comboAeronave.Text.Trim() == "")
                {
                    textoError += "El campo Aeronave es obligatorio\n";
                }
                if (comboRuta.SelectedItem != null && comboAeronave.SelectedItem != null)
                {
                    if (!((Ruta)comboRuta.SelectedItem).getTipoServicio.Equals(((Aeronave)comboAeronave.SelectedItem).getTipoServicio))
                    {
                        textoError += "Ruta y Aeronave no tienen el mismo tipo de servicio para generar el viaje\n";

                    }

                }
                else {
                    textoError += "Ruta y Aeronave obligatorio\n";
                }
                if (dateTimeFechaSalida.Value == null)
                {
                    textoError += "El campo fecha de salida es obligatorio\n";
                }
                if (dateTimeFechaSalida.Value.CompareTo(JanadianDateDB.Instance.getFechaSistema()) <= 0)
                {
                    textoError += "El campo fecha de salida debe ser mayor a la fecha actual\n";
                }
                if (dateTimeFechaLlegada.Value == null)
                {
                    textoError += "El campo fecha de llegada es obligatorio\n";
                }
                if (dateTimeFechaLlegada.Value.CompareTo(JanadianDateDB.Instance.getFechaSistema()) <= 0)
                {
                    textoError += "El campo fecha de llegada debe ser mayor a la fecha actual\n";
                }
                 if (dateTimeFechaLlegada.Value.CompareTo(dateTimeFechaSalida.Value) <= 0)
                {
                    textoError += "El campo fecha de llegada debe ser mayor a la salida\n";
                }
                 if (dateTimeFechaLlegada.Value.Subtract(dateTimeFechaSalida.Value).TotalHours > 24)
                {
                    textoError += "El campo fecha de llegada debe como maximo 24 hs a la salida\n";
                }

                 int cantViajesFecha = JanadianDateDB.Instance.getCantViajesFecha(((Aeronave)comboAeronave.SelectedItem), dateTimeFechaSalida.Value);

                 if (cantViajesFecha > 0)
                 {
                     textoError += "La aeronave seleccionada no esta disponible para la fecha que desea generar el viaje\n";

                 }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                JanadianDateDB.Instance.insertarViaje(new Viaje(0, ((Aeronave)comboAeronave.SelectedItem).getId, ((Ruta)comboRuta.SelectedItem).getId, dateTimeFechaSalida.Value, dateTimeFechaLlegada.Value, dateTimeFechaLlegada.Value), ((Aeronave)comboAeronave.SelectedItem), ((Ruta)comboRuta.SelectedItem));
                MessageBox.Show(null, "Se ha creado el viaje", "Generar Viaje");
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
