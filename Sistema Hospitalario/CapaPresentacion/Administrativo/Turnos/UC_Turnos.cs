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
    public partial class UC_Turnos : UserControl
    {
        private List<TurnoDTO> _turnosMaster = new List<TurnoDTO>();
        
        private readonly BindingSource _bs = new BindingSource();

        public UC_Turnos()
        {
            InitializeComponent();     
            ConfigurarActividad();     
            CargarFilasEjemplo();      
            ConfigurarEnlazadoDeColumnas(); 
            CargarOpcionesDeFiltro();  
        }

        public event EventHandler RegistrarTurnoSolicitado;

        private void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            RegistrarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarActividad()
        {
            dgvTurnos.AutoGenerateColumns = false; 
            dgvTurnos.ReadOnly = true;             
            dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTurnos.RowHeadersVisible = false;   
            dgvTurnos.AllowUserToResizeRows = false;

            dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTurnos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTurnos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvTurnos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTurnos.ColumnHeadersHeight = 35;
            dgvTurnos.EnableHeadersVisualStyles = false;
            dgvTurnos.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void ConfigurarEnlazadoDeColumnas()
        {
            dgvTurnos.AutoGenerateColumns = false;

            if (dgvTurnos.Columns.Contains("colHora"))
            {
                dgvTurnos.Columns["colHora"].DataPropertyName = "Fecha";
                dgvTurnos.Columns["colHora"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvTurnos.Columns["colHora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvTurnos.Columns.Contains("colPaciente"))
                dgvTurnos.Columns["colPaciente"].DataPropertyName = "Paciente";

            if (dgvTurnos.Columns.Contains("colMedico"))
                dgvTurnos.Columns["colMedico"].DataPropertyName = "Medico";

            if (dgvTurnos.Columns.Contains("colEstado"))
                dgvTurnos.Columns["colEstado"].DataPropertyName = "Estado";
        }

        public class TurnoDTO
        {
            public DateTime Fecha { get; set; }
            public string Paciente { get; set; }
            public string Medico { get; set; }
            public string Estado { get; set; }
        }

        private void CargarFilasEjemplo()
        {   
            _turnosMaster = new List<TurnoDTO>
            {
                new TurnoDTO { Fecha = new DateTime(2024, 06, 15, 10, 00, 0), Paciente = "Juan Pérez",     Medico = "Dr. López",     Estado = "Pendiente" },
                new TurnoDTO { Fecha = new DateTime(2024, 06, 15, 11, 00, 0), Paciente = "Ana Gómez",      Medico = "Dra. Martínez", Estado = "En Curso" },
                new TurnoDTO { Fecha = new DateTime(2024, 06, 14, 09, 30, 0), Paciente = "Luis Fernández", Medico = "Dr. Sánchez",   Estado = "Completado" }
            };

            _bs.DataSource = _turnosMaster.OrderBy(t => t.Fecha).ToList();
            dgvTurnos.DataSource = _bs;
        }

        private void CargarOpcionesDeFiltro()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "Médico", "Estado", "Fecha" });
            cboCampo.SelectedIndex = 0;
        }

        private void AplicarFiltro(string campo, string texto)
        {
            string q = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<TurnoDTO> query = _turnosMaster;

            if (!string.IsNullOrEmpty(q))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t => (t.Paciente ?? "").ToLower().Contains(q)); break;
                    case "Médico":
                        query = query.Where(t => (t.Medico ?? "").ToLower().Contains(q)); break;
                    case "Estado":
                        query = query.Where(t => (t.Estado ?? "").ToLower().Contains(q)); break;
                    case "Fecha":
                        query = query.Where(t =>
                            t.Fecha.ToString("yyyy-MM-dd HH:mm").ToLower().Contains(q) ||
                            t.Fecha.ToString("HH:mm").Contains(q)); break;
                    default: // "Todos"
                        query = query.Where(t =>
                            (t.Paciente ?? "").ToLower().Contains(q) ||
                            (t.Medico ?? "").ToLower().Contains(q) ||
                            (t.Estado ?? "").ToLower().Contains(q) ||
                            t.Fecha.ToString("yyyy-MM-dd HH:mm").ToLower().Contains(q) ||
                            t.Fecha.ToString("HH:mm").Contains(q));
                        break;
                }
            }

            _bs.DataSource = query.OrderBy(t => t.Fecha).ToList();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {            
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null)
            {
                cboCampo.SelectedIndex = 0;
            }

            _bs.DataSource = _turnosMaster.OrderBy(t => t.Fecha).ToList();
        }   
    }
}
