using AerolineaFrba.Excepciones;
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
    public partial class ListadoRuta : Form
    {
        public ListadoRuta()
        {
            InitializeComponent();
            List<String> ciudades = JanadianDateDB.Instance.getCiudades();

            foreach (String f in ciudades)
            {
                comboOrigen.Items.Add(f);
                comboDestino.Items.Add(f);
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textId.Text = "";
            textCodigo.Text = "";
            textBoxKG.Text = "";
            textBoxPasaje.Text = "";
            comboOrigen.Text = "";
            comboDestino.Text = "";
            comboBoxTipoServicio.Text = "";
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
            checkBoxHabilitado.Checked = false;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT r.Id, r.Codigo,r.Precio_BaseKG,r.Precio_BasePasaje,r.Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Ruta] r ";
                bool conditions = false;
                if (comboOrigen.Text != null && comboOrigen.Text.Trim() != "")
                {
                    query += String.Format(" INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf ON (r.Id=rf.Rol)  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f ON (f.Id=rf.Funcionalidad) WHERE f.Descripcion='{0}'", comboOrigen.Text);
                    conditions = true;
                }
                if (textId.Text != null && textId.Text.Trim() != "")
                {
                    // bool isNumeric = Regex.IsMatch(textId.Text, @"^\d+$");
                    String andText = "";
                    if (conditions)
                    {
                        andText = " AND ";
                    }
                    else
                    {
                        query += String.Format(" WHERE ");
                        conditions = true;
                    }
                    query += String.Format(andText + " r.Id={0}", textId.Text);
                }
                if (textCodigo.Text != null && textCodigo.Text.Trim() != "")
                {
                    String andText = "";
                    if (conditions)
                    {
                        andText = " AND ";
                    }
                    else
                    {
                        query += String.Format(" WHERE ");
                        conditions = true;
                    }
                    query += String.Format(andText + " r.Nombre like '%{0}%'", textCodigo.Text);
                }
                if (checkBoxHabilitado.Checked)
                {
                    String andText = "";
                    if (conditions)
                    {
                        andText = " AND ";
                    }
                    else
                    {
                        query += String.Format(" WHERE ");
                        conditions = true;
                    }
                    query += String.Format(andText + " r.Habilitado = 1");
                }
                else
                {
                    String andText = "";
                    if (conditions)
                    {
                        andText = " AND ";
                    }
                    else
                    {
                        query += String.Format(" WHERE ");
                        conditions = true;
                    }
                    query += String.Format(andText + " r.Habilitado = 0");
                }
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");
                dataGridRol1.Columns.Clear();
                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);
                // Create a  button column
                DataGridViewComboBoxColumn columnSave = new DataGridViewComboBoxColumn();

                List<List<String>> funciones = new List<List<String>>();

                // Set column values
                columnSave.Name = "comboFuncionalidadRol";
                columnSave.HeaderText = "Funcionalidades";
                dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnSave);
                getFuncionalidadesRol();



            }
            catch (Exception en)
            {
                en.ToString();
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }
        private void getFuncionalidadesRol()
        {
            foreach (DataGridViewRow row in dataGridRol1.Rows)
            {
                try
                {
                    List<String> func = JanadianDateDB.Instance.getFuncionalidadesByRol(Convert.ToInt32(row.Cells["Id"].Value));
                    DataGridViewComboBoxCell combrol = (DataGridViewComboBoxCell)row.Cells["comboFuncionalidadRol"];
                    foreach (String f in func)
                    {
                        combrol.Items.Add(f);
                    }
                    if (combrol.Items.Count > 0)
                    {
                        combrol.Value = combrol.Items[0];
                    }
                }

                catch (NoResultsException err)
                {
                    err.ToString();

                }
            }
        }
    }
}
