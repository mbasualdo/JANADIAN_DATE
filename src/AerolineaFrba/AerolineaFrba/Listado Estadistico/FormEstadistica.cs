using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Listado_Estadistico
{
    public partial class FormEstadistica : Form
    {
        public FormEstadistica()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String textoError = validarIngreso();

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("select top 5 count(*) as Pasajes,Destino  from JANADIAN_DATE.[Pasajes_Vendidos_Destino] WHERE Fecha_Compra>'{0}' AND Fecha_Compra<'{1}' GROUP by Destino ORDER BY Pasajes desc ", JanadianDateDB.Instance.generarFechaInicialSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value), JanadianDateDB.Instance.generarFechaFinalSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value));
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridView1, query);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private string validarIngreso()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            String textoError = "";
            if (comboBoxSemestre.SelectedItem == null || comboBoxSemestre.Text.Trim() == "")
            {
                textoError += "El campo Semestre es obligatorio\n";
            }

            if (dateTimePickerAnio.Value == null)
            {
                textoError += "El campo Año es obligatorio\n";
            }
            return textoError;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String textoError = validarIngreso();

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("select top 5 count(*) as Pasajes,Destino  from JANADIAN_DATE.[Pasajes_Vendidos_Destino] WHERE Fecha_Compra>'{0}' AND Fecha_Compra<'{1}' GROUP by Destino ORDER BY Pasajes desc ", JanadianDateDB.Instance.generarFechaInicialSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value), JanadianDateDB.Instance.generarFechaFinalSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value));
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridView1, query);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            try
            {
                String textoError = validarIngreso();

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("select TOP 5 [JANADIAN_DATE].[Millas_Disponibles](Id) as Acumulado,Nombre,Dni,Mail from JANADIAN_DATE.[Clientes_Millas] WHERE Fecha>'{0}' AND Fecha<'{1}' ORDER BY Acumulado DESC ", JanadianDateDB.Instance.generarFechaInicialSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value), JanadianDateDB.Instance.generarFechaFinalSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value));
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridView1, query);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String textoError = validarIngreso();

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("select top 5 count(*) as Pasajes,Destino  from JANADIAN_DATE.[Pasajes_Cancelados_Destino] WHERE Fecha_Compra>'{0}' AND Fecha_Compra<'{1}' GROUP by Destino ORDER BY Pasajes desc ", JanadianDateDB.Instance.generarFechaInicialSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value), JanadianDateDB.Instance.generarFechaFinalSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value));
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridView1, query);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                String textoError = validarIngreso();

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                String query = String.Format("SELECT top 5  SUM(DATEDIFF(DAY,Fecha_Baja,Fecha_Reinicio)) as DiasFuera,Matricula  from  JANADIAN_DATE.[Aeronaves_Fuera_Servicio]  WHERE Fecha_Baja>'{0}' AND Fecha_Baja<'{1}' group by Matricula  ORDER BY DiasFuera DESC ", JanadianDateDB.Instance.generarFechaInicialSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value), JanadianDateDB.Instance.generarFechaFinalSemestre(comboBoxSemestre.SelectedItem, dateTimePickerAnio.Value));
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridView1, query);
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Owner.Show();
            this.Hide();
        }

        private void FormEstadistica_Load(object sender, EventArgs e)
        {
            comboBoxSemestre.Items.Add("ENE-JUN");
            comboBoxSemestre.Items.Add("JUL-DIC");

             dateTimePickerAnio.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerAnio.CustomFormat = "yyyy";
            //  this.dateTimeFecha.Width = 1000;
            this.dateTimePickerAnio.ShowUpDown = true;
        }
    }
}
