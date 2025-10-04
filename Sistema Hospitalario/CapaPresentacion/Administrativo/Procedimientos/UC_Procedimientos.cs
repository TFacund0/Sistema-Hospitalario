using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Procedimientos : UserControl
    {
        // ======================== CONSTRUCTOR UC PROCEDIMIENTOS ========================
        public UC_Procedimientos()
        {
            InitializeComponent();
            ConfigurarActividad();
        }

        // ======================== CONFIGURAR ACTIVIDAD ========================
        private void ConfigurarActividad()
        {
            dgvProcedimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvProcedimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProcedimientos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProcedimientos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvProcedimientos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProcedimientos.ColumnHeadersHeight = 35;
            dgvProcedimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }
    }
}
