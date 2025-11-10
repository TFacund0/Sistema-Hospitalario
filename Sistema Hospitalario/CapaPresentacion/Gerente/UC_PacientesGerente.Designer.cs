namespace Sistema_Hospitalario.CapaPresentacion.Gerente
{
    partial class UC_PacientesGerente
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chartPacientesPorDia = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPacientesPorEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cboCampo = new System.Windows.Forms.ComboBox();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.colPaciente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPacientesPorDia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPacientesPorEstado)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.687962F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.65166F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.660377F));
            this.tableLayoutPanel1.Controls.Add(this.panel7, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.24144F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.21651F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.542056F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1321, 770);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.LightGray;
            this.panel7.Controls.Add(this.tableLayoutPanel2);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.label20);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Location = new System.Drawing.Point(78, 65);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1165, 652);
            this.panel7.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.25926F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8333333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.18518F));
            this.tableLayoutPanel2.Controls.Add(this.chartPacientesPorDia, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chartPacientesPorEstado, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(44, 105);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1080, 249);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // chartPacientesPorDia
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPacientesPorDia.ChartAreas.Add(chartArea1);
            this.chartPacientesPorDia.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartPacientesPorDia.Legends.Add(legend1);
            this.chartPacientesPorDia.Location = new System.Drawing.Point(3, 3);
            this.chartPacientesPorDia.Name = "chartPacientesPorDia";
            this.chartPacientesPorDia.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPacientesPorDia.Series.Add(series1);
            this.chartPacientesPorDia.Size = new System.Drawing.Size(560, 243);
            this.chartPacientesPorDia.TabIndex = 6;
            this.chartPacientesPorDia.Text = "chart1";
            // 
            // chartPacientesPorEstado
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPacientesPorEstado.ChartAreas.Add(chartArea2);
            this.chartPacientesPorEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartPacientesPorEstado.Legends.Add(legend2);
            this.chartPacientesPorEstado.Location = new System.Drawing.Point(582, 3);
            this.chartPacientesPorEstado.Name = "chartPacientesPorEstado";
            this.chartPacientesPorEstado.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartPacientesPorEstado.Series.Add(series2);
            this.chartPacientesPorEstado.Size = new System.Drawing.Size(495, 243);
            this.chartPacientesPorEstado.TabIndex = 7;
            this.chartPacientesPorEstado.Text = "chart2";
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
            this.panel8.Controls.Add(this.dgvPacientes);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Location = new System.Drawing.Point(41, 379);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1083, 246);
            this.panel8.TabIndex = 9;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(939, 81);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(124, 28);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(797, 81);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(124, 28);
            this.btnBuscar.TabIndex = 19;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(170, 85);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(154, 24);
            this.txtBuscar.TabIndex = 11;
            // 
            // cboCampo
            // 
            this.cboCampo.FormattingEnabled = true;
            this.cboCampo.Location = new System.Drawing.Point(21, 85);
            this.cboCampo.Name = "cboCampo";
            this.cboCampo.Size = new System.Drawing.Size(121, 24);
            this.cboCampo.TabIndex = 10;
            // 
            // dgvPacientes
            // 
            this.dgvPacientes.AllowUserToAddRows = false;
            this.dgvPacientes.AllowUserToDeleteRows = false;
            this.dgvPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPaciente,
            this.colDni,
            this.colEdad,
            this.colEstado});
            this.dgvPacientes.Location = new System.Drawing.Point(21, 116);
            this.dgvPacientes.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.ReadOnly = true;
            this.dgvPacientes.RowHeadersWidth = 51;
            this.dgvPacientes.RowTemplate.Height = 24;
            this.dgvPacientes.Size = new System.Drawing.Size(1042, 111);
            this.dgvPacientes.TabIndex = 9;
            // 
            // colPaciente
            // 
            this.colPaciente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPaciente.HeaderText = "Paciente";
            this.colPaciente.MinimumWidth = 6;
            this.colPaciente.Name = "colPaciente";
            this.colPaciente.ReadOnly = true;
            // 
            // colDni
            // 
            this.colDni.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 6;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            // 
            // colEdad
            // 
            this.colEdad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEdad.HeaderText = "Edad";
            this.colEdad.MinimumWidth = 6;
            this.colEdad.Name = "colEdad";
            this.colEdad.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 6;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colEstado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(264, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gestión completa de pacientes registrados";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Listado de Pacientes";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(37, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(189, 20);
            this.label20.TabIndex = 1;
            this.label20.Text = "Gestión de pacientes";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(36, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(136, 28);
            this.label21.TabIndex = 0;
            this.label21.Text = "Pacientes";
            // 
            // UC_PacientesGerente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_PacientesGerente";
            this.Size = new System.Drawing.Size(1321, 770);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPacientesPorDia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPacientesPorEstado)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPacientesPorEstado;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPacientesPorDia;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cboCampo;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
    }
}
