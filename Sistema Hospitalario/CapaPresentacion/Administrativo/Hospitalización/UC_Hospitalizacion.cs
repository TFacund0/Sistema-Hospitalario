
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Hospitalizacion : UserControl
    {
        // Servicio para interactuar con la capa de negocio
        private readonly PacienteService pacienteService = new PacienteService();
        private readonly InternacionService internacionService = new InternacionService();
        private readonly HabitacionService habitacionService = new HabitacionService();
        private readonly CamaService camaService = new CamaService();

        // Lista para almacenar los pacientes obtenidos del servicio en el dgvInternaciones
        private List<InternacionDto> listaInternaciones = new List<InternacionDto>();

        // BindingSource para enlazar la lista de habitaciones al DataGridView
        private readonly BindingSource enlaceInternaciones = new BindingSource();

        // Evento para notificar cuando se solicita registrar una internación
        public event EventHandler RegistrarInternacionSolicitada;

        // ============================ CONSTRUCTOR DEL UC HOSPITALIZACIÓN ============================
        public UC_Hospitalizacion()
        {
            InitializeComponent();

            ConfigurarInformacionCajas();

            ConfigurarTablaInternaciones();
            ConfigurarEnlazadoDatosPacienteColumnas();
            CargarDatosDGV();
            CargarOpcionesDeFiltro();
        }


        // ============================ BOTÓN REGISTRAR INTERNACIÓN ============================
        private void BtnRegistrarInternacion_Click(object sender, EventArgs e)
        {
            RegistrarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

        // ============================ CONFIGURACION DE LAS CAJAS DE RESUMEN ============================

        // Método que configura la información en las cajas de resumen
        private void ConfigurarInformacionCajas()
        {
            int totalHabitaciones = habitacionService.TotalHabitaciones();
            lblTotalHabitaciones.Text = totalHabitaciones.ToString();

            int totalCamasDisponibles = camaService.TotalCamasXEstado("Disponible");
            lblDisponibles.Text = totalCamasDisponibles.ToString();

            int totalCamas = camaService.TotalCamas();

            float porcentajeDisponible = totalCamas > 0 ? ((float)totalCamasDisponibles / totalCamas) * 100 : 0;
            lblPorcentajeDisponibles.Text = $"{porcentajeDisponible:F2}% de camas disponibles";

            int totalCamasOcupadas = camaService.TotalCamasXEstado("Ocupada");
            lblOcupadas.Text = totalCamasOcupadas.ToString();

            float porcentajeOcupadas = totalCamas > 0 ? ((float)totalCamasOcupadas / totalCamas) * 100 : 0;
            lblPorcentajeOcupadas.Text = $"{porcentajeOcupadas:F2}% de camas ocupadas";

            int totalPacientesInternados = pacienteService.ContarPorEstadoId("internado");
            lblPacientesInternados.Text = totalPacientesInternados.ToString();
        }

        // ============================ CONFIGURACION DEL DATAGRIDVIEW ============================

        // Método que configura el DataGridView de Habitaciones en el UC Hospitalización
        private void ConfigurarTablaInternaciones()
        {
            dgvInternaciones.AutoGenerateColumns = false; // Desactiva la generación automática de columnas
            dgvInternaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de fila completa

            dgvInternaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajusta el tamaño de las columnas para llenar el espacio disponible
            dgvInternaciones.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Establece la fuente predeterminada para las celdas
            dgvInternaciones.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248); // Color de fondo para filas alternas

            dgvInternaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente en negrita para los encabezados de columna
            dgvInternaciones.ColumnHeadersHeight = 35; // Altura de los encabezados de columna
            dgvInternaciones.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke; // Color de fondo para los encabezados de columna
        }

        // Método que carga los datos en el DataGridView de Internaciones
        private void CargarDatosDGV()
        {
            listaInternaciones = internacionService.ListadoInternacionDtos();
            enlaceInternaciones.DataSource = listaInternaciones;
            dgvInternaciones.DataSource = enlaceInternaciones;
        }

        // Método que configura el enlace de datos entre las columnas del DataGridView y las propiedades del DTO
        private void ConfigurarEnlazadoDatosPacienteColumnas()
        {
            dgvInternaciones.AutoGenerateColumns = false;

            dgvInternaciones.Columns["colHabitacion"].DataPropertyName = "Nro_habitacion";
            dgvInternaciones.Columns["colPiso"].DataPropertyName = "Nro_piso";
            dgvInternaciones.Columns["colInternado"].DataPropertyName = "Internado";
            dgvInternaciones.Columns["colFechaIngreso"].DataPropertyName = "Fecha_ingreso";
            dgvInternaciones.Columns["colCama"].DataPropertyName = "Id_Cama";
        }

        private void CargarOpcionesDeFiltro()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] {
                                "Todos",
                                "Habitación",
                                "Piso",
                                "Internado",
                                "Fecha ingreso",
                                "Cama"
                            });
            cboCampo.SelectedIndex = 0;
        }


        // Aplica el filtro basado en el campo y el texto ingresado
        // Aplica el filtro basado en el campo y el texto ingresado
        private void AplicarFiltro(string campo, string texto)
        {
            string busqueda = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<InternacionDto> query = listaInternaciones;

            if (!string.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Habitación":
                        if (int.TryParse(busqueda, out int numHab))
                            query = query.Where(i => i.Nro_habitacion == numHab);
                        else
                            query = Enumerable.Empty<InternacionDto>();
                        break;

                    case "Piso":
                        if (int.TryParse(busqueda, out int piso))
                            query = query.Where(i => i.Nro_piso == piso);
                        else
                            query = Enumerable.Empty<InternacionDto>();
                        break;

                    case "Internado":
                        query = query.Where(i => (i.Internado ?? "")
                            .ToLower()
                            .Contains(busqueda));
                        break;

                    case "Fecha ingreso":
                        // Podés hacer búsqueda por día (dd/MM/yyyy)
                        if (DateTime.TryParse(busqueda, out DateTime fecha))
                        {
                            var soloFecha = fecha.Date;
                            query = query.Where(i => i.Fecha_ingreso.Date == soloFecha);
                        }
                        else
                        {
                            // o por string
                            query = query.Where(i =>
                                i.Fecha_ingreso.ToShortDateString()
                                .ToLower()
                                .Contains(busqueda));
                        }
                        break;

                    case "Cama":
                        if (int.TryParse(busqueda, out int idCama))
                            query = query.Where(i => i.Id_cama == idCama);
                        else
                            query = Enumerable.Empty<InternacionDto>();
                        break;

                    case "Todos":
                    default:
                        query = query.Where(i =>
                            i.Nro_habitacion.ToString().Contains(busqueda) ||
                            i.Nro_piso.ToString().Contains(busqueda) ||
                            (i.Internado ?? "").ToLower().Contains(busqueda) ||
                            i.Fecha_ingreso.ToShortDateString().ToLower().Contains(busqueda) ||
                            i.Id_cama.ToString().Contains(busqueda)
                        );
                        break;
                }
            }

            enlaceInternaciones.DataSource = query
                .OrderBy(i => i.Nro_habitacion)
                .ThenBy(i => i.Id_cama)
                .ToList();

            enlaceInternaciones.ResetBindings(false);
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
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            enlaceInternaciones.DataSource = listaInternaciones
                .OrderBy(i => i.Nro_habitacion)
                .ThenBy(i => i.Id_cama)
                .ToList();

            enlaceInternaciones.ResetBindings(false);
        }
    }
}
