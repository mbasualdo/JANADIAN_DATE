using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class FormCompra : Form
    {
        private Usuario usuario;
        public FormCompra()
        {
            InitializeComponent();
        }

        public FormCompra(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();

        }

        private void FormCompra_Load(object sender, EventArgs e)
        {
            List<String> ciudades = JanadianDateDB.Instance.getCiudades();

            foreach (String f in ciudades)
            {
                comboOrigen.Items.Add(f);
                comboDestino.Items.Add(f);
            }
            dateTimeFechaSalida.MinDate = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            dateTimeFechaSalida.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);

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
            dateTimeFechaSalida.Value = JanadianDateDB.Instance.getFechaSistema().AddDays(1);
            comboOrigen.SelectedItem = null;
            comboDestino.SelectedItem = null;
            dataGridRol1.DataSource = null;
            dataGridRol1.Columns.Clear();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                String textoError = "";

                if (comboDestino.SelectedItem== null )
                {
                    textoError += "El campo Destino es obligatorio\n";
                }
                if (comboOrigen.SelectedItem == null)
                {
                    textoError += "El campo Origen es obligatorio\n";
                }
                if (comboOrigen.SelectedItem == comboDestino.SelectedItem)
                {
                    textoError += "El campo Origen debe ser diferente al campo Destino\n";
                }
                if (dateTimeFechaSalida.Value<=JanadianDateDB.Instance.getFechaSistema())
                {
                    textoError += "El campo Fecha salida debe ser mayor al dia de hoy \n";
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                string query = string.Format(" SELECT * FROM JANADIAN_DATE.Viaje_Disponible where  CONVERT(DATE,FechaSalida ) = '{0:dd/MM/yyyy}' AND Origen='{1}' AND Destino='{2}'", dateTimeFechaSalida.Value.Date, comboOrigen.SelectedItem.ToString(), comboDestino.SelectedItem.ToString());
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
                else
                {
                    MessageBox.Show(null, "No hay pasajes/paquetes disponibles para la busqueda", "Compra");
                }

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
                if (numericUpDownKG.Value <= 0 && numericUpDownPax.Value<=0)
                {
                    MessageBox.Show(null, "No ingreso cantidad de pasajes/kg a enviar", "Compra");
                    return;
                }

                if (numericUpDownKG.Value > Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["KG_Disponibles"].Value))
                {
                    MessageBox.Show(null, "La cantidad de kg a enviar es mayor a los disponibles", "Compra");
                    return;
                }
                if (numericUpDownPax.Value > Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["Butacas_Libres"].Value))
                {
                    MessageBox.Show(null, "La cantidad de butacas seleccionadas es mayor a las libres", "Compra");
                    return;
                }
//                ComprarViaje frm = new ComprarViaje(usuario, Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Viaje"].Value), numericUpDownKG.Value, numericUpDownPax.Value,"Pasajero 1");
                
                List<Cliente> clientes = new List<Cliente>();
                List<Butaca> butacas = new List<Butaca>();
                List<Decimal> paquetes = new List<Decimal>();

                if (numericUpDownKG.Value > 0) {
                    ComprarViaje frm = new ComprarViaje(usuario, Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Viaje"].Value), Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Aeronave"].Value), numericUpDownKG.Value, 0, "Datos para enviar Encomienda", Convert.ToDateTime(dataGridRol1.Rows[e.RowIndex].Cells["FechaSalida"].Value));
                        DialogResult result = frm.ShowDialog(this);
                        if (result == DialogResult.OK )
                        {
                            // fill other values
                            clientes.Add(frm.getCliente);
                            paquetes.Add(numericUpDownKG.Value);
                            butacas.Add(null);
                        }

                    }
                if (numericUpDownPax.Value > 0)
                {
                    for (int i = 0; i < numericUpDownPax.Value; i++)
                    {

                        ComprarViaje frm = new ComprarViaje(usuario, Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Viaje"].Value), Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Aeronave"].Value), 0, numericUpDownPax.Value, "Datos pasaje" + (i + 1).ToString(), Convert.ToDateTime(dataGridRol1.Rows[e.RowIndex].Cells["FechaSalida"].Value));
                        DialogResult result = frm.ShowDialog(this);
                        if (result == DialogResult.OK)
                        {
                            // fill other values
                            clientes.Add(frm.getCliente);
                            butacas.Add(frm.getButaca);
                            paquetes.Add(0);
                        }
                    }
                }


                PagoCompra frmPago = new PagoCompra(usuario, Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Viaje"].Value), Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Aeronave"].Value), clientes,butacas,paquetes);
                DialogResult resultPago = frmPago.ShowDialog(this);
                if (resultPago == DialogResult.OK)
                {
                    // fill other values
                   // clientes.Add(frmPago.getCliente);
                    paquetes.Add(numericUpDownKG.Value);
                    butacas.Add(null);
                }
                this.limpiar();
                }
            }
    }
}
