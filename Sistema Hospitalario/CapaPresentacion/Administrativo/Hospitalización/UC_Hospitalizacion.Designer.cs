namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    partial class UC_Hospitalizacion
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblPorcentajeOcupadas = new System.Windows.Forms.Label();
            this.lblOcupadas = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblPacientesInternados = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalHabitaciones = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cboCampo = new System.Windows.Forms.ComboBox();
            this.dgvInternaciones = new System.Windows.Forms.DataGridView();
            this.colHabitacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInternado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPorcentajeDisponibles = new System.Windows.Forms.Label();
            this.lblDisponibles = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternaciones)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1290, 760);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(42, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 680);
            this.panel2.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblPorcentajeOcupadas);
            this.panel6.Controls.Add(this.lblOcupadas);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Location = new System.Drawing.Point(633, 130);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(233, 102);
            this.panel6.TabIndex = 13;
            // 
            // lblPorcentajeOcupadas
            // 
            this.lblPorcentajeOcupadas.AutoSize = true;
            this.lblPorcentajeOcupadas.Location = new System.Drawing.Point(13, 64);
            this.lblPorcentajeOcupadas.Name = "lblPorcentajeOcupadas";
            this.lblPorcentajeOcupadas.Size = new System.Drawing.Size(153, 16);
            this.lblPorcentajeOcupadas.TabIndex = 12;
            this.lblPorcentajeOcupadas.Text = "0% de camas ocupadas";
            // 
            // lblOcupadas
            // 
            this.lblOcupadas.AutoSize = true;
            this.lblOcupadas.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcupadas.Location = new System.Drawing.Point(14, 40);
            this.lblOcupadas.Name = "lblOcupadas";
            this.lblOcupadas.Size = new System.Drawing.Size(19, 18);
            this.lblOcupadas.TabIndex = 11;
            this.lblOcupadas.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 16);
            this.label16.TabIndex = 10;
            this.label16.Text = "Ocupadas";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblPacientesInternados);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Location = new System.Drawing.Point(927, 130);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(233, 102);
            this.panel5.TabIndex = 13;
            // 
            // lblPacientesInternados
            // 
            this.lblPacientesInternados.AutoSize = true;
            this.lblPacientesInternados.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPacientesInternados.Location = new System.Drawing.Point(14, 40);
            this.lblPacientesInternados.Name = "lblPacientesInternados";
            this.lblPacientesInternados.Size = new System.Drawing.Size(19, 18);
            this.lblPacientesInternados.TabIndex = 11;
            this.lblPacientesInternados.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(12, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 16);
            this.label13.TabIndex = 10;
            this.label13.Text = "Total Internados";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblTotalHabitaciones);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(43, 130);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 102);
            this.panel3.TabIndex = 13;
            // 
            // lblTotalHabitaciones
            // 
            this.lblTotalHabitaciones.AutoSize = true;
            this.lblTotalHabitaciones.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHabitaciones.Location = new System.Drawing.Point(14, 40);
            this.lblTotalHabitaciones.Name = "lblTotalHabitaciones";
            this.lblTotalHabitaciones.Size = new System.Drawing.Size(19, 18);
            this.lblTotalHabitaciones.TabIndex = 11;
            this.lblTotalHabitaciones.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Total Habitaciones";
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.btnLimpiar);
            this.panel8.Controls.Add(this.btnBuscar);
            this.panel8.Controls.Add(this.txtBuscar);
            this.panel8.Controls.Add(this.cboCampo);
            this.panel8.Controls.Add(this.dgvInternaciones);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Location = new System.Drawing.Point(42, 267);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1118, 377);
            this.panel8.TabIndex = 6;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(1007, 43);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(83, 36);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(885, 43);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(96, 36);
            this.btnBuscar.TabIndex = 19;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(265, 50);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(210, 24);
            this.txtBuscar.TabIndex = 11;
            // 
            // cboCampo
            // 
            this.cboCampo.FormattingEnabled = true;
            this.cboCampo.Location = new System.Drawing.Point(18, 50);
            this.cboCampo.Name = "cboCampo";
            this.cboCampo.Size = new System.Drawing.Size(216, 24);
            this.cboCampo.TabIndex = 10;
            // 
            // dgvInternaciones
            // 
            this.dgvInternaciones.AllowUserToAddRows = false;
            this.dgvInternaciones.AllowUserToDeleteRows = false;
            this.dgvInternaciones.AllowUserToResizeRows = false;
            this.dgvInternaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInternaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInternaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHabitacion,
            this.colPiso,
            this.colInternado,
            this.colFechaIngreso,
            this.colCama});
            this.dgvInternaciones.EnableHeadersVisualStyles = false;
            this.dgvInternaciones.Location = new System.Drawing.Point(19, 89);
            this.dgvInternaciones.Name = "dgvInternaciones";
            this.dgvInternaciones.ReadOnly = true;
            this.dgvInternaciones.RowHeadersVisible = false;
            this.dgvInternaciones.RowHeadersWidth = 51;
            this.dgvInternaciones.RowTemplate.Height = 24;
            this.dgvInternaciones.Size = new System.Drawing.Size(1071, 266);
            this.dgvInternaciones.TabIndex = 9;
            // 
            // colHabitacion
            // 
            this.colHabitacion.HeaderText = "Habitación";
            this.colHabitacion.MinimumWidth = 6;
            this.colHabitacion.Name = "colHabitacion";
            this.colHabitacion.ReadOnly = true;
            // 
            // colPiso
            // 
            this.colPiso.HeaderText = "Piso";
            this.colPiso.MinimumWidth = 6;
            this.colPiso.Name = "colPiso";
            this.colPiso.ReadOnly = true;
            // 
            // colInternado
            // 
            this.colInternado.HeaderText = "Internado";
            this.colInternado.MinimumWidth = 6;
            this.colInternado.Name = "colInternado";
            this.colInternado.ReadOnly = true;
            // 
            // colFechaIngreso
            // 
            this.colFechaIngreso.HeaderText = "Fecha_Ingreso";
            this.colFechaIngreso.MinimumWidth = 6;
            this.colFechaIngreso.Name = "colFechaIngreso";
            this.colFechaIngreso.ReadOnly = true;
            // 
            // colCama
            // 
            this.colCama.HeaderText = "Cama";
            this.colCama.MinimumWidth = 6;
            this.colCama.Name = "colCama";
            this.colCama.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Listado de Habitaciones";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblPorcentajeDisponibles);
            this.panel4.Controls.Add(this.lblDisponibles);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(339, 130);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 102);
            this.panel4.TabIndex = 5;
            // 
            // lblPorcentajeDisponibles
            // 
            this.lblPorcentajeDisponibles.AutoSize = true;
            this.lblPorcentajeDisponibles.Location = new System.Drawing.Point(13, 64);
            this.lblPorcentajeDisponibles.Name = "lblPorcentajeDisponibles";
            this.lblPorcentajeDisponibles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPorcentajeDisponibles.Size = new System.Drawing.Size(162, 16);
            this.lblPorcentajeDisponibles.TabIndex = 12;
            this.lblPorcentajeDisponibles.Text = "0% de camas disponibles";
            // 
            // lblDisponibles
            // 
            this.lblDisponibles.AutoSize = true;
            this.lblDisponibles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisponibles.Location = new System.Drawing.Point(14, 40);
            this.lblDisponibles.Name = "lblDisponibles";
            this.lblDisponibles.Size = new System.Drawing.Size(19, 18);
            this.lblDisponibles.TabIndex = 11;
            this.lblDisponibles.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Disponibles";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.DodgerBlue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(965, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "Nueva Internación";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.BtnRegistrarInternacion_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Gestión de internaciones y habitaciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hospitalización";
            // 
            // UC_Hospitalizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Hospitalizacion";
            this.Size = new System.Drawing.Size(1290, 760);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternaciones)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataGridView dgvInternaciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblPorcentajeDisponibles;
        private System.Windows.Forms.Label lblDisponibles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblPorcentajeOcupadas;
        private System.Windows.Forms.Label lblOcupadas;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblPacientesInternados;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalHabitaciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cboCampo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHabitacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInternado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaIngreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCama;
    }
}
