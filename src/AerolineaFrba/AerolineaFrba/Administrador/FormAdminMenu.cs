using AerolineaFrba.Abm_Aeronave;
using AerolineaFrba.Abm_Rol;
using AerolineaFrba.Abm_Ruta;
using AerolineaFrba.Canje_Millas;
using AerolineaFrba.Compra;
using AerolineaFrba.Consulta_Millas;
using AerolineaFrba.Devolucion;
using AerolineaFrba.Generacion_Viaje;
using AerolineaFrba.Listado_Estadistico;
using AerolineaFrba.Registro_Llegada_Destino;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    public partial class FormAdminMenu : Form
    {
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Usuario userLogin;

        public FormAdminMenu()
        {
            InitializeComponent();
        }

        public FormAdminMenu(Usuario userLogin)
        {
            this.userLogin = userLogin;
            InitializeComponent();

        }

        private void labelBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminMenu_Load(object sender, EventArgs e)
        {
            try
            {
                List<String> funcionalidades = JanadianDateDB.Instance.getFuncionalidadesAdmin();

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
                MessageBox.Show(null, "Error al traer las funcionalidades", "Menu Administrador");
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

            if (target.Equals("ABM_ROL")){
                NavigateToForm(new FormRol());
            }
            if (target.Equals("ABM_RUTA_AEREA")){
                NavigateToForm(new FormRuta());
            }
            if (target.Equals("ABM_AERONAVE")){
                NavigateToForm(new FormAeronave());
            }
             if (target.Equals("GENERAR_VIAJE")){
                 NavigateToForm(new FormViaje());
            }
             if (target.Equals("REGISTRO_LLEGADA_DESTINO")){
                 NavigateToForm(new FormLlegada());
            }
            if (target.Equals("COMPRA_PASAJE_ENCOMIENDA")){
                NavigateToForm(new FormCompra(this.userLogin));
            }
            if (target.Equals("CANCELACION_DEVOLUCION")){
                NavigateToForm(new FormDevolucion());
            }
             if (target.Equals("CONSULTA_MILLAS")){
                 NavigateToForm(new FormMillas());
            }
             if (target.Equals("CANJE_MILLAS")){
                 NavigateToForm(new FormCanje());
            }
             if (target.Equals("ESTADISTICAS")){
                 NavigateToForm(new FormEstadistica());
            }
        }

        private void NavigateToForm(Form frm)
        {
            frm.Show(this);
            this.Hide();
        }


    }
}
