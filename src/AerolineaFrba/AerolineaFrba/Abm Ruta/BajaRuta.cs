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
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }
        }
                public BajaRuta(Ruta rutaSel)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.rutaSel = rutaSel;
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void limpiarForm()
        {
            textNombre.Text = "";
            listBoxFuncionalidades.SelectedItems.Clear();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                JanadianDateDB.Instance.bajaLogicaRol(rutaSel.getId);
                MessageBox.Show(null, String.Format("Se ha dado de baja correctamente el Rol con Id {0}", rutaSel.getId), "Baja de Rol");
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
            textNombre.Text = rutaSel.getNombre;

            foreach (String s in rutaSel.getFuncionalidades)
            {
                listBoxFuncionalidades.SelectedItems.Add(s);
            }

        }
    }
}
