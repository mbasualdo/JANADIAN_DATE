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
    public partial class ComprarViaje : Form
    {
        private Usuario usuario;
        private Cliente cliente;
        private Butaca butaca;
        private decimal kg;
        private int viaje;
        private int aeronave;
        private DateTime fechaSalida;
        private List<String> butacasReservadas;

        public Cliente getCliente
        {
            get { return cliente; }
        }
        public Usuario getUsuario
        {
            get { return usuario; }
        }
        public Butaca getButaca
        {
            get { return butaca; }
        }
        public ComprarViaje(Usuario usuario, int viaje, int aeronave, decimal kg, decimal pax, string label,DateTime fechaSalida,List<Butaca> butacasReservadas)
        {
            InitializeComponent();
            this.usuario = usuario;
            labelDatos.Text = label;
            this.kg = kg;
            this.viaje = viaje;
            this.aeronave = aeronave;
            this.fechaSalida = fechaSalida;
            this.butacasReservadas = new List<string>();
            foreach (Butaca b in butacasReservadas)
            {
                if (b != null)
                {
                    this.butacasReservadas.Add(b.getId.ToString());
                }
            }
        }

        private void ComprarViaje_Load(object sender, EventArgs e)
        {
            if (this.kg == 0)
            {
                dataGridRol1.Visible = true;
                labelButacas.Visible = true;
            }
            else
            {
                labelKG.Visible = true;
                textBoxKG.Visible = true;
                textBoxKG.Text = Convert.ToString(kg);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            validarDNI();
        }

        private Cliente validarDNI()
        {
            Cliente c = null;
            if (textBox1.Text != null && textBox1.Text.Trim() != "")
            {
                Decimal value;
                if (!Decimal.TryParse(textBox1.Text, out value))
                {
                    MessageBox.Show(null, "El campo DNI no es valido", "Error");
                    return null;
                }

                c = JanadianDateDB.Instance.getCliente(Convert.ToDecimal(textBox1.Text.Trim()));

                if (c != null)
                {
                    textNombre.Text = c.getNombre;
                    textApellido.Text = c.getApellido;
                    textDireccion.Text = c.getDir;
                    textTelefono.Text = c.getTelefono.ToString();
                    textMail.Text = c.getMail;
                    dateTimeNacimiento.Value = c.getFechaNacimiento;

                    return c;
                }
            }
            return c;
        }
        private Cliente validarDNISinPisarDatos()
        {
            Cliente c = null;
            if (textBox1.Text != null && textBox1.Text.Trim() != "")
            {
                Decimal value;
                if (!Decimal.TryParse(textBox1.Text, out value))
                {
                    MessageBox.Show(null, "El campo DNI no es valido", "Error");
                    return null;
                }

                c = JanadianDateDB.Instance.getCliente(Convert.ToDecimal(textBox1.Text.Trim()));

            }
            return c;
        }
        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textNombre.Text = "";
            textApellido.Text = "";
            textDireccion.Text = "";
            textTelefono.Text = "";
            textMail.Text = "";
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String textoError = "";
                if (textNombre.Text == null || textNombre.Text.Trim() == "")
                {
                    textoError += "El campo Nombre es obligatorio\n";
                }
                if (textApellido.Text == null || textApellido.Text.Trim() == "")
                {
                    textoError += "El campo Apellido es obligatorio\n";
                }
                if (textDireccion.Text == null || textDireccion.Text.Trim() == "")
                {
                    textoError += "El campo Direccion es obligatorio\n";
                }
                if (textTelefono.Text == null || textTelefono.Text.Trim() == "")
                {
                    textoError += "El campo telefono es obligatorio\n";
                }
                else
                {
                    Decimal value;
                    if (!Decimal.TryParse(textTelefono.Text, out value))
                    {
                        textoError += "El campo telefono no es valido\n";
                    }
                }
                if (dateTimeNacimiento.Value == null || dateTimeNacimiento.Value > JanadianDateDB.Instance.getFechaSistema())
                {
                    textoError += "El campo Fecha de nacimiento es incorrecto\n";
                }
                Cliente c = validarDNISinPisarDatos();
                if (c == null)
                {
                    if (textBox1.Text == null || textBox1.Text.Trim() == "")
                    {
                        textoError += "El campo DNI es obligatorio\n";
                    }
                    else
                    {
                        Decimal value;
                        if (!Decimal.TryParse(textBox1.Text, out value))
                        {
                            textoError += "El campo DNI no es valido\n";
                        }

                    }
                }
                if (c != null && JanadianDateDB.Instance.getCantViajesFechaCliente(c, fechaSalida) > 0)
                {
                    textoError += "El Cliente ya posee viajes en la fecha del viaje que selecciono\n";
                }
                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
                if (c == null)
                {
                    JanadianDateDB.Instance.insertarCliente(new Cliente(Convert.ToDecimal(textBox1.Text.Trim()), Convert.ToString(textNombre.Text), Convert.ToString(textApellido.Text), Convert.ToString(textDireccion.Text), Convert.ToDecimal(textTelefono.Text), Convert.ToString(textMail.Text), Convert.ToDateTime(dateTimeNacimiento.Value)));
                }
                else
                {
                    JanadianDateDB.Instance.actualizarCliente(new Cliente(Convert.ToDecimal(textBox1.Text.Trim()), Convert.ToString(textNombre.Text), Convert.ToString(textApellido.Text), Convert.ToString(textDireccion.Text), Convert.ToDecimal(textTelefono.Text), Convert.ToString(textMail.Text), Convert.ToDateTime(dateTimeNacimiento.Value)));
                }
                this.cliente = JanadianDateDB.Instance.getCliente(Convert.ToDecimal(textBox1.Text.Trim()));



                MessageBox.Show(null, "Se han confirmado correctamente los datos del cliente", "Compra");


                if(kg>0){
                    //encomienda, luego de validar los datos se retorna para seguir con el siguiente
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }else{
                    //pasaje, luego de validar los datos se elige el asiento
                string query = string.Format(" SELECT  b.Id,b.Numero,b.Tipo FROM JANADIAN_DATE.Butaca b WHERE aeronave={0} AND b.Id not in ({1})", aeronave, string.Join(",",butacasReservadas.ToArray()));
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

                    // this.Close();

                }
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
                this.butaca = new Butaca(Convert.ToInt32(dataGridRol1.Rows[e.RowIndex].Cells["Id"].Value), Convert.ToDecimal(dataGridRol1.Rows[e.RowIndex].Cells["Numero"].Value), Convert.ToString(dataGridRol1.Rows[e.RowIndex].Cells["Tipo"].Value), 1, aeronave);
                MessageBox.Show(null, "Seleccion de butaca correcta", "Compra");
 
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
