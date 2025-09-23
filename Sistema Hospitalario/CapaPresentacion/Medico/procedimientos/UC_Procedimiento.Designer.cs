namespace Sistema_Hospitalario.CapaPresentacion.Medico.procedimientos
{
    partial class UC_Procedimiento
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel26 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelform = new System.Windows.Forms.Panel();
            this.CBProcedimiento = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.TBTYR = new System.Windows.Forms.TextBox();
            this.TBObservaciones = new System.Windows.Forms.TextBox();
            this.TBAFILIADO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TBDNI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TBTratamiento = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel30.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panelform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(968, 106);
            this.panel3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de procedimiento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(472, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Complete la información para registrar una nueva consulta medica";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 106);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(37, 512);
            this.panel4.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel25);
            this.panel7.Controls.Add(this.panelform);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(37, 106);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(931, 512);
            this.panel7.TabIndex = 9;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // panel25
            // 
            this.panel25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel25.Controls.Add(this.panel30);
            this.panel25.Controls.Add(this.panel29);
            this.panel25.Controls.Add(this.panel28);
            this.panel25.Controls.Add(this.panel26);
            this.panel25.Controls.Add(this.panel23);
            this.panel25.Location = new System.Drawing.Point(572, 414);
            this.panel25.Margin = new System.Windows.Forms.Padding(2);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(316, 45);
            this.panel25.TabIndex = 9;
            // 
            // panel30
            // 
            this.panel30.Controls.Add(this.btnCancelar);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel30.Location = new System.Drawing.Point(226, 0);
            this.panel30.Margin = new System.Windows.Forms.Padding(2);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(90, 45);
            this.panel30.TabIndex = 11;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(3, 6);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 33);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // panel29
            // 
            this.panel29.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel29.Location = new System.Drawing.Point(202, 0);
            this.panel29.Margin = new System.Windows.Forms.Padding(2);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(22, 45);
            this.panel29.TabIndex = 11;
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.btnLimpiar);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel28.Location = new System.Drawing.Point(112, 0);
            this.panel28.Margin = new System.Windows.Forms.Padding(2);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(90, 45);
            this.panel28.TabIndex = 11;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(3, 6);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(82, 33);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // panel26
            // 
            this.panel26.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel26.Location = new System.Drawing.Point(90, 0);
            this.panel26.Margin = new System.Windows.Forms.Padding(2);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(22, 45);
            this.panel26.TabIndex = 10;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.btnGuardar);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Margin = new System.Windows.Forms.Padding(2);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(90, 45);
            this.panel23.TabIndex = 10;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(0, 2);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 40);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // panelform
            // 
            this.panelform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelform.Controls.Add(this.CBProcedimiento);
            this.panelform.Controls.Add(this.dateTimePicker1);
            this.panelform.Controls.Add(this.label8);
            this.panelform.Controls.Add(this.TBTYR);
            this.panelform.Controls.Add(this.TBObservaciones);
            this.panelform.Controls.Add(this.TBAFILIADO);
            this.panelform.Controls.Add(this.label7);
            this.panelform.Controls.Add(this.label6);
            this.panelform.Controls.Add(this.TBDNI);
            this.panelform.Controls.Add(this.label5);
            this.panelform.Controls.Add(this.label3);
            this.panelform.Controls.Add(this.label4);
            this.panelform.Location = new System.Drawing.Point(22, 7);
            this.panelform.Margin = new System.Windows.Forms.Padding(2);
            this.panelform.Name = "panelform";
            this.panelform.Size = new System.Drawing.Size(849, 409);
            this.panelform.TabIndex = 1;
            // 
            // CBProcedimiento
            // 
            this.CBProcedimiento.FormattingEnabled = true;
            this.CBProcedimiento.Location = new System.Drawing.Point(3, 131);
            this.CBProcedimiento.Name = "CBProcedimiento";
            this.CBProcedimiento.Size = new System.Drawing.Size(251, 21);
            this.CBProcedimiento.TabIndex = 16;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(100, 62);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Fecha:";
            // 
            // TBTYR
            // 
            this.TBTYR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBTYR.BackColor = System.Drawing.Color.White;
            this.TBTYR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBTYR.Location = new System.Drawing.Point(0, 330);
            this.TBTYR.Margin = new System.Windows.Forms.Padding(2);
            this.TBTYR.Multiline = true;
            this.TBTYR.Name = "TBTYR";
            this.TBTYR.Size = new System.Drawing.Size(847, 64);
            this.TBTYR.TabIndex = 13;
            // 
            // TBObservaciones
            // 
            this.TBObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TBObservaciones.BackColor = System.Drawing.Color.White;
            this.TBObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBObservaciones.Location = new System.Drawing.Point(0, 207);
            this.TBObservaciones.Margin = new System.Windows.Forms.Padding(2);
            this.TBObservaciones.Multiline = true;
            this.TBObservaciones.Name = "TBObservaciones";
            this.TBObservaciones.Size = new System.Drawing.Size(847, 64);
            this.TBObservaciones.TabIndex = 12;
            this.TBObservaciones.TextChanged += new System.EventHandler(this.TBDX_TextChanged);
            // 
            // TBAFILIADO
            // 
            this.TBAFILIADO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TBAFILIADO.BackColor = System.Drawing.Color.White;
            this.TBAFILIADO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBAFILIADO.Location = new System.Drawing.Point(568, 20);
            this.TBAFILIADO.Margin = new System.Windows.Forms.Padding(2);
            this.TBAFILIADO.Name = "TBAFILIADO";
            this.TBAFILIADO.Size = new System.Drawing.Size(163, 20);
            this.TBAFILIADO.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(431, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "NRO de afiliado:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 188);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Observaciones";
            // 
            // TBDNI
            // 
            this.TBDNI.BackColor = System.Drawing.Color.White;
            this.TBDNI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBDNI.Location = new System.Drawing.Point(137, 20);
            this.TBDNI.Margin = new System.Windows.Forms.Padding(2);
            this.TBDNI.Name = "TBDNI";
            this.TBDNI.Size = new System.Drawing.Size(163, 20);
            this.TBDNI.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 311);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "tratamiento y recomendaciones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-3, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "DNI del paciente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-3, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tipo de procedimiento realizado";
            // 
            // TBTratamiento
            // 
            this.TBTratamiento.BackColor = System.Drawing.Color.LightGray;
            this.TBTratamiento.Dock = System.Windows.Forms.DockStyle.Right;
            this.TBTratamiento.Location = new System.Drawing.Point(933, 106);
            this.TBTratamiento.Margin = new System.Windows.Forms.Padding(2);
            this.TBTratamiento.Name = "TBTratamiento";
            this.TBTratamiento.Size = new System.Drawing.Size(35, 512);
            this.TBTratamiento.TabIndex = 10;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(37, 577);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(896, 41);
            this.panel6.TabIndex = 11;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // UC_Procedimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.TBTratamiento);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "UC_Procedimiento";
            this.Size = new System.Drawing.Size(968, 618);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel30.ResumeLayout(false);
            this.panel28.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panelform.ResumeLayout(false);
            this.panelform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panelform;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TBTYR;
        private System.Windows.Forms.TextBox TBObservaciones;
        private System.Windows.Forms.TextBox TBAFILIADO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TBDNI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel TBTratamiento;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox CBProcedimiento;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
