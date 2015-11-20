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
    public partial class PagoCompra : Form
    {
        private Usuario usuario;
        private int viaje;
        private int aeronave;
        private List<Cliente> clientes;
        private List<Butaca> butacas;
        private List<decimal> paquetes;
        private Cliente cliente;
        private Double pago;
        
        public PagoCompra()
        {
        }

        public PagoCompra(Usuario usuario, int viaje, int aeronave, List<Cliente> clientes, List<Butaca> butacas, List<decimal> paquetes)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.usuario = usuario;
            this.viaje = viaje;
            this.aeronave = aeronave;
            this.clientes = clientes;
            this.butacas = butacas;
            this.paquetes = paquetes;
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

                
            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private void PagoCompra_Load(object sender, EventArgs e)
        {
            //SI ES KIOSCO SOLO PUEDE PAGAR CON TARJETA PERO SI LO HACE UN ADMIN PUEDE HACERLO EN EFECTIVO
            comboBoxFormaPago.Items.Add("TC");

            if(usuario!=null){
                  comboBoxFormaPago.Items.Add("EFECTIVO");
            }

            dateTimePickerVenc.Format = DateTimePickerFormat.Custom;

            this.dateTimePickerVenc.CustomFormat = "MMyy";

            //  this.dateTimeFecha.Width = 1000;
            this.dateTimePickerVenc.ShowUpDown = true;

            //calculo el monto a pagar para mostrarlo

            labelPago.Text = "$" + this.calcularPago().ToString();
        }



        private void comboBoxFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFormaPago.SelectedItem.ToString().CompareTo("TC")==0)
            {
                groupBox2.Visible = true;
            }
            else {
                groupBox2.Visible = false;
            }
        }

        private void buttonPagar_Click(object sender, EventArgs e)
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

                if (comboBoxFormaPago.SelectedItem.ToString().Trim() == "")
                
                {
                    textoError += "La forma de pago es obligatoria\n";

                }

                if(comboBoxFormaPago.SelectedItem.ToString().CompareTo("TC")==0){


                if (textBoxNumeroTarj.Text == null || textBoxNumeroTarj.Text.Trim() == "")
                {
                    textoError += "El campo numero de tarjeta es obligatorio\n";
                }
                else
                {
                    Decimal value;
                    if (!Decimal.TryParse(textBoxNumeroTarj.Text, out value))
                    {
                        textoError += "El campo tarjeta no es valido";
                    }

                }
                if (textBoxCodTarj.Text == null || textBoxCodTarj.Text.Trim() == "")
                {
                    textoError += "El campo numero de seguridad es obligatorio\n";
                }
                else
                {
                    Decimal value;
                    if (!Decimal.TryParse(textBoxCodTarj.Text, out value))
                    {
                        textoError += "El campo codigo seguridad no es valido";
                    }

                }
                if (textBoxTipoTarj.Text == null || textBoxTipoTarj.Text.Trim() == "")
                {
                    textoError += "El campo tipo de tarjeta es obligatorio\n";
                }

                if (dateTimePickerVenc.Value == null)
                {
                    textoError += "El campo fecha de vencimiento es obligatorio\n";
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }
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

                String dataCompra = this.guardarCompra();

                MessageBox.Show(null, "Se ha confirmado la compra\n", "Compra");


            }
            catch (Exception exAlta)
            {
                Console.WriteLine(exAlta.ToString());

                MessageBox.Show(null, "Intente de nuevo", "Error");
                return;
            }
        }

        private string guardarCompra()
        {
            String textoRetorno = "\n";

            DateTime fecha = JanadianDateDB.Instance.getFechaSistema();
            Viaje v = JanadianDateDB.Instance.getViajeById(viaje);
            Ruta r = JanadianDateDB.Instance.getRutaById(v.getRuta);
            int idCompra = JanadianDateDB.Instance.insertarCompra(new CompraDB(0, this.pago, fecha, this.viaje, comboBoxFormaPago.SelectedItem.ToString(), cliente.getId));
            JanadianDateDB.Instance.insertarPaquetes(this.paquetes,this.clientes,idCompra,v,r);
            JanadianDateDB.Instance.insertarPasajes(this.butacas, this.clientes, idCompra, v, r);

            return textoRetorno;
        }

        private void buttonLimpiarTarjeta_Click(object sender, EventArgs e)
        {
            textBoxCodTarj.Text="";
            textBoxNumeroTarj.Text = "";
            textBoxTipoTarj.Text = "";
        }
        private double calcularPago()
        {
            double val = 0;

           Viaje v= JanadianDateDB.Instance.getViajeById(viaje);
           Ruta r = JanadianDateDB.Instance.getRutaById(v.getRuta);

           foreach (Butaca b in butacas)
           {
               if (b != null)
               {
                   val = (val + (r.getPrecio_BasePasaje));
               }
           }
           foreach (decimal p in paquetes)
           {
               if ( p > 0)
               {
                   val = (val + (Convert.ToDouble(p) * r.getPrecio_BaseKG));
               }
           }

           this.pago = val;
           return pago;
        }


    }
}
