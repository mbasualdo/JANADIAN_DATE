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

        public Cliente getCliente
        {
            get { return cliente; }
        }
        public Usuario getUsuario
        {
            get { return usuario; }
        }
        public ComprarViaje(Usuario usuario, int viaje, decimal kg, decimal pax, string label)
        {
            InitializeComponent();
            this.usuario = usuario;
            labelDatos.Text = label;
            this.kg = kg;
        }

        private void ComprarViaje_Load(object sender, EventArgs e)
        {
            if (this.kg == 0)
            {
                dataGridRol1.Visible = true;
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

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
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
                Cliente c = validarDNI();
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
                            textoError += "El campo DNI no es valido";
                        }

                    }
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

                    // this.Close();

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

        }
    }
}
