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

namespace AerolineaFrba.Abm_Rol
{
    public partial class ListadoSeleccionBajaRol : Form
    {
        public ListadoSeleccionBajaRol()
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
                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);
                // Create a  button column
                DataGridViewComboBoxColumn columnFunciones = new DataGridViewComboBoxColumn();

                List<List<String>> funciones = new List<List<String>>();

                // Set column values
                columnFunciones.Name = "comboFuncionalidadRol";
                columnFunciones.HeaderText = "Funcionalidades";
                dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnFunciones);
                getFuncionalidadesRol();

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
        private List<String> getFuncionalidadRol(DataGridViewCell dataGridViewCell)
        {
            List<String> func = new List<string>();

            try
            {
                DataGridViewComboBoxCell combrol = (DataGridViewComboBoxCell)dataGridViewCell;
                foreach (String f in combrol.Items)
                {
                    func.Add(f);
                }

            }

            catch (NoResultsException err)
            {
                err.ToString();

            }
            return func;
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
                    combrol.Value = combrol.Items[0];

                }

                catch (NoResultsException err)
                {
                    err.ToString();

                }
            }
        }

        private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridRol1.Columns["buttonSelection"].Index && e.RowIndex >= 0)
            {
                Rol rolSel = new Rol(Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Id"].Value), Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Nombre"].Value), getFuncionalidadRol(dataGridRol1.Rows[e.RowIndex].Cells["comboFuncionalidadRol"]), Convert.ToBoolean(dataGridRol1.Rows[e.RowIndex].Cells["Habilitado"].Value));
                Form frm = new BajaRol(rolSel);
                frm.Show(this);
            }
        }
    }
}
