using Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos;
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

        public event EventHandler<TurnoDTO> VerTurnoSolicitado;
        public event EventHandler RegistrarTurnoSolicitado;

        public UC_Turnos()
        {
            InitializeComponent();     
            ConfigurarActividad();     
            CargarFilasEjemplo();      
            ConfigurarEnlazadoDeColumnas(); 
            CargarOpcionesDeFiltro();

            dgvTurnos.CellContentClick += dgvTurnos_CellContentClick;
        }

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


        private void dgvTurnos_CellContentClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvTurnos.Columns[e.ColumnIndex].Name == "colAccion")
            {
                var turno = _bs[e.RowIndex] as TurnoDTO;
                if (turno != null) VerTurnoSolicitado?.Invoke(this, turno);
            }
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


        private void CargarFilasEjemplo()
        {
            _turnosMaster = new List<TurnoDTO>
    {
        new TurnoDTO
        {
            Paciente = "Juan Pérez",
            Medico = "Dr. López",
            Procedimiento = "Consulta general",
            Correo = "juan.perez@mail.com",
            DNI = "30123456",
            Telefono = "3794123456",
            FechaTurno = new DateTime(2024, 06, 15, 10, 00, 0),
            FechaRegistro = new DateTime(2024, 06, 10, 14, 30, 0),
            Observaciones = "Primera consulta",
            Estado = "Pendiente"
        },
        new TurnoDTO
        {
            Paciente = "Ana Gómez",
            Medico = "Dra. Martínez",
            Procedimiento = "Control post-operatorio",
            Correo = "ana.gomez@mail.com",
            DNI = "28999888",
            Telefono = "3794654321",
            FechaTurno = new DateTime(2024, 06, 15, 11, 00, 0),
            FechaRegistro = new DateTime(2024, 06, 12, 09, 15, 0),
            Observaciones = "Traer estudios previos",
            Estado = "En Curso"
        },
        new TurnoDTO
        {
            Paciente = "Luis Fernández",
            Medico = "Dr. Sánchez",
            Procedimiento = "Ecografía abdominal",
            Correo = "luis.fernandez@mail.com",
            DNI = "27111222",
            Telefono = "3794987654",
            FechaTurno = new DateTime(2024, 06, 14, 09, 30, 0),
            FechaRegistro = new DateTime(2024, 06, 08, 16, 45, 0),
            Observaciones = "Ayuno 8 horas",
            Estado = "Completado"
        }
    };

            _bs.DataSource = _turnosMaster.OrderBy(t => t.FechaTurno).ToList();
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
                            t.FechaTurno.ToString("yyyy-MM-dd HH:mm").ToLower().Contains(q) ||
                            t.FechaTurno.ToString("HH:mm").Contains(q));
                        break;

                    default:
                        query = query.Where(t =>
                            (t.Paciente ?? "").ToLower().Contains(q) ||
                            (t.Medico ?? "").ToLower().Contains(q) ||
                            (t.Estado ?? "").ToLower().Contains(q) ||
                            t.Fecha.ToString("yyyy-MM-dd HH:mm").ToLower().Contains(q) ||
                            t.Fecha.ToString("HH:mm").Contains(q));
                        break;
                }
            }

            _bs.DataSource = query.OrderBy(t => t.FechaTurno).ToList();
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
