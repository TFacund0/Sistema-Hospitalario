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
    public partial class UC_Hospitalizacion : UserControl
    {
        // Lista master + BindingSource para el grid de habitaciones
        private List<HabitacionDTO> _habitaciones = new List<HabitacionDTO>();
        private readonly BindingSource _bsHab = new BindingSource();

        public UC_Hospitalizacion()
        {
            InitializeComponent();

            ConfigurarTablaHabitaciones();
            ConfigurarEnlazadoColumnasHabitacion();
            CargarHabitacionesEjemplo();
            CargarOpcionesFiltroHospitalizacion();
        }

        public event EventHandler RegistrarInternacionSolicitada;

        private void btnRegistrarInternacion_Click(object sender, EventArgs e)
        {
            RegistrarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarTablaHabitaciones()
        {
            dgvHabitaciones.AutoGenerateColumns = false;
            dgvHabitaciones.ReadOnly = true;
            dgvHabitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHabitaciones.RowHeadersVisible = false;
            dgvHabitaciones.AllowUserToResizeRows = false;

            dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHabitaciones.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvHabitaciones.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            dgvHabitaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvHabitaciones.ColumnHeadersHeight = 35;
            dgvHabitaciones.EnableHeadersVisualStyles = false;
            dgvHabitaciones.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public class HabitacionDTO
        {
            public string Piso { get; set; }          // "1", "2", ...
            public string Habitacion { get; set; }    // "101"
            public string Internado { get; set; }     // "" si disponible
            public DateTime? FechaIngreso { get; set; }
            public string Tipo { get; set; }          // "Clínica", "UCI", "Privada", ...
            public string Estado { get; set; }        // "Disponible", "Ocupada", "Reservada", "Limpieza", "Mantenimiento"

            // Helpers para el grid
            public string Fecha_Ingreso => FechaIngreso?.ToString("yyyy-MM-dd HH:mm") ?? "";
        }


        private void ConfigurarEnlazadoColumnasHabitacion()
        {
            dgvHabitaciones.AutoGenerateColumns = false;

            if (dgvHabitaciones.Columns.Contains("colPiso"))
                dgvHabitaciones.Columns["colPiso"].DataPropertyName = "Piso";

            if (dgvHabitaciones.Columns.Contains("colHabitacion"))
                dgvHabitaciones.Columns["colHabitacion"].DataPropertyName = "Habitacion";

            if (dgvHabitaciones.Columns.Contains("colInternado"))
                dgvHabitaciones.Columns["colInternado"].DataPropertyName = "Internado";

            if (dgvHabitaciones.Columns.Contains("colFechaIngreso"))
                dgvHabitaciones.Columns["colFechaIngreso"].DataPropertyName = "Fecha_Ingreso";

            if (dgvHabitaciones.Columns.Contains("colTipo"))
                dgvHabitaciones.Columns["colTipo"].DataPropertyName = "Tipo";

            if (dgvHabitaciones.Columns.Contains("colEstado"))
                dgvHabitaciones.Columns["colEstado"].DataPropertyName = "Estado";
        }

        private void CargarHabitacionesEjemplo()
        {
            _habitaciones = new List<HabitacionDTO>
            {
                new HabitacionDTO { Piso="1", Habitacion="101", Internado="María González", FechaIngreso=new DateTime(2025,9,8, 10,30,0), Tipo="Clínica", Estado="Ocupada" },
                new HabitacionDTO { Piso="1", Habitacion="102", Internado="",                 FechaIngreso=null,                             Tipo="Clínica", Estado="Disponible" },
                new HabitacionDTO { Piso="2", Habitacion="201", Internado="Carlos Rodríguez", FechaIngreso=new DateTime(2025,9,7, 18,15,0), Tipo="UCI",     Estado="Ocupada" },
                new HabitacionDTO { Piso="2", Habitacion="202", Internado="",                 FechaIngreso=null,                             Tipo="Privada", Estado="Reservada" },
                new HabitacionDTO { Piso="3", Habitacion="301", Internado="",                 FechaIngreso=null,                             Tipo="Clínica", Estado="Limpieza" },
            };

            _bsHab.DataSource = _habitaciones
                .OrderBy(h => h.Piso).ThenBy(h => h.Habitacion)
                .ToList();

            dgvHabitaciones.DataSource = _bsHab;
        }

        private void CargarOpcionesFiltroHospitalizacion()
        {
            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Piso", "Habitación", "Internado", "Tipo", "Estado", "Fecha" });
            cboCampo.SelectedIndex = 0;
        }

        private void AplicarFiltroHospitalizacion(string campo, string texto)
        {
            string q = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<HabitacionDTO> query = _habitaciones;

            if (!string.IsNullOrEmpty(q))
            {
                switch (campo ?? "Todos")
                {
                    case "Piso":
                        query = query.Where(h => (h.Piso ?? "").ToLower().Contains(q)); break;

                    case "Habitación":
                        query = query.Where(h => (h.Habitacion ?? "").ToLower().Contains(q)); break;

                    case "Internado":
                        query = query.Where(h => (h.Internado ?? "").ToLower().Contains(q)); break;

                    case "Tipo":
                        query = query.Where(h => (h.Tipo ?? "").ToLower().Contains(q)); break;

                    case "Estado":
                        query = query.Where(h => (h.Estado ?? "").ToLower().Contains(q)); break;

                    case "Fecha": // busca por “yyyy-MM-dd” u “HH:mm”
                        query = query.Where(h =>
                            (h.FechaIngreso?.ToString("yyyy-MM-dd HH:mm") ?? "").ToLower().Contains(q) ||
                            (h.FechaIngreso?.ToString("yyyy-MM-dd") ?? "").ToLower().Contains(q) ||
                            (h.FechaIngreso?.ToString("HH:mm") ?? "").Contains(q));
                        break;

                    default: // "Todos"
                        query = query.Where(h =>
                            (h.Piso ?? "").ToLower().Contains(q) ||
                            (h.Habitacion ?? "").ToLower().Contains(q) ||
                            (h.Internado ?? "").ToLower().Contains(q) ||
                            (h.Tipo ?? "").ToLower().Contains(q) ||
                            (h.Estado ?? "").ToLower().Contains(q) ||
                            (h.FechaIngreso?.ToString("yyyy-MM-dd HH:mm") ?? "").ToLower().Contains(q));
                        break;
                }
            }

            _bsHab.DataSource = query
                .OrderBy(h => h.Piso).ThenBy(h => h.Habitacion)
                .ToList();

            _bsHab.ResetBindings(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltroHospitalizacion(campo, texto);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cboCampo.SelectedIndex = 0;

            _bsHab.DataSource = _habitaciones
                .OrderBy(h => h.Piso).ThenBy(h => h.Habitacion)
                .ToList();

            _bsHab.ResetBindings(false);
        }

    }
}
