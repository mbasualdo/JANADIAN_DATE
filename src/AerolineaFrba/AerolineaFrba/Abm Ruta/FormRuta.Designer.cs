﻿namespace AerolineaFrba.Abm_Ruta
{
    partial class FormRuta
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
            this.linkBaja = new System.Windows.Forms.LinkLabel();
            this.linkModificacion = new System.Windows.Forms.LinkLabel();
            this.linkListado = new System.Windows.Forms.LinkLabel();
            this.linkAlta = new System.Windows.Forms.LinkLabel();
            this.labelFuncionalidades = new System.Windows.Forms.Label();
            this.labelBienvenida = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // linkBaja
            // 
            this.linkBaja.AutoSize = true;
            this.linkBaja.Location = new System.Drawing.Point(48, 197);
            this.linkBaja.Name = "linkBaja";
            this.linkBaja.Size = new System.Drawing.Size(33, 13);
            this.linkBaja.TabIndex = 19;
            this.linkBaja.TabStop = true;
            this.linkBaja.Text = "BAJA";
            this.linkBaja.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBaja_LinkClicked);
            // 
            // linkModificacion
            // 
            this.linkModificacion.AutoSize = true;
            this.linkModificacion.Location = new System.Drawing.Point(48, 167);
            this.linkModificacion.Name = "linkModificacion";
            this.linkModificacion.Size = new System.Drawing.Size(84, 13);
            this.linkModificacion.TabIndex = 18;
            this.linkModificacion.TabStop = true;
            this.linkModificacion.Text = "MODIFICACION";
            this.linkModificacion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkModificacion_LinkClicked);
            // 
            // linkListado
            // 
            this.linkListado.AutoSize = true;
            this.linkListado.Location = new System.Drawing.Point(48, 135);
            this.linkListado.Name = "linkListado";
            this.linkListado.Size = new System.Drawing.Size(53, 13);
            this.linkListado.TabIndex = 17;
            this.linkListado.TabStop = true;
            this.linkListado.Text = "LISTADO";
            this.linkListado.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListado_LinkClicked);
            // 
            // linkAlta
            // 
            this.linkAlta.AutoSize = true;
            this.linkAlta.Location = new System.Drawing.Point(48, 106);
            this.linkAlta.Name = "linkAlta";
            this.linkAlta.Size = new System.Drawing.Size(34, 13);
            this.linkAlta.TabIndex = 16;
            this.linkAlta.TabStop = true;
            this.linkAlta.Text = "ALTA";
            this.linkAlta.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAlta_LinkClicked);
            // 
            // labelFuncionalidades
            // 
            this.labelFuncionalidades.AutoSize = true;
            this.labelFuncionalidades.Location = new System.Drawing.Point(14, 56);
            this.labelFuncionalidades.Name = "labelFuncionalidades";
            this.labelFuncionalidades.Size = new System.Drawing.Size(84, 13);
            this.labelFuncionalidades.TabIndex = 15;
            this.labelFuncionalidades.Text = "Funcionalidades";
            // 
            // labelBienvenida
            // 
            this.labelBienvenida.AutoSize = true;
            this.labelBienvenida.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBienvenida.Location = new System.Drawing.Point(12, 31);
            this.labelBienvenida.Name = "labelBienvenida";
            this.labelBienvenida.Size = new System.Drawing.Size(114, 25);
            this.labelBienvenida.TabIndex = 14;
            this.labelBienvenida.Text = "ABM Ruta";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(12, 7);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(31, 13);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Atras";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // FormRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 262);
            this.Controls.Add(this.linkBaja);
            this.Controls.Add(this.linkModificacion);
            this.Controls.Add(this.linkListado);
            this.Controls.Add(this.linkAlta);
            this.Controls.Add(this.labelFuncionalidades);
            this.Controls.Add(this.labelBienvenida);
            this.Controls.Add(this.linkLabel2);
            this.Name = "FormRuta";
            this.Text = "Aerolinea FRBA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkBaja;
        private System.Windows.Forms.LinkLabel linkModificacion;
        private System.Windows.Forms.LinkLabel linkListado;
        private System.Windows.Forms.LinkLabel linkAlta;
        private System.Windows.Forms.Label labelFuncionalidades;
        private System.Windows.Forms.Label labelBienvenida;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}