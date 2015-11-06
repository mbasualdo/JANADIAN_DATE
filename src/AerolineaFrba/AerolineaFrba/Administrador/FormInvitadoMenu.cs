using AerolineaFrba.Compra;
using AerolineaFrba.Consulta_Millas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Administrador
{
    public partial class FormInvitadoMenu : Form
    {
                private System.Windows.Forms.LinkLabel linkLabel1;

        public FormInvitadoMenu()
        {
            InitializeComponent();
        }

        private void labelBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void FormInvitadoMenu_Load(object sender, EventArgs e)
        {
                    
            try
            {
                List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidadesInvitado();

                // Create the LinkLabel.
                this.linkLabel1 = new System.Windows.Forms.LinkLabel();

                // Configure the LinkLabel's size and location. Specify that the
                // size should be automatically determined by the content.
                this.linkLabel1.Location = new System.Drawing.Point(34, 56);
                this.linkLabel1.Size = new System.Drawing.Size(224, 16);
                this.linkLabel1.AutoSize = true;


                this.linkLabel1.TabIndex = 0;
                this.linkLabel1.TabStop = true;

                // Add an event handler to do something when the links are clicked.
                this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);

                this.linkLabel1.Text = "";
                int i = 0;
                foreach (String func in funcionalidades)
                {
                    this.linkLabel1.Text += ("\n\n" + func);
                    this.linkLabel1.Links.Add(i, ("\n\n" + func).Length, func);
                    i += ("\n\n" + func).Length;
                }

                this.Controls.AddRange(new System.Windows.Forms.Control[] { this.linkLabel1 });
            }
            catch {
                MessageBox.Show(null, "Error al traer las funcionalidades", "Menu Invitado");
                return;
            }
        }
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

            // Determine which link was clicked within the LinkLabel.
            this.linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;

            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            if (target.Equals("COMPRA_PASAJE_ENCOMIENDA")){
                NavigateToForm(new FormCompra());
            }
             if (target.Equals("CONSULTA_MILLAS")){
                 NavigateToForm(new FormMillas());
            }

        }

        private void NavigateToForm(Form frm)
        {
            frm.Show(this);
            this.Hide();
        }
    }
}
