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
        // Lista con TODOS los turnos cargados en memoria (la “fuente maestra”)
        private List<TurnoDTO> _turnosMaster = new List<TurnoDTO>();

        // Componente que actúa como puente entre la lista y el DataGridView.
        // Permite re-binder fácil cuando filtrás, ordenás, etc.
        private readonly BindingSource _bs = new BindingSource();

        public UC_Turnos()
        {
            InitializeComponent();     // crea y posiciona los controles del diseñador
            InformacionPaneles();      // carga números de los paneles KPI (arriba)
            ConfigurarActividad();     // deja el DataGridView con estilo/propiedades
            CargarFilasEjemplo();      // arma lista de turnos de ejemplo y la bindea
            ConfigurarEnlazadoDeColumnas(); // mapea columnas del grid a propiedades del objeto
            CargarOpcionesDeFiltro();  // llena el combo de “campo” (Todos, Paciente, etc.)
        }

        private void InformacionPaneles()
        {
            // Carga números de tarjetas “Pendientes / En curso / Completados / Cancelados”.
            // Valida que los string sean enteros válidos antes de mostrarlos.
            // Si algo es inválido, muestra un MessageBox.
            
            string cantPendientes = "10";
            string cantEnCurso = "5";
            string cantCompletados = "15";
            string cantCancelados = "3";

            if (int.TryParse(cantPendientes, out int valorPendiente) && valorPendiente >= 0 && valorPendiente < 1000 &&
                int.TryParse(cantEnCurso, out int valorEnCurso) && valorEnCurso >= 0 && valorEnCurso < 1000 &&
                int.TryParse(cantCompletados, out int valorCompletado) && valorCompletado >= 0 && valorCompletado < 1000 &&
                int.TryParse(cantCancelados, out int valorCancel) && valorCancel >= 0 && valorCancel < 1000)
            {
                lblTurnosPendientes.Text = cantPendientes;
                lblTurnosCurso.Text = cantEnCurso;
                lblTurnosCompletados.Text = cantCompletados;
                lblTurnosCancelados.Text = cantCancelados;
            }
            else
            {
                MessageBox.Show("Error: Los valores de los paneles deben ser números enteros no negativos y dentro del rango permitido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarActividad()
        {
            dgvTurnos.AutoGenerateColumns = false; // << Usar solo tus columnas del diseñador
            dgvTurnos.ReadOnly = true;             // no editable
            dgvTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTurnos.RowHeadersVisible = false;   // oculta el encabezado de filas
            dgvTurnos.AllowUserToResizeRows = false;

            // Autoajuste y estilos
            dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTurnos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTurnos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvTurnos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTurnos.ColumnHeadersHeight = 35;
            dgvTurnos.EnableHeadersVisualStyles = false;
            dgvTurnos.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Si tuvieras una columna llamada “Fecha” (por Name), aplica formato de hora/fecha.
            if (dgvTurnos.Columns.Contains("Fecha"))
            {
                dgvTurnos.Columns["Fecha"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvTurnos.Columns["Fecha"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ConfigurarEnlazadoDeColumnas()
        {
            // Mapea cada COLUMNA del DataGridView (por su Name en el diseñador)
            // a la PROPIEDAD del objeto TurnoDTO (DataPropertyName).
            // Así el grid sabe qué mostrar en cada columna.

            // colHora  -> TurnoDTO.Fecha
            // colPaciente -> TurnoDTO.Paciente
            // colMedico   -> TurnoDTO.Medico
            // colEstado   -> TurnoDTO.Estado

            // También se da formato a la hora en colHora (“yyyy-MM-dd HH:mm”).
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


        // llena el combo de campo (si tenés uno llamado cboCampo)
        private void CargarOpcionesDeFiltro()
        {
            // Llena el ComboBox de “campo de búsqueda”
            // Opciones: Todos, Paciente, Médico, Estado, Fecha
            // Deja el combo en modo DropDownList (no editable)
            if (cboCampo == null) return; // por si todavía no lo agregaste

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "Médico", "Estado", "Fecha" });
            cboCampo.SelectedIndex = 0;
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
            // Llena _turnosMaster con datos de demo
            // (hoy reemplaza a la consulta a BD)
            _turnosMaster = new List<TurnoDTO>
            {
                new TurnoDTO { Fecha = new DateTime(2024, 06, 15, 10, 00, 0), Paciente = "Juan Pérez",     Medico = "Dr. López",     Estado = "Pendiente" },
                new TurnoDTO { Fecha = new DateTime(2024, 06, 15, 11, 00, 0), Paciente = "Ana Gómez",      Medico = "Dra. Martínez", Estado = "En Curso" },
                new TurnoDTO { Fecha = new DateTime(2024, 06, 14, 09, 30, 0), Paciente = "Luis Fernández", Medico = "Dr. Sánchez",   Estado = "Completado" }
            };

            // Bindea la lista ORDENADA al BindingSource,
            // y el BindingSource al DataGridView.
            _bs.DataSource = _turnosMaster.OrderBy(t => t.Fecha).ToList();
            dgvTurnos.DataSource = _bs;
        }

        // ---- (Opcional) Filtrado por campo + texto, por si querés usarlo con btnBuscar ----
        private void AplicarFiltro(string campo, string texto)
        {
            // 1) Toma el texto a buscar y lo normaliza (lower)
            // 2) Empieza desde la lista MAESTRA (_turnosMaster)
            // 3) Si hay texto, filtra según el “campo”:
            //      - Paciente:   busca en TurnoDTO.Paciente
            //      - Médico:     TurnoDTO.Medico
            //      - Estado:     TurnoDTO.Estado
            //      - Fecha:      compara strings de fecha/hora ("yyyy-MM-dd HH:mm" o "HH:mm")
            //      - Todos:      busca en todos los campos anteriores
            // 4) Ordena por fecha y rebindea el BindingSource.
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
            // Lee el “campo” elegido en el combo y el texto del textbox,
            // y aplica el filtro sobre el grid.
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Vacía el TextBox, resetea el ComboBox a “Todos”
            // y vuelve a mostrar la lista completa ORDENDADA por fecha.
            txtBuscar.Clear();
            if (cboCampo != null)
            {
                cboCampo.SelectedIndex = 0;
            }

            _bs.DataSource = _turnosMaster.OrderBy(t => t.Fecha).ToList();
        }

        // Evento para manejar el clic en el botón "Registrar Paciente"
        public event EventHandler RegistrarTurnoSolicitado;

        private void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            // Dispara un evento que tiene que manejar el formulario del Menu Administrativo
            RegistrarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }       
    }
}
