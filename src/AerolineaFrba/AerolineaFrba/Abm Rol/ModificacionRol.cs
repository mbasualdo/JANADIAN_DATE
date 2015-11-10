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
    public partial class ModificacionRol : Form
    {
        private Rol rolSel;


        public ModificacionRol()
        {
            InitializeComponent();
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                listBoxFuncionalidades.Items.Add(f);
            }
        }

        public ModificacionRol(Rol rolSel)
        {
            // TODO: Complete member initialization
            this.rolSel = rolSel;
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
                if (JanadianDateDB.Instance.getRolByname(textNombre.Text) != null)
                {
                    textoError += "Ya existe un rol con este nombre\n";
                }
                if (listBoxFuncionalidades.SelectedItems.Count == 0)
                {
                    textoError += "El campo Funcionalidades es obligatorio\n";
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                JanadianDateDB.Instance.insertarRol(JanadianDateDB.RemoveSpecialCharacters(textNombre.Text), listBoxFuncionalidades.SelectedItems.Cast<string>().ToList());
                MessageBox.Show(null, "Se ha insertado correctamente el nuevo Rol", "Alta de Rol");
                limpiarForm();
            }
            catch
            {
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void ModificacionRol_Load(object sender, EventArgs e)
        {
            textNombre.Text = rolSel.getNombre;
           foreach(String c in rolSel.getFuncionalidades){
               listBoxFuncionalidades.SelectedItems.Add(c);
           }
        }

    }
}
