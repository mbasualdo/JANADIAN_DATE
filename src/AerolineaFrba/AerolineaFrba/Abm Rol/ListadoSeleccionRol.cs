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
    public partial class ListadoSeleccionRol : Form
    {
        public ListadoSeleccionRol()
        {
            InitializeComponent();
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                comboFuncionalidad.Items.Add(f);
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textId.Text = "";
            textNombre.Text = "";
            comboFuncionalidad.Text = "";
            dataGridRol1.DataSource = null;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT r.Id, r.Nombre,r.Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Rol] r ";
                bool conditions = false;
                if (comboFuncionalidad.Text != null && comboFuncionalidad.Text.Trim() != "")
                {
                    query += String.Format(" INNER JOIN [GD2C2015].[JANADIAN_DATE].[Rol_Funcionalidad] rf ON (r.Id=rf.Rol)  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Funcionalidad] f ON (f.Id=rf.Funcionalidad) WHERE f.Descripcion='{0}'", comboFuncionalidad.Text);
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
                if (textNombre.Text != null && textNombre.Text.Trim() != "")
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
                    query += String.Format(andText + " r.Nombre like '%{0}%'", textNombre.Text);
                }
                Console.WriteLine(query);
                MessageBox.Show(null, query, "Query");

                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);
                // Create a  button column
                DataGridViewButtonColumn columnSave = new DataGridViewButtonColumn();

                // Set column values
                columnSave.Name = "buttonSelection";
                columnSave.HeaderText = "Seleccionar";
                dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnSave);

            }
            catch
            {
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridRol1.Columns["buttonSelection"].Index && e.RowIndex >= 0)
            {
             //   Form frm = new ModificacionRol(dataGridRol1.Rows[e.RowIndex].Cells[0].Value, dataGridRol1.Rows[e.RowIndex].Cells[1].Value, dataGridRol1.Rows[e.RowIndex].Cells[2].Value);
             //   frm.Show(this);
            }
        }
    }
}