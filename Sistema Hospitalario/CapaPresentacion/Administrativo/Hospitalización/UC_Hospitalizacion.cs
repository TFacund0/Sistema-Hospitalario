
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
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService;

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
        private List<ListadoInternacionDto> listaInternaciones = new List<ListadoInternacionDto>();

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
        }

        // ============================ BOTÓN REGISTRAR INTERNACIÓN ============================
        private void btnRegistrarInternacion_Click(object sender, EventArgs e)
        {
            RegistrarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

        // ============================ CONFIGURACION DE LAS CAJAS DE RESUMEN ============================

        // Método que configura la información en las cajas de resumen
        private async void ConfigurarInformacionCajas()
        {
            int totalHabitaciones = await habitacionService.TotalHabitaciones();
            lblTotalHabitaciones.Text = totalHabitaciones.ToString();

            int totalCamasDisponibles = await camaService.TotalCamasXEstado(8, "Disponible");
            lblDisponibles.Text = totalCamasDisponibles.ToString();

            int totalCamas = await camaService.TotalCamas();

            float porcentajeDisponible = ((float)totalCamasDisponibles / (float)totalCamas) * 100;
            lblPorcentajeDisponibles.Text = porcentajeDisponible.ToString() + "% de camas disponibles";

            int totalCamasOcupadas = await camaService.TotalCamasXEstado(9, "Ocupada");
            lblOcupadas.Text = totalCamasOcupadas.ToString();

            float porcentajeOcupadas = ((float)totalCamasOcupadas / (float)totalCamas) * 100;
            lblPorcentajeOcupadas.Text = porcentajeOcupadas.ToString() + "% de camas ocupadas";

            int totalPacientesInternados = await pacienteService.ContarPorEstadoIdAsync(2);
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
            listaInternaciones = internacionService.listadoInternacionDtos();
            enlaceInternaciones.DataSource = listaInternaciones;
            dgvInternaciones.DataSource = listaInternaciones;
        }

        // Método que configura el enlace de datos entre las columnas del DataGridView y las propiedades del DTO
        private void ConfigurarEnlazadoDatosPacienteColumnas()
        {
            dgvInternaciones.AutoGenerateColumns = false;

            dgvInternaciones.Columns["colHabitacion"].DataPropertyName = "Nro_habitacion";
            dgvInternaciones.Columns["colPiso"].DataPropertyName = "Nro_piso";
            dgvInternaciones.Columns["colInternado"].DataPropertyName = "Internado";
            dgvInternaciones.Columns["colFechaIngreso"].DataPropertyName = "Fecha_ingreso";
            dgvInternaciones.Columns["colCama"].DataPropertyName = "cama";
            dgvInternaciones.Columns["colTipo"].DataPropertyName = "Tipo_habitacion";
        }
    }
}
