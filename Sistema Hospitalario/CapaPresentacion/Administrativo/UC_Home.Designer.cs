namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    partial class UC_Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvActividad = new System.Windows.Forms.DataGridView();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Horario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCantidadConsultas = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPorcentajeCamas = new System.Windows.Forms.Label();
            this.lblCamasOcupadas = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPacientesActivos = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividad)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1290, 760);
            this.panel1.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(45, 720);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1200, 40);
            this.panel11.TabIndex = 13;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(45, 40);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1200, 720);
            this.panel8.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 680);
            this.panel2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.dgvActividad);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Location = new System.Drawing.Point(41, 263);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1121, 395);
            this.panel7.TabIndex = 3;
            // 
            // dgvActividad
            // 
            this.dgvActividad.AllowUserToAddRows = false;
            this.dgvActividad.AllowUserToDeleteRows = false;
            this.dgvActividad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActividad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNombre,
            this.Apellido,
            this.colAccion,
            this.Horario,
            this.colTipo});
            this.dgvActividad.Location = new System.Drawing.Point(21, 75);
            this.dgvActividad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvActividad.Name = "dgvActividad";
            this.dgvActividad.ReadOnly = true;
            this.dgvActividad.RowHeadersWidth = 51;
            this.dgvActividad.RowTemplate.Height = 24;
            this.dgvActividad.Size = new System.Drawing.Size(1080, 302);
            this.dgvActividad.TabIndex = 6;
            // 
            // colNombre
            // 
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 6;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.Width = 125;
            // 
            // Apellido
            // 
            this.Apellido.HeaderText = "Apellido";
            this.Apellido.MinimumWidth = 6;
            this.Apellido.Name = "Apellido";
            this.Apellido.ReadOnly = true;
            this.Apellido.Width = 125;
            // 
            // colAccion
            // 
            this.colAccion.HeaderText = "Accion";
            this.colAccion.MinimumWidth = 6;
            this.colAccion.Name = "colAccion";
            this.colAccion.ReadOnly = true;
            this.colAccion.Width = 125;
            // 
            // Horario
            // 
            this.Horario.HeaderText = "Horario";
            this.Horario.MinimumWidth = 6;
            this.Horario.Name = "Horario";
            this.Horario.ReadOnly = true;
            this.Horario.Width = 125;
            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.MinimumWidth = 6;
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            this.colTipo.Width = 125;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ultimas acciones en el sistema";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Actividad Reciente";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.lblCantidadConsultas);
            this.panel5.Controls.Add(this.label);
            this.panel5.Location = new System.Drawing.Point(919, 112);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(243, 114);
            this.panel5.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(67, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 16);
            this.label13.TabIndex = 7;
            this.label13.Text = "Programadas";
            // 
            // lblCantidadConsultas
            // 
            this.lblCantidadConsultas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCantidadConsultas.AutoSize = true;
            this.lblCantidadConsultas.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadConsultas.Location = new System.Drawing.Point(89, 46);
            this.lblCantidadConsultas.Name = "lblCantidadConsultas";
            this.lblCantidadConsultas.Size = new System.Drawing.Size(54, 25);
            this.lblCantidadConsultas.TabIndex = 5;
            this.lblCantidadConsultas.Text = "120";
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(21, 23);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(190, 18);
            this.label.TabIndex = 6;
            this.label.Text = "Cantidad de Consultas";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblPorcentajeCamas);
            this.panel4.Controls.Add(this.lblCamasOcupadas);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Location = new System.Drawing.Point(502, 112);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(242, 114);
            this.panel4.TabIndex = 3;
            // 
            // lblPorcentajeCamas
            // 
            this.lblPorcentajeCamas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPorcentajeCamas.AutoSize = true;
            this.lblPorcentajeCamas.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentajeCamas.Location = new System.Drawing.Point(53, 78);
            this.lblPorcentajeCamas.Name = "lblPorcentajeCamas";
            this.lblPorcentajeCamas.Size = new System.Drawing.Size(129, 16);
            this.lblPorcentajeCamas.TabIndex = 4;
            this.lblPorcentajeCamas.Text = "70% de ocupación";
            // 
            // lblCamasOcupadas
            // 
            this.lblCamasOcupadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCamasOcupadas.AutoSize = true;
            this.lblCamasOcupadas.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamasOcupadas.Location = new System.Drawing.Point(69, 46);
            this.lblCamasOcupadas.Name = "lblCamasOcupadas";
            this.lblCamasOcupadas.Size = new System.Drawing.Size(96, 25);
            this.lblCamasOcupadas.TabIndex = 2;
            this.lblCamasOcupadas.Text = "70/100";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(45, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 18);
            this.label12.TabIndex = 3;
            this.label12.Text = "Camas Ocupadas";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.lblPacientesActivos);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(41, 112);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 114);
            this.panel3.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(77, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "En el hospital";
            // 
            // lblPacientesActivos
            // 
            this.lblPacientesActivos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPacientesActivos.AutoSize = true;
            this.lblPacientesActivos.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPacientesActivos.Location = new System.Drawing.Point(95, 46);
            this.lblPacientesActivos.Name = "lblPacientesActivos";
            this.lblPacientesActivos.Size = new System.Drawing.Size(54, 25);
            this.lblPacientesActivos.TabIndex = 0;
            this.lblPacientesActivos.Text = "247";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Pacientes Activos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Resumen general del sistema hospitalario";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1245, 40);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(45, 720);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 40);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(45, 720);
            this.panel10.TabIndex = 10;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1290, 40);
            this.panel9.TabIndex = 2;
            // 
            // UC_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UC_Home";
            this.Size = new System.Drawing.Size(1291, 761);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividad)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvActividad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPacientesActivos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblCantidadConsultas;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lblPorcentajeCamas;
        private System.Windows.Forms.Label lblCamasOcupadas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Horario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel11;
    }
}
