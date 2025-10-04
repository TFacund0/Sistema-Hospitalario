using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Turnos : UserControl
    {
        // ============== EVENTOS ==============
        //public event EventHandler<TurnoDTO> VerTurnoSolicitado;
        public event EventHandler RegistrarTurnoSolicitado;

        // ============== CONSTRUCTOR UC TURNOS ==============
        public UC_Turnos()
        {
            InitializeComponent();     
            ConfigurarActividad();    
        }

        // ============== BOTÓN NUEVO TURNO ==============
        private void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            RegistrarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // ============== CONFIGURACION DEL DATAGRIDVIEW ==============
        private void ConfigurarActividad()
        {
            dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTurnos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTurnos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvTurnos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTurnos.ColumnHeadersHeight = 35;
            dgvTurnos.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public class TurnoDTO
        {
            public string Paciente { get; set; }
            public string Medico { get; set; }
            public string Procedimiento { get; set; }
            public string Correo { get; set; }
            public string DNI { get; set; }
            public string Telefono { get; set; }

            public DateTime FechaTurno { get; set; }
            public DateTime FechaRegistro { get; set; }

            public string Observaciones { get; set; }
            public string Estado { get; set; }
            public DateTime Fecha => FechaTurno;
        }
    }
}
