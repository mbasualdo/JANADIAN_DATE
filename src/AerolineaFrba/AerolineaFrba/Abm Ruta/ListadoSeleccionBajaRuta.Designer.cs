﻿namespace AerolineaFrba.Abm_Ruta
{
    partial class ListadoSeleccionBajaRuta
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
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTipoServicio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboDestino = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFuncionalidad = new System.Windows.Forms.Label();
            this.comboOrigen = new System.Windows.Forms.ComboBox();
            this.labelCodigo = new System.Windows.Forms.Label();
            this.textCodigo = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.textId = new System.Windows.Forms.TextBox();
            this.numericUpDownKG = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPasaje = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPasaje)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridRol1
            // 
            this.dataGridRol1.AllowUserToAddRows = false;
            this.dataGridRol1.AllowUserToDeleteRows = false;
            this.dataGridRol1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRol1.Location = new System.Drawing.Point(12, 191);
            this.dataGridRol1.Name = "dataGridRol1";
            this.dataGridRol1.ReadOnly = true;
            this.dataGridRol1.Size = new System.Drawing.Size(552, 150);
            this.dataGridRol1.TabIndex = 15;
            this.dataGridRol1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRol1_CellContentClick);
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(489, 162);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 14;
            this.buttonBuscar.Text = "Buscar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(12, 162);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 13;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownKG);
            this.groupBox1.Controls.Add(this.numericUpDownPasaje);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxTipoServicio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboDestino);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelFuncionalidad);
            this.groupBox1.Controls.Add(this.comboOrigen);
            this.groupBox1.Controls.Add(this.labelCodigo);
            this.groupBox1.Controls.Add(this.textCodigo);
            this.groupBox1.Controls.Add(this.labelId);
            this.groupBox1.Controls.Add(this.textId);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 144);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de bùsqueda";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Tipo Servicio";
            // 
            // comboBoxTipoServicio
            // 
            this.comboBoxTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoServicio.FormattingEnabled = true;
            this.comboBoxTipoServicio.Location = new System.Drawing.Point(376, 81);
            this.comboBoxTipoServicio.Name = "comboBoxTipoServicio";
            this.comboBoxTipoServicio.Size = new System.Drawing.Size(151, 21);
            this.comboBoxTipoServicio.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Destino";
            // 
            // comboDestino
            // 
            this.comboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDestino.FormattingEnabled = true;
            this.comboDestino.Location = new System.Drawing.Point(376, 49);
            this.comboDestino.Name = "comboDestino";
            this.comboDestino.Size = new System.Drawing.Size(151, 21);
            this.comboDestino.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Precio Base KG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Precio Base Pasaje";
            // 
            // labelFuncionalidad
            // 
            this.labelFuncionalidad.AutoSize = true;
            this.labelFuncionalidad.Location = new System.Drawing.Point(291, 25);
            this.labelFuncionalidad.Name = "labelFuncionalidad";
            this.labelFuncionalidad.Size = new System.Drawing.Size(38, 13);
            this.labelFuncionalidad.TabIndex = 21;
            this.labelFuncionalidad.Text = "Origen";
            // 
            // comboOrigen
            // 
            this.comboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOrigen.FormattingEnabled = true;
            this.comboOrigen.Location = new System.Drawing.Point(376, 22);
            this.comboOrigen.Name = "comboOrigen";
            this.comboOrigen.Size = new System.Drawing.Size(151, 21);
            this.comboOrigen.TabIndex = 20;
            // 
            // labelCodigo
            // 
            this.labelCodigo.AutoSize = true;
            this.labelCodigo.Location = new System.Drawing.Point(11, 58);
            this.labelCodigo.Name = "labelCodigo";
            this.labelCodigo.Size = new System.Drawing.Size(40, 13);
            this.labelCodigo.TabIndex = 19;
            this.labelCodigo.Text = "Codigo";
            // 
            // textCodigo
            // 
            this.textCodigo.Location = new System.Drawing.Point(61, 55);
            this.textCodigo.Name = "textCodigo";
            this.textCodigo.Size = new System.Drawing.Size(185, 20);
            this.textCodigo.TabIndex = 18;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(33, 22);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(16, 13);
            this.labelId.TabIndex = 17;
            this.labelId.Text = "Id";
            // 
            // textId
            // 
            this.textId.Location = new System.Drawing.Point(61, 19);
            this.textId.Name = "textId";
            this.textId.Size = new System.Drawing.Size(185, 20);
            this.textId.TabIndex = 16;
            // 
            // numericUpDownKG
            // 
            this.numericUpDownKG.DecimalPlaces = 2;
            this.numericUpDownKG.Location = new System.Drawing.Point(116, 115);
            this.numericUpDownKG.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownKG.Name = "numericUpDownKG";
            this.numericUpDownKG.Size = new System.Drawing.Size(130, 20);
            this.numericUpDownKG.TabIndex = 38;
            // 
            // numericUpDownPasaje
            // 
            this.numericUpDownPasaje.DecimalPlaces = 2;
            this.numericUpDownPasaje.Location = new System.Drawing.Point(116, 89);
            this.numericUpDownPasaje.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPasaje.Name = "numericUpDownPasaje";
            this.numericUpDownPasaje.Size = new System.Drawing.Size(130, 20);
            this.numericUpDownPasaje.TabIndex = 37;
            // 
            // ListadoSeleccionBajaRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 460);
            this.Controls.Add(this.dataGridRol1);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListadoSeleccionBajaRuta";
            this.Text = "ListadoSeleccionBajaRuta";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPasaje)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridRol1;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTipoServicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFuncionalidad;
        private System.Windows.Forms.ComboBox comboOrigen;
        private System.Windows.Forms.Label labelCodigo;
        private System.Windows.Forms.TextBox textCodigo;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textId;
        private System.Windows.Forms.NumericUpDown numericUpDownKG;
        private System.Windows.Forms.NumericUpDown numericUpDownPasaje;
    }
}