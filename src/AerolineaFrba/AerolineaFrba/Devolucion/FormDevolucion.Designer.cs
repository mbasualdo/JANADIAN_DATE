namespace AerolineaFrba.Devolucion
{
    partial class FormDevolucion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridRol1 = new System.Windows.Forms.DataGridView();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelFuncionalidad = new System.Windows.Forms.Label();
            this.labelCodigo = new System.Windows.Forms.Label();
            this.textMotivo = new System.Windows.Forms.TextBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.textBoxCompra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(155, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cancelaciones / Devoluciones";
            // 
            // dataGridRol1
            // 
            this.dataGridRol1.AllowUserToAddRows = false;
            this.dataGridRol1.AllowUserToDeleteRows = false;
            this.dataGridRol1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRol1.Location = new System.Drawing.Point(36, 207);
            this.dataGridRol1.Name = "dataGridRol1";
            this.dataGridRol1.ReadOnly = true;
            this.dataGridRol1.Size = new System.Drawing.Size(552, 178);
            this.dataGridRol1.TabIndex = 15;
            this.dataGridRol1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRol1_CellContentClick);
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(453, 166);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(135, 23);
            this.buttonBuscar.TabIndex = 14;
            this.buttonBuscar.Text = "Cancelar / Devolver";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(36, 166);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 13;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCompra);
            this.groupBox1.Controls.Add(this.checkBoxAll);
            this.groupBox1.Controls.Add(this.labelFuncionalidad);
            this.groupBox1.Controls.Add(this.labelCodigo);
            this.groupBox1.Controls.Add(this.textMotivo);
            this.groupBox1.Location = new System.Drawing.Point(36, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 109);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de bùsqueda";
            // 
            // labelFuncionalidad
            // 
            this.labelFuncionalidad.AutoSize = true;
            this.labelFuncionalidad.Location = new System.Drawing.Point(142, 26);
            this.labelFuncionalidad.Name = "labelFuncionalidad";
            this.labelFuncionalidad.Size = new System.Drawing.Size(131, 13);
            this.labelFuncionalidad.TabIndex = 21;
            this.labelFuncionalidad.Text = "Numero de compra / PNR";
            // 
            // labelCodigo
            // 
            this.labelCodigo.AutoSize = true;
            this.labelCodigo.Location = new System.Drawing.Point(142, 61);
            this.labelCodigo.Name = "labelCodigo";
            this.labelCodigo.Size = new System.Drawing.Size(39, 13);
            this.labelCodigo.TabIndex = 19;
            this.labelCodigo.Text = "Motivo";
            // 
            // textMotivo
            // 
            this.textMotivo.Location = new System.Drawing.Point(245, 54);
            this.textMotivo.Name = "textMotivo";
            this.textMotivo.Size = new System.Drawing.Size(185, 20);
            this.textMotivo.TabIndex = 18;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(145, 86);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAll.Size = new System.Drawing.Size(141, 17);
            this.checkBoxAll.TabIndex = 22;
            this.checkBoxAll.Text = "Cancelar toda la compra";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            // 
            // textBoxCompra
            // 
            this.textBoxCompra.Location = new System.Drawing.Point(279, 22);
            this.textBoxCompra.Name = "textBoxCompra";
            this.textBoxCompra.Size = new System.Drawing.Size(151, 20);
            this.textBoxCompra.TabIndex = 23;
            // 
            // FormDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 460);
            this.Controls.Add(this.dataGridRol1);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormDevolucion";
            this.Text = "Cancelacion/devolucion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridRol1;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelFuncionalidad;
        private System.Windows.Forms.Label labelCodigo;
        private System.Windows.Forms.TextBox textMotivo;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.TextBox textBoxCompra;
    }
}