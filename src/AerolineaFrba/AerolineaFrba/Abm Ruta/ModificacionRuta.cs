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
    public partial class ModificacionRuta : Form
    {
        private Ruta rutaSel;
        public ModificacionRuta()
        {
            InitializeComponent();
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }
        }
        public ModificacionRuta(Ruta rutaSel)
        {
            // TODO: Complete member initialization
            this.rutaSel = rutaSel;
            InitializeComponent();
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
            checkBoxHabilitado.Checked = false;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";
                if (textNombre.Text == null || textNombre.Text.Trim() == "")
                {
                    textoError += "El campo nombre es obligatorio\n";
                }
                else
                {
                    rutaSel.setNombre(JanadianDateDB.RemoveSpecialCharacters(textNombre.Text));
                }
                if (listBoxFuncionalidades.SelectedItems.Count == 0)
                {
                    textoError += "El campo Funcionalidades es obligatorio\n";
                }
                else
                {
                    List<String> funcionalidades = new List<String>();
                    foreach (String f in listBoxFuncionalidades.SelectedItems)
                    {
                        funcionalidades.Add(f);
                    }
                    rutaSel.setFuncionalidades(funcionalidades);
                }
                rutaSel.setHabilitado(checkBoxHabilitado.Checked);
                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.modificarRol(rutaSel);
                MessageBox.Show(null, String.Format("Se ha modificado correctamente el Rol con Id {0}", rutaSel.getId), "Modificacion de Rol");
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

        private void ModificacionRuta_Load(object sender, EventArgs e)
        {
            textNombre.Text = rutaSel.getNombre;
            checkBoxHabilitado.Checked = rutaSel.getHabilitado;

            foreach (String c in rutaSel.getFuncionalidades)
            {
                listBoxFuncionalidades.SelectedItems.Add(c);
            }
        }
    }
}
