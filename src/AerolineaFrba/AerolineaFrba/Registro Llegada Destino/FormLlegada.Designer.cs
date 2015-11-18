namespace AerolineaFrba.Registro_Llegada_Destino
{
    partial class FormLlegada
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
            this.dateTimeFechaLlegada = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboAeronave = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboDestino = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboOrigen = new System.Windows.Forms.ComboBox();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridRol1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimeFechaLlegada
            // 
            this.dateTimeFechaLlegada.CustomFormat = "MMMM dd, yyyy - dddd";
            this.dateTimeFechaLlegada.Location = new System.Drawing.Point(218, 118);
            this.dateTimeFechaLlegada.Name = "dateTimeFechaLlegada";
            this.dateTimeFechaLlegada.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFechaLlegada.TabIndex = 23;
            this.dateTimeFechaLlegada.Value = new System.DateTime(2015, 11, 10, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Matricula";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboAeronave
            // 
            this.comboAeronave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAeronave.FormattingEnabled = true;
            this.comboAeronave.Location = new System.Drawing.Point(218, 22);
            this.comboAeronave.Name = "comboAeronave";
            this.comboAeronave.Size = new System.Drawing.Size(131, 21);
            this.comboAeronave.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(146, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "Registre la llegada de la Aeronave";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboDestino);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboOrigen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboAeronave);
            this.groupBox1.Controls.Add(this.dateTimeFechaLlegada);
            this.groupBox1.Location = new System.Drawing.Point(35, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 166);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de viaje";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Fecha y hora Llegada";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Destino";
            // 
            // comboDestino
            // 
            this.comboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDestino.FormattingEnabled = true;
            this.comboDestino.Location = new System.Drawing.Point(218, 76);
            this.comboDestino.Name = "comboDestino";
            this.comboDestino.Size = new System.Drawing.Size(185, 21);
            this.comboDestino.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Origen";
            // 
            // comboOrigen
            // 
            this.comboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOrigen.FormattingEnabled = true;
            this.comboOrigen.Location = new System.Drawing.Point(218, 49);
            this.comboOrigen.Name = "comboOrigen";
            this.comboOrigen.Size = new System.Drawing.Size(185, 21);
            this.comboOrigen.TabIndex = 29;
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(35, 227);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 27;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(12, 17);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(31, 13);
            this.linkLabel2.TabIndex = 33;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Atras";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(448, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridRol1
            // 
            this.dataGridRol1.AllowUserToAddRows = false;
            this.dataGridRol1.AllowUserToDeleteRows = false;
            this.dataGridRol1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRol1.Location = new System.Drawing.Point(35, 280);
            this.dataGridRol1.Name = "dataGridRol1";
            this.dataGridRol1.ReadOnly = true;
            this.dataGridRol1.Size = new System.Drawing.Size(488, 107);
            this.dataGridRol1.TabIndex = 35;
            this.dataGridRol1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridRol1_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(212, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "Viajes de la aeronave";
            // 
            // FormLlegada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 428);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridRol1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormLlegada";
            this.Text = "Registro Llegada";
            this.Load += new System.EventHandler(this.FormLlegada_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRol1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeFechaLlegada;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboAeronave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboDestino;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboOrigen;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridRol1;
        private System.Windows.Forms.Label label5;
    }
}