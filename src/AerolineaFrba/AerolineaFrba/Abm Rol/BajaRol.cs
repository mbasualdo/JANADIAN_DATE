using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class BajaRol : Form
    {
        private Rol rolSel;

        public BajaRol()
        {
            InitializeComponent();
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }
        }

        public BajaRol(Rol rolSel)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.rolSel = rolSel;
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                JanadianDateDB.Instance.bajaLogicaRol(rolSel.getId);
                MessageBox.Show(null,String.Format("Se ha dado de baja correctamente el Rol con Id {0}", rolSel.getId), "Baja de Rol");
                limpiarForm();
                this.Close();
            }
            catch
            {
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
            textNombre.Text = "";
            listBoxFuncionalidades.SelectedItems.Clear();
        }

        private void BajaRol_Load(object sender, EventArgs e)
        {
            textNombre.Text = rolSel.getNombre;

            foreach (String s in rolSel.getFuncionalidades) {
                listBoxFuncionalidades.SelectedItems.Add(s);
            }

        }
    }
}
