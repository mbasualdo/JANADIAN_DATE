namespace AerolineaFrba.Abm_Rol
{
    partial class ListadoSeleccionRol
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
            this.dataGridRol1 = new System.Windows.Forms.DataGridView();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelFuncionalidad = new System.Windows.Forms.Label();
            this.comboFuncionalidad = new System.Windows.Forms.ComboBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.textId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridRol1
            // 
            this.dataGridRol1.AllowUserToAddRows = false;
            this.dataGridRol1.AllowUserToDeleteRows = false;
            this.dataGridRol1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRol1.Location = new System.Drawing.Point(17, 219);
            this.dataGridRol1.Name = "dataGridRol1";
            this.dataGridRol1.ReadOnly = true;
            this.dataGridRol1.Size = new System.Drawing.Size(552, 150);
            this.dataGridRol1.TabIndex = 7;
            this.dataGridRol1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRol1_CellContentClick);
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(494, 165);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 6;
            this.buttonBuscar.Text = "Buscar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(17, 165);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 5;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelFuncionalidad);
            this.groupBox1.Controls.Add(this.comboFuncionalidad);
            this.groupBox1.Controls.Add(this.labelNombre);
            this.groupBox1.Controls.Add(this.textNombre);
            this.groupBox1.Controls.Add(this.labelId);
            this.groupBox1.Controls.Add(this.textId);
            this.groupBox1.Location = new System.Drawing.Point(17, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de bùsqueda";
            // 
            // labelFuncionalidad
            // 
            this.labelFuncionalidad.AutoSize = true;
            this.labelFuncionalidad.Location = new System.Drawing.Point(286, 29);
            this.labelFuncionalidad.Name = "labelFuncionalidad";
            this.labelFuncionalidad.Size = new System.Drawing.Size(73, 13);
            this.labelFuncionalidad.TabIndex = 5;
            this.labelFuncionalidad.Text = "Funcionalidad";
            // 
            // comboFuncionalidad
            // 
            this.comboFuncionalidad.FormattingEnabled = true;
            this.comboFuncionalidad.Location = new System.Drawing.Point(371, 26);
            this.comboFuncionalidad.Name = "comboFuncionalidad";
            this.comboFuncionalidad.Size = new System.Drawing.Size(151, 21);
            this.comboFuncionalidad.TabIndex = 4;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(6, 62);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(44, 13);
            this.labelNombre.TabIndex = 3;
            this.labelNombre.Text = "Nombre";
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(56, 59);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(185, 20);
            this.textNombre.TabIndex = 2;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(28, 26);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(16, 13);
            this.labelId.TabIndex = 1;
            this.labelId.Text = "Id";
            // 
            // textId
            // 
            this.textId.Location = new System.Drawing.Point(56, 23);
            this.textId.Name = "textId";
            this.textId.Size = new System.Drawing.Size(185, 20);
            this.textId.TabIndex = 0;
            // 
            // ListadoSeleccionRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 408);
            this.Controls.Add(this.dataGridRol1);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListadoSeleccionRol";
            this.Text = "ListadoSeleccionRol";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridRol1;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelFuncionalidad;
        private System.Windows.Forms.ComboBox comboFuncionalidad;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textId;
    }
}