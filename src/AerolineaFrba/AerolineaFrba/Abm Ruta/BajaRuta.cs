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
    public partial class BajaRuta : Form
    {
        private Ruta rutaSel;
        public BajaRuta()
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
                public BajaRuta(Ruta rutaSel)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.rutaSel = rutaSel;
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

                JanadianDateDB.Instance.bajaLogicaRuta(rutaSel.getId);
                MessageBox.Show(null, String.Format("Se ha dado de baja correctamente la ruta con Id {0}", rutaSel.getId), "Baja de Ruta");
                limpiarForm();
                this.Close();
            }
            catch
            {
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void BajaRuta_Load(object sender, EventArgs e)
        {
            textCodigo.Text = rutaSel.getCodigo.ToString();
            textBoxKG.Text = rutaSel.getPrecio_BaseKG.ToString();
            textBoxPasaje.Text = rutaSel.getPrecio_BasePasaje.ToString();
            comboOrigen.SelectedItem = rutaSel.getOrigen;
            comboDestino.Text = rutaSel.getDestino;
            comboBoxTipoServicio.Text = rutaSel.getTipoServicio;


        }
    }
}
