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
        public BajaAeronave(Aeronave aeronaveSel)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.aeronaveSel = aeronaveSel;
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

                JanadianDateDB.Instance.bajaLogicaRuta(aeronaveSel.getId);
                MessageBox.Show(null, String.Format("Se ha dado de baja correctamente la ruta con Id {0}", aeronaveSel.getId), "Baja de Ruta");
                limpiarForm();
                this.Close();
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
            textCodigo.Text = aeronaveSel.getCodigo.ToString();
            textBoxKG.Text = aeronaveSel.getPrecio_BaseKG.ToString();
            textBoxPasaje.Text = aeronaveSel.getPrecio_BasePasaje.ToString();
            comboOrigen.SelectedItem = aeronaveSel.getOrigen;
            comboDestino.Text = aeronaveSel.getDestino;
            comboBoxTipoServicio.Text = aeronaveSel.getTipoServicio;
        }
    }
}
