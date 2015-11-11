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
            List<String> tiposServicio = JanadianDateDB.Instance.getTposServicio();

            foreach (String f in tiposServicio)
            {
                comboBoxTipoServicio.Items.Add(f);
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
                String query = "SELECT r.Id, r.Codigo,r.Precio_BaseKG,r.Precio_BasePasaje,o.Nombre as Origen,d.Nombre as Destino,t.Nombre as Tipo_Servicio,r.Habilitado FROM [GD2C2015].[JANADIAN_DATE].[Ruta] r INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] o on (r.Ciudad_Origen=o.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] d on (r.Ciudad_Destino=d.Id) INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t on (r.Tipo_Servicio=t.Id)  ";
                bool conditions = false;
                if (comboOrigen.Text != null && comboOrigen.Text.Trim() != "")
                {
                    query += String.Format(" WHERE Origen='{0}'", comboOrigen.Text);
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
                    query += String.Format(andText + "  Destino='{0}'", comboDestino.Text);
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
                    query += String.Format(andText + "  Tipo_Servicio='{0}'", comboBoxTipoServicio.Text);
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
                if (textBoxPasaje.Text != null && textBoxPasaje.Text.Trim() != "")
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
                    query += String.Format(andText + " r.Precio_BasePasaje={0}", textBoxPasaje.Text);
                }
                if (textBoxKG.Text != null && textBoxKG.Text.Trim() != "")
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
                    query += String.Format(andText + " r.Precio_BaseKG={0}", textBoxKG.Text);
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
