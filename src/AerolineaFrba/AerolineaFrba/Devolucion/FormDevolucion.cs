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

namespace AerolineaFrba.Devolucion
{
    public partial class FormDevolucion : Form
    {
        public FormDevolucion()
        {
            InitializeComponent();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            textMotivo.Text = "";
            textBoxCompra.Text = "";
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
            checkBoxAll.Checked = false;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";

                if (textBoxCompra.Text == null || textBoxCompra.Text.Trim() == "")
                {
                    textoError += "El campo PNR es obligatorio\n";
                }
                else
                {
                    Decimal value;
                    if (!Decimal.TryParse(textBoxCompra.Text, out value))
                    {
                        textoError += "El campo PNR no es valido\n";
                    }
                }
                if (textMotivo.Text == null || textMotivo.Text.Trim() == "")
                {
                    textoError += "El campo Motivo es obligatorio\n";
                }

                CompraDB compra = null;
                Viaje viaje = null;

                if (textoError.Length == 0)
                {

                    compra = JanadianDateDB.Instance.getCompraByPNR(textBoxCompra.Text.Trim());

                    if (compra == null)
                    {
                        textoError += "No existe la compra para las condiciones ingresadas\n";

                    }
                    else {
                        viaje = JanadianDateDB.Instance.getViajeById(compra.getViaje);

                        if (viaje == null)
                        {
                            textoError += "No existe el viaje para las condiciones ingresadas\n";

                        }
                        else if (viaje.getFechaSalida.CompareTo(JanadianDateDB.Instance.getFechaSistema())<0)
                        {
                            textoError += "El viaje ya ha salido. No se puede cancelar pasajes\n";
                        }
                        else if (viaje.getFechaLlegada.CompareTo(viaje.getFechaSalida) > 0)
                        {
                            textoError += "Ya se ha registrado la llegada del viaje asociado a esta compra\nNo se puede cancelar porque ya ha viajado\n";

                        }
                    }
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                if (checkBoxAll.Checked)
                {
                    DialogResult dialogResult1 = MessageBox.Show("Esta Seguro que desea cancelar todos los pasajes y paquetes de la compra", "Cancelacion/Devolucion", MessageBoxButtons.YesNo);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        int idCancelacion = JanadianDateDB.Instance.cancelarPasajesPaquetesDeCompra(compra.getPNR, textMotivo.Text);

                        MessageBox.Show(null, "Se han cancelado todos los pasajes/paquetes de la compra \nSe le devolvera en " + compra.getFormaPago + " el  pago realizado. \nCodigo de devolucion: " + idCancelacion.ToString(), "Cancelacion/Devolucion");
                        this.limpiar();
                    }
                }
                else
                {
                    string query = string.Format("SELECT PNR,codigo,Tipo,Viaje,Forma_Pago,Butaca,KG FROM JANADIAN_DATE.[Pasajes_Paquetes_Compra_Viaje] WHERE PNR={0}", compra.getPNR);
                    Console.WriteLine(query);
                    //MessageBox.Show(null, query, "Query");
                    dataGridRol1.Columns.Clear();
                    DataTable table = JanadianDateDB.Instance.getDataTableResults(dataGridRol1, query);

                    if (table.Rows.Count > 0)
                    {

                        dataGridRol1.DataSource = table;


                        // Create a  button column
                        DataGridViewButtonColumn columnSave = new DataGridViewButtonColumn();

                        // Set column values
                        columnSave.Name = "buttonSelection";
                        columnSave.HeaderText = "Seleccionar";
                        dataGridRol1.Columns.Insert(dataGridRol1.Columns.Count, columnSave);
                    }
                    else {
                        MessageBox.Show(null, "No hay pasajes/paquetes disponibles para cancelar de esta compra", "Cancelacion/Devolucion");
                    }
                }


            }
            catch (NoResultsException exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "No hay compras", "Error");
                return;
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void dataGridRol1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridRol1.Columns["buttonSelection"].Index && e.RowIndex >= 0)
            {
                if (Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Tipo"].Value).CompareTo("PASAJE") == 0)
                {
                    int idCancelacion = JanadianDateDB.Instance.cancelarPasajeCompra(textBoxCompra.Text, Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["Codigo"].Value), textMotivo.Text, Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Viaje"].Value));
                    MessageBox.Show(null, "Se ha cancelado el pasaje seleccionado\nSe le devolvera en " + dataGridRol1.Rows[e.RowIndex].Cells["Forma_Pago"].Value + " el  pago realizado por la butaca " + dataGridRol1.Rows[e.RowIndex].Cells["Butaca"].Value + " comprada.\nCodigo de devolucion: " + idCancelacion.ToString(), "Cancelacion/Devolucion");

                }
                else
                {
                    int idCancelacion = JanadianDateDB.Instance.cancelarPaqueteCompra(textBoxCompra.Text, Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["Codigo"].Value), textMotivo.Text);
                    MessageBox.Show(null, "Se ha cancelado el paquete seleccionado\nSe le devolvera en " + dataGridRol1.Rows[e.RowIndex].Cells["Forma_Pago"].Value + " el  pago realizado por los " + dataGridRol1.Rows[e.RowIndex].Cells["KG"].Value + " kg comprados.\nCodigo de devolucion: " + idCancelacion.ToString(), "Cancelacion/Devolucion");

                }

                this.limpiar();

            }
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
    }
}
