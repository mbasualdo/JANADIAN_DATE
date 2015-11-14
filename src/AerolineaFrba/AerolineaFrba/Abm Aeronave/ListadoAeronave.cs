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
    public partial class ListadoAeronave : Form
    {
        public ListadoAeronave()
        {
            InitializeComponent();
            List<String> fabricantes = JanadianDateDB.Instance.getFabricantes();

            foreach (String f in fabricantes)
            {
                comboFabricante.Items.Add(f);
            }
            List<String> tiposServicio = JanadianDateDB.Instance.getTiposServicio();

            foreach (String f in tiposServicio)
            {
                comboBoxTipoServicio.Items.Add(f);
            } 
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxId.Text = "";
            textMatricula.Text = "";
            textBoxModelo.Text = "";
            numericUpDownKG.Value = 0.00M;
            numericUpDownVentanilla.Value = 0.00M;
            numericUpDownPasillo.Value = 0.00M;
            comboFabricante.SelectedItem = null;
            comboBoxTipoServicio.SelectedItem = null;
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
            checkBoxHabilitado.Checked = true;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT a.Id,a.Modelo,a.Habilitado,a.Matricula,a.KG_Disponibles,t.Nombre as Tipo_Servicio,f.Nombre as Fabricante,a.Cant_Butacas_Ventanilla,a.Cant_Butacas_Pasillo FROM [GD2C2015].[JANADIAN_DATE].[Aeronave] a INNER JOIN [GD2C2015].[JANADIAN_DATE].[Fabricante] f on (a.Fabricante=f.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t on (a.Tipo_Servicio=t.Id)  ";
                bool conditions = false;
                if (comboFabricante.Text != null && comboFabricante.Text.Trim() != "")
                {
                    query += String.Format(" WHERE f.Nombre='{0}'", comboFabricante.Text);
                    conditions = true;
                }
                if (comboBoxTipoServicio.Text != null && comboBoxTipoServicio.Text.Trim() != "")
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
                    query += String.Format(andText + "  t.Nombre='{0}'", comboBoxTipoServicio.Text);
                    conditions = true;
                }
                if (textBoxId.Text != null && textBoxId.Text.Trim() != "")
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
                    query += String.Format(andText + " a.Id={0}", textBoxId.Text);
                }
                if (textBoxModelo.Text != null && textBoxModelo.Text.Trim() != "")
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
                    query += String.Format(andText + " a.Modelo like '%{0}%'", textBoxModelo.Text);
                }
                if (numericUpDownKG.Text != null && numericUpDownKG.Text.Trim() != "")
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
                    query += String.Format(andText + " a.KG_Disponibles={0}", numericUpDownKG.Text);
                }
                if (numericUpDownVentanilla.Text != null && numericUpDownVentanilla.Text.Trim() != "")
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
                    query += String.Format(andText + " a.Cant_Butacas_Ventanilla={0}", numericUpDownVentanilla.Text);
                }
                if (numericUpDownPasillo.Text != null && numericUpDownPasillo.Text.Trim() != "")
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
                    query += String.Format(andText + " a.Cant_Butacas_Pasillo={0}", numericUpDownPasillo.Text);
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
                    query += String.Format(andText + " a.Habilitado = 1");
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
                    query += String.Format(andText + " a.Habilitado = 0");
                }
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");
                dataGridRol1.Columns.Clear();
                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);

            }
            catch (Exception en)
            {
                en.ToString();
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }
    }
}
