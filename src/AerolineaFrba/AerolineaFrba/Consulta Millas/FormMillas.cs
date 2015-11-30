using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Consulta_Millas
{
    public partial class FormMillas : Form
    {
        public FormMillas()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backClick();

        }
        private void backClick()
        {
            this.Owner.Show();
            this.Hide();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            textBoxDni.Clear();
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
            labelPuntos.Text = "Puntos acumulados disponibles";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                String textoError = "";
                if (textBoxDni.Text == null || textBoxDni.Text.Trim() == "")
                {
                    textoError += "El campo DNI es obligatorio\n";
                }
                else
                {
                    Decimal value;
                    if (!Decimal.TryParse(textBoxDni.Text, out value))
                    {
                        textoError += "El campo DNI no es valido\n";
                    }
                }

                Cliente cliente=null;
                if (textoError.CompareTo("")==0)
                {
                    cliente = JanadianDateDB.Instance.getCliente(Convert.ToDecimal(textBoxDni.Text));
               }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                String query = String.Format("( SELECT m.Fecha as Fecha,m.Cantidad,m.Motivo from [JANADIAN_DATE].[Canje] m INNER JOIN [JANADIAN_DATE].[Cliente]  c ON (m.Cliente=c.Id)  WHERE c.dni =  {0}  UNION  SELECT m.Fecha as Fecha,m.Cantidad,m.Motivo from [JANADIAN_DATE].[Millas] m INNER JOIN [JANADIAN_DATE].[Cliente]  c ON (m.Cliente=c.Id)  WHERE c.dni = {0}   ) ORDER BY Fecha desc  ", textBoxDni.Text);
                Console.WriteLine(query);
                //MessageBox.Show(null, query, "Query");
                int millas = JanadianDateDB.Instance.getMillasTotalesDisponibles(cliente);
                labelPuntos.Text = "Puntos acumulados disponibles: " + millas.ToString();

                dataGridRol1.Columns.Clear();
                dataGridRol1.DataSource = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);


            }
           catch (Exception exAlta)
           {
               Console.WriteLine(exAlta.ToString());

               MessageBox.Show(null, "Intente de nuevo", "Error");
               return;
           }
        }
    }
}
