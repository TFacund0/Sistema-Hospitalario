using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Pacientes : UserControl
    {
        private List<PacienteDTO> pacienteDTOs = new List<PacienteDTO>();
        private readonly BindingSource _bs = new BindingSource();

        public UC_Pacientes()
        {
            InitializeComponent();

            ConfigurarActividad();
            CargarOpcionesDeFiltro();
            ConfigurarEnlazadoDeColumnas();
            CargarFilasEjemplo();
        }

        public event EventHandler ExportarDatosSolicitado;

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarDatosSolicitado?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RegistrarPacienteSolicitado;

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            RegistrarPacienteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarActividad()
        {
            dgvPacientes.ReadOnly = true;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.RowHeadersVisible = false;
            dgvPacientes.AllowUserToResizeRows = false;

            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPacientes.ColumnHeadersHeight = 35;
            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public class PacienteDTO
        {
            public DateTime FechaNacimiento { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string direccion { get; set; }
            public string obraSocial { get; set; }
            public int nroAfiliado { get; set; }
            public int dni { get; set; }
            public int habitacion { get; set; }
            public int telefono { get; set; }
            public string observaciones { get; set; }
            public string Estado { get; set; }

            public string Paciente => $"{nombre} {apellido}";
            public int Edad
            {
                get
                {
                    var today = DateTime.Today;
                    var age = today.Year - FechaNacimiento.Year;
                    if (FechaNacimiento.Date > today.AddYears(-age)) age--;
                    return age;
                }
            }
            
            public string DNI => dni.ToString();
        }

        private void ConfigurarEnlazadoDeColumnas()
        {
            dgvPacientes.AutoGenerateColumns = false;

            dgvPacientes.Columns["colPaciente"].DataPropertyName = "Paciente";   // string calculado: nombre + apellido
            dgvPacientes.Columns["colDNI"].DataPropertyName = "DNI";        // string (dni.ToString())
            dgvPacientes.Columns["colEdad"].DataPropertyName = "Edad";       // int calculado por fecha nac.
            dgvPacientes.Columns["colEstado"].DataPropertyName = "Estado";
            dgvPacientes.Columns["colHabitacion"].DataPropertyName = "Habitacion"; // string ("" si 0)
        }

        private void CargarFilasEjemplo()
        {
            pacienteDTOs = new List<PacienteDTO>
            {
                new PacienteDTO()
                {
                    nombre = "Juan",
                    apellido = "Pérez",
                    direccion = "Calle Falsa 123",
                    obraSocial = "OSDE",
                    nroAfiliado = 123456,
                    dni = 12345678,
                    habitacion = 101,
                    telefono = 123456789,
                    observaciones = "Ninguna",
                    Estado = "Internado",
                    FechaNacimiento = new DateTime(1980, 5, 15)
                },
                new PacienteDTO()
                {
                    nombre = "María",
                    apellido = "Gómez",
                    direccion = "Avenida Siempre Viva 742",
                    obraSocial = "Swiss Medical",
                    nroAfiliado = 654321,
                    dni = 87654321,
                    habitacion = 202,
                    telefono = 987654321,
                    observaciones = "Alergia a la penicilina",
                    Estado = "Consulta",
                    FechaNacimiento = new DateTime(1990, 8, 22)
                },
            }
            ;

            _bs.DataSource = pacienteDTOs;
            dgvPacientes.DataSource = _bs;
        }

        private void CargarOpcionesDeFiltro()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "DNI", "Estado" });
            cboCampo.SelectedIndex = 0;
        }

        private void AplicarFiltro(string campo, string texto)
        {
            string q = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<PacienteDTO> query = pacienteDTOs;

            if (!string.IsNullOrEmpty(q))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t => ($"{t.nombre} {t.apellido}".ToLower()).Contains(q));
                        break;
                    case "DNI":
                        query = query.Where(t => t.dni.ToString().Contains(q));
                        break;
                    case "Estado":
                        query = query.Where(t => (t.Estado ?? "").ToLower().Contains(q));
                        break;
                    default:
                        query = query.Where(t =>
                            ($"{t.nombre} {t.apellido}".ToLower()).Contains(q) ||
                            t.dni.ToString().Contains(q) ||
                            (t.Estado ?? "").ToLower().Contains(q) ||
                            (t.direccion ?? "").ToLower().Contains(q));
                        break;
                }
            }

            _bs.DataSource = query.OrderBy(t => t.apellido).ThenBy(t => t.nombre).ToList();
            _bs.ResetBindings(false);     // <<--- refresca el DGV
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            _bs.DataSource = pacienteDTOs.OrderBy(t => t.FechaNacimiento).ToList();
            _bs.ResetBindings(false);     // <<--- refresca
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }
    }
}
