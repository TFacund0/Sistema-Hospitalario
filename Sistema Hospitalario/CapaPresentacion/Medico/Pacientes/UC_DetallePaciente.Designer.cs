namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    partial class UC_DetallePaciente
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
            this.TBContacto = new System.Windows.Forms.Panel();
            this.paneltxtHistorialDetalle = new System.Windows.Forms.Panel();
            this.webBrowserHistorial = new System.Windows.Forms.WebBrowser();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.TBContacto.SuspendLayout();
            this.paneltxtHistorialDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // TBContacto
            // 
            this.TBContacto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBContacto.BackColor = System.Drawing.Color.LightGray;
            this.TBContacto.Controls.Add(this.btnImprimir);
            this.TBContacto.Controls.Add(this.paneltxtHistorialDetalle);
            this.TBContacto.Location = new System.Drawing.Point(32, 31);
            this.TBContacto.Name = "TBContacto";
            this.TBContacto.Size = new System.Drawing.Size(900, 552);
            this.TBContacto.TabIndex = 1;
            // 
            // paneltxtHistorialDetalle
            // 
            this.paneltxtHistorialDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneltxtHistorialDetalle.BackColor = System.Drawing.Color.White;
            this.paneltxtHistorialDetalle.Controls.Add(this.webBrowserHistorial);
            this.paneltxtHistorialDetalle.Location = new System.Drawing.Point(33, 18);
            this.paneltxtHistorialDetalle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.paneltxtHistorialDetalle.Name = "paneltxtHistorialDetalle";
            this.paneltxtHistorialDetalle.Size = new System.Drawing.Size(838, 468);
            this.paneltxtHistorialDetalle.TabIndex = 16;
            // 
            // webBrowserHistorial
            // 
            this.webBrowserHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHistorial.Location = new System.Drawing.Point(0, 0);
            this.webBrowserHistorial.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHistorial.Name = "webBrowserHistorial";
            this.webBrowserHistorial.Size = new System.Drawing.Size(838, 468);
            this.webBrowserHistorial.TabIndex = 0;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(652, 504);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(170, 33);
            this.btnImprimir.TabIndex = 24;
            this.btnImprimir.Text = "Imprimir historia Clinica";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // UC_DetallePaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TBContacto);
            this.Name = "UC_DetallePaciente";
            this.Size = new System.Drawing.Size(968, 618);
            this.TBContacto.ResumeLayout(false);
            this.paneltxtHistorialDetalle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TBContacto;
        private System.Windows.Forms.Panel paneltxtHistorialDetalle;
        private System.Windows.Forms.WebBrowser webBrowserHistorial;
        private System.Windows.Forms.Button btnImprimir;
    }
}
