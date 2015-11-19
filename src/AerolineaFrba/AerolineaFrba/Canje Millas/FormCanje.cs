using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Canje_Millas
{
    public partial class FormCanje : Form
    {
        public FormCanje()
        {
            InitializeComponent();
            List<Producto> productos = JanadianDateDB.Instance.getProductos();

            foreach (Producto f in productos)
            {
                comboBoxProducto.Items.Add(f);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            comboBoxProducto.SelectedItem = null;
            numericUpDownCantidad.Value=1;
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
                if (comboBoxProducto.SelectedItem== null )
                {
                    textoError += "El campo Producto es obligatorio\n";
                }
                if (numericUpDownCantidad.Value <1)
                {
                    textoError += "El campo Cantidad debe ser positivo\n";
                }
                if (( comboBoxProducto.SelectedItem!=null && numericUpDownCantidad.Value > ((Producto) comboBoxProducto.SelectedItem).getStock))
                {
                    textoError += "No hay stock suficiente del producto solicitado\n";
                }

                Cliente cliente=null;
                if (textoError.CompareTo("") == 0)
                {
                    cliente = JanadianDateDB.Instance.getCliente(Convert.ToDecimal(textBoxDni.Text));
                    if (cliente!=null)
                    {
                       int millas = JanadianDateDB.Instance.getMillasTotalesDisponibles(cliente);
                       if (millas <= 0)
                       {
                           textoError += "No tiene millas disponibles para canjear\n";
                       }
                       else if ((numericUpDownCantidad.Value * (((Producto) comboBoxProducto.SelectedItem).getMillasNecesarias)) > millas)
                       {
                           textoError += "No tiene millas suficientes para canjear lo pedido\n";
                       }

                    }else{
                         textoError += "No hay clientes con el DNI ingresado\n";
                    }
                }

                if (textoError.Length != 0)
                {
                    MessageBox.Show(null, textoError, "Error de Validacion");
                    return;

                }

                JanadianDateDB.Instance.canjearMillas(cliente, (Producto)comboBoxProducto.SelectedItem,numericUpDownCantidad.Value);

                MessageBox.Show(null, "Se canjearon correctamente las millas", "Canje de millas");

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
