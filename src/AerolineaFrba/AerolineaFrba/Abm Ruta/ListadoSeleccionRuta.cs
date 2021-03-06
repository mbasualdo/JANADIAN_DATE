﻿using AerolineaFrba.Excepciones;
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
    public partial class ListadoSeleccionRuta : Form
    {
        public ListadoSeleccionRuta()
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

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {

            textId.Text = "";
            textCodigo.Text = "";
            numericUpDownKG.Value = 0.00M;
            numericUpDownPasaje.Value = 0.00M;
            comboOrigen.SelectedItem = null;
            comboDestino.SelectedItem = null;
            comboBoxTipoServicio.SelectedItem = null;
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
            checkBoxHabilitado.Checked = true;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT r.Id, r.Codigo,r.Precio_BaseKG,r.Precio_BasePasaje,o.Nombre as Origen,d.Nombre as Destino,t.Nombre as Tipo_Servicio,r.Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Ruta] r INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] o on (r.Ciudad_Origen=o.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] d on (r.Ciudad_Destino=d.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t on (r.Tipo_Servicio=t.Id)  ";
                bool conditions = false;
                if (comboOrigen.Text != null && comboOrigen.Text.Trim() != "")
                {
                    query += String.Format(" WHERE o.Nombre='{0}'", comboOrigen.Text);
                    conditions = true;
                }
                if (comboDestino.Text != null && comboDestino.Text.Trim() != "")
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
                    query += String.Format(andText + "  d.Nombre='{0}'", comboDestino.Text);
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
                    query += String.Format(andText + " r.Codigo like '%{0}%'", textCodigo.Text);
                }
                if (numericUpDownPasaje.Text != null && numericUpDownPasaje.Text.Trim() != "" && numericUpDownPasaje.Value>0)
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
                    query += String.Format(andText + " r.Precio_BasePasaje={0:0.00}", numericUpDownPasaje.Value.ToString().Replace(",", "."));
                }
                if (numericUpDownKG.Text != null && numericUpDownKG.Text.Trim() != "" && numericUpDownKG.Value > 0)
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
                    query += String.Format(andText + " r.Precio_BaseKG={0:0.00}", numericUpDownKG.Value.ToString().Replace(",", "."));
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
                DataGridViewButtonColumn columnSave = new DataGridViewButtonColumn();

                // Set column values
                columnSave.Name = "buttonSelection";
                columnSave.HeaderText = "Seleccionar";
                dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnSave);

            }
            catch (Exception excep)
            {
                Console.WriteLine(excep.ToString());
                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridRol1.Columns["buttonSelection"].Index && e.RowIndex >= 0)
            {
                Ruta rutaSel = new Ruta(Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Id"].Value), Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Origen"].Value), Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Destino"].Value), Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["Codigo"].Value), Convert.ToDouble(dataGridRol1.Rows[e.RowIndex].Cells["Precio_BaseKG"].Value), Convert.ToDouble(dataGridRol1.Rows[e.RowIndex].Cells["Precio_BasePasaje"].Value), Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Tipo_Servicio"].Value), Convert.ToBoolean(dataGridRol1.Rows[e.RowIndex].Cells["Habilitado"].Value));
                Form frm = new ModificacionRuta(rutaSel);
                frm.Show(this);
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
    }
}
