namespace WindowsFormsInicio_de_sesion
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.LabelUsuario = new System.Windows.Forms.Label();
            this.BotonIngresar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelFormLogin = new System.Windows.Forms.Panel();
            this.panelLogin.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFormLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(175, 170);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(199, 22);
            this.txtUsuario.TabIndex = 0;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(175, 249);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(199, 22);
            this.txtContraseña.TabIndex = 1;
            this.txtContraseña.UseSystemPasswordChar = true;
            // 
            // LabelUsuario
            // 
            this.LabelUsuario.AutoSize = true;
            this.LabelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUsuario.Location = new System.Drawing.Point(225, 144);
            this.LabelUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUsuario.Name = "LabelUsuario";
            this.LabelUsuario.Size = new System.Drawing.Size(79, 25);
            this.LabelUsuario.TabIndex = 2;
            this.LabelUsuario.Text = "Usuario";
            // 
            // BotonIngresar
            // 
            this.BotonIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(69)))), ((int)(((byte)(122)))));
            this.BotonIngresar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BotonIngresar.FlatAppearance.BorderSize = 0;
            this.BotonIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BotonIngresar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BotonIngresar.Location = new System.Drawing.Point(192, 320);
            this.BotonIngresar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BotonIngresar.Name = "BotonIngresar";
            this.BotonIngresar.Size = new System.Drawing.Size(163, 28);
            this.BotonIngresar.TabIndex = 3;
            this.BotonIngresar.Text = "Ingresar";
            this.BotonIngresar.UseVisualStyleBackColor = false;
            this.BotonIngresar.Click += new System.EventHandler(this.BotonIngresar_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(153, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "Iniciar Sesion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(201, 223);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Contraseña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.GhostWhite;
            this.label2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.label2.Location = new System.Drawing.Point(169, 352);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 41);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clinicks";
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Linen;
            this.panelLogin.Controls.Add(this.panel2);
            this.panelLogin.Controls.Add(this.panelFormLogin);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogin.Location = new System.Drawing.Point(0, 0);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(1067, 469);
            this.panelLogin.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(537, 469);
            this.panel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Sistema_Hospitalario.Properties.Resources.ChatGPT_Image_1_sept_2025__03_00_36_p_m_;
            this.pictureBox1.Location = new System.Drawing.Point(65, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(376, 325);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelFormLogin
            // 
            this.panelFormLogin.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelFormLogin.Controls.Add(this.txtContraseña);
            this.panelFormLogin.Controls.Add(this.LabelUsuario);
            this.panelFormLogin.Controls.Add(this.label3);
            this.panelFormLogin.Controls.Add(this.txtUsuario);
            this.panelFormLogin.Controls.Add(this.BotonIngresar);
            this.panelFormLogin.Controls.Add(this.label4);
            this.panelFormLogin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelFormLogin.Location = new System.Drawing.Point(533, 0);
            this.panelFormLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFormLogin.Name = "panelFormLogin";
            this.panelFormLogin.Size = new System.Drawing.Size(533, 469);
            this.panelFormLogin.TabIndex = 9;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1067, 469);
            this.Controls.Add(this.panelLogin);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelLogin.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelFormLogin.ResumeLayout(false);
            this.panelFormLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label LabelUsuario;
        private System.Windows.Forms.Button BotonIngresar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Panel panelFormLogin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

