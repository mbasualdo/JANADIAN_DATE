using AerolineaFrba.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class ListadoRol : Form
    {

        public ListadoRol()
        {
            InitializeComponent();
            List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidades();

            foreach (String f in funcionalidades)
            {
                comboFuncionalidad.Items.Add(f);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListadoRol_Load(object sender, EventArgs e)
        {

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textId.Text = "";
            textNombre.Text = "";
            comboFuncionalidad.Text = "";
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
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
                    foreach(String f in func){
                        combrol.Items.Add(f);
                    }
                    if (combrol.Items.Count>0)
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

        //private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //bool validRow = (e.RowIndex != -1); //Make sure the clicked row isn't the header.
        //var datagridview = sender as DataGridView;

        //// Check to make sure the cell clicked is the cell containing the combobox 
        //if(datagridview.Columns["comboFuncionalidadRol"] is DataGridViewComboBoxColumn && validRow)
        //{
        //    dataGridRol1.BeginEdit(true);
        //         ((ComboBox)dataGridRol1.EditingControl).DroppedDown = true;
        //    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
        //}
        //}
        //        this.dataGridRol1 = new DataGridView();
        //this.dataGridRol1.DataSource = JanadianDateDB.Instance.getRoles(dataGridRol1);
        //this.dataGridRol1.Location = new System.Drawing.Point(10, 50);
        //this.dataGridRol1.Size = new System.Drawing.Size(500, 300);
        //this.Controls.AddRange(new System.Windows.Forms.Control[] { this.dataGridRol1 });
    }
}
