namespace AerolineaFrba.Generacion_Viaje
{
    partial class FormViaje
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboRuta = new System.Windows.Forms.ComboBox();
            this.comboAeronave = new System.Windows.Forms.ComboBox();
            this.labelFuncionalidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimeFechaLlegada = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimeFechaLlegada);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimeFechaSalida);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboRuta);
            this.groupBox1.Controls.Add(this.comboAeronave);
            this.groupBox1.Controls.Add(this.labelFuncionalidad);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(586, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione su busqueda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(416, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Fecha Salida";
            // 
            // dateTimeFechaSalida
            // 
            this.dateTimeFechaSalida.CustomFormat = "MMMM dd, yyyy - dddd";
            this.dateTimeFechaSalida.Location = new System.Drawing.Point(349, 39);
            this.dateTimeFechaSalida.Name = "dateTimeFechaSalida";
            this.dateTimeFechaSalida.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFechaSalida.TabIndex = 18;
            this.dateTimeFechaSalida.Value = new System.DateTime(2015, 11, 10, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Aeronave";
            // 
            // comboRuta
            // 
            this.comboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRuta.FormattingEnabled = true;
            this.comboRuta.Location = new System.Drawing.Point(79, 36);
            this.comboRuta.Name = "comboRuta";
            this.comboRuta.Size = new System.Drawing.Size(264, 21);
            this.comboRuta.TabIndex = 14;
            // 
            // comboAeronave
            // 
            this.comboAeronave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAeronave.FormattingEnabled = true;
            this.comboAeronave.Location = new System.Drawing.Point(79, 94);
            this.comboAeronave.Name = "comboAeronave";
            this.comboAeronave.Size = new System.Drawing.Size(264, 21);
            this.comboAeronave.TabIndex = 16;
            // 
            // labelFuncionalidad
            // 
            this.labelFuncionalidad.AutoSize = true;
            this.labelFuncionalidad.Location = new System.Drawing.Point(20, 39);
            this.labelFuncionalidad.Name = "labelFuncionalidad";
            this.labelFuncionalidad.Size = new System.Drawing.Size(30, 13);
            this.labelFuncionalidad.TabIndex = 15;
            this.labelFuncionalidad.Text = "Ruta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(235, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Genere su viaje";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(12, 9);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(31, 13);
            this.linkLabel2.TabIndex = 7;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Atras";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Location = new System.Drawing.Point(501, 182);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 9;
            this.buttonBuscar.Text = "Generar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(24, 182);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 8;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Location = new System.Drawing.Point(377, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Fecha Llegada Estimada";
            // 
            // dateTimeFechaLlegada
            // 
            this.dateTimeFechaLlegada.CustomFormat = "MMMM dd, yyyy - dddd";
            this.dateTimeFechaLlegada.Location = new System.Drawing.Point(349, 95);
            this.dateTimeFechaLlegada.Name = "dateTimeFechaLlegada";
            this.dateTimeFechaLlegada.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFechaLlegada.TabIndex = 20;
            this.dateTimeFechaLlegada.Value = new System.DateTime(2015, 11, 10, 0, 0, 0, 0);
            // 
            // FormViaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 245);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormViaje";
            this.Text = "Generar Viaje";
            this.Load += new System.EventHandler(this.FormViaje_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboRuta;
        private System.Windows.Forms.ComboBox comboAeronave;
        private System.Windows.Forms.Label labelFuncionalidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeFechaSalida;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimeFechaLlegada;
    }
}