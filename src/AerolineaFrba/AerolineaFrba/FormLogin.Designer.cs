namespace AerolineaFrba
{
    partial class FormLogin
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBienvenida = new System.Windows.Forms.Label();
            this.labelIniciarSesion = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxLoginUser = new System.Windows.Forms.TextBox();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabelNoAdmin = new System.Windows.Forms.LinkLabel();
            this.labelLoginPassword = new System.Windows.Forms.Label();
            this.labelLoginUser = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBienvenida
            // 
            this.labelBienvenida.AutoSize = true;
            this.labelBienvenida.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBienvenida.Location = new System.Drawing.Point(3, 9);
            this.labelBienvenida.Name = "labelBienvenida";
            this.labelBienvenida.Size = new System.Drawing.Size(317, 25);
            this.labelBienvenida.TabIndex = 2;
            this.labelBienvenida.Text = "Bienvenido a Aerolinea FRBA";
            this.labelBienvenida.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelIniciarSesion
            // 
            this.labelIniciarSesion.AutoSize = true;
            this.labelIniciarSesion.Location = new System.Drawing.Point(-3, 0);
            this.labelIniciarSesion.Name = "labelIniciarSesion";
            this.labelIniciarSesion.Size = new System.Drawing.Size(151, 13);
            this.labelIniciarSesion.TabIndex = 3;
            this.labelIniciarSesion.Text = "Iniciar Sesion de Administrador";
            this.labelIniciarSesion.Click += new System.EventHandler(this.labelIniciarSesion_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(150, 102);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxLoginUser
            // 
            this.textBoxLoginUser.Location = new System.Drawing.Point(86, 50);
            this.textBoxLoginUser.Name = "textBoxLoginUser";
            this.textBoxLoginUser.Size = new System.Drawing.Size(200, 20);
            this.textBoxLoginUser.TabIndex = 5;
            // 
            // textBoxLoginPassword
            // 
            this.textBoxLoginPassword.Location = new System.Drawing.Point(86, 76);
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.PasswordChar = '*';
            this.textBoxLoginPassword.Size = new System.Drawing.Size(200, 20);
            this.textBoxLoginPassword.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabelNoAdmin);
            this.panel1.Controls.Add(this.labelLoginPassword);
            this.panel1.Controls.Add(this.labelLoginUser);
            this.panel1.Controls.Add(this.textBoxLoginUser);
            this.panel1.Controls.Add(this.textBoxLoginPassword);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.labelIniciarSesion);
            this.panel1.Location = new System.Drawing.Point(132, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 164);
            this.panel1.TabIndex = 7;
            // 
            // linkLabelNoAdmin
            // 
            this.linkLabelNoAdmin.AutoSize = true;
            this.linkLabelNoAdmin.Location = new System.Drawing.Point(136, 139);
            this.linkLabelNoAdmin.Name = "linkLabelNoAdmin";
            this.linkLabelNoAdmin.Size = new System.Drawing.Size(105, 13);
            this.linkLabelNoAdmin.TabIndex = 9;
            this.linkLabelNoAdmin.TabStop = true;
            this.linkLabelNoAdmin.Text = "No soy administrador";
            this.linkLabelNoAdmin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNoAdmin_LinkClicked);
            // 
            // labelLoginPassword
            // 
            this.labelLoginPassword.AutoSize = true;
            this.labelLoginPassword.Location = new System.Drawing.Point(22, 82);
            this.labelLoginPassword.Name = "labelLoginPassword";
            this.labelLoginPassword.Size = new System.Drawing.Size(53, 13);
            this.labelLoginPassword.TabIndex = 8;
            this.labelLoginPassword.Text = "Password";
            // 
            // labelLoginUser
            // 
            this.labelLoginUser.AutoSize = true;
            this.labelLoginUser.Location = new System.Drawing.Point(24, 57);
            this.labelLoginUser.Name = "labelLoginUser";
            this.labelLoginUser.Size = new System.Drawing.Size(43, 13);
            this.labelLoginUser.TabIndex = 7;
            this.labelLoginUser.Text = "Usuario";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 262);
            this.Controls.Add(this.labelBienvenida);
            this.Controls.Add(this.panel1);
            this.Name = "FormLogin";
            this.Text = "Aerolinea FRBA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBienvenida;
        private System.Windows.Forms.Label labelIniciarSesion;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxLoginUser;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelLoginPassword;
        private System.Windows.Forms.Label labelLoginUser;
        private System.Windows.Forms.LinkLabel linkLabelNoAdmin;
    }
}

