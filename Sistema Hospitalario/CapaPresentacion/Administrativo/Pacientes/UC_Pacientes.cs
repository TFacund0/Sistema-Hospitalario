using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.Servicios;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Pacientes : UserControl
    {
        // BindingSource para enlazar la lista de pacientes al DataGridView
        private readonly BindingSource enlacePacientes = new BindingSource();

        // Servicio para interactuar con la capa de negocio
        private readonly PacienteService pacienteService = new PacienteService();

        // Lista maestra de pacientes cargada desde el servicio
        private List<PacienteListadoDto> listaPacientes = new List<PacienteListadoDto>();

        // Evento que notifica al formulario padre que se solicitó registrar un nuevo paciente
        public event EventHandler RegistrarPacienteSolicitado;

        // Evento que pasa el detalle del paciente seleccionado
        public event EventHandler<PacienteDetalleDto> VerPacienteSolicitado;

        // ===================== CONSTRUCTOR DEL UC PACIENTES =====================
        public UC_Pacientes()
        {
            InitializeComponent();

            ConfigurarTablaActividad();
            ConfigurarLabelsInformacion();

            CargarOpcionesDeFiltro();
            ConfigurarEnlazadoDeColumnas();
            CargarDesdeServicio();

            dgvPacientes.CellContentClick += dgvPacientes_CellContentClick;
        }

        // ===================== BOTÓN NUEVO PACIENTE =====================
        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            RegistrarPacienteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // Metodo para configurar el DataGridView de pacientes
        private void ConfigurarTablaActividad()
        {
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de fila completa

            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar columnas al ancho del control
            dgvPacientes.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Fuente para las celdas
            dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248); // Color alternativo para filas

            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente para encabezados
            dgvPacientes.ColumnHeadersHeight = 35; // Altura de encabezado
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke; // Color de fondo del encabezado
        }

        private async void ConfigurarLabelsInformacion()
        {
            lblTotalPacientes.Text = (await pacienteService.ContarPorEstadoIdAsync(1)).ToString();
            lblTotalInternados.Text = (await pacienteService.ContarPorEstadoIdAsync(2)).ToString();
            lblTotalEgresados.Text = (await pacienteService.ContarPorEstadoIdAsync(3)).ToString();
        }


        // Metodo para cargar la lista de pacientes desde el servicio
        private void CargarDesdeServicio()
        {
            // Trae la lista “plana” para el grid desde la capa de negocio (BD)
            listaPacientes = pacienteService.ListarPacientes(); // List<PacienteListadoDto>
            enlacePacientes.DataSource = listaPacientes;
            dgvPacientes.DataSource = enlacePacientes;
        }

        // Configura el enlace entre las columnas del DataGridView y las propiedades del DTO
        private void ConfigurarEnlazadoDeColumnas()
        {
            dgvPacientes.AutoGenerateColumns = false;

            dgvPacientes.Columns["colPaciente"].DataPropertyName = "Paciente"; 
            dgvPacientes.Columns["colDNI"].DataPropertyName = "DNI";      
            dgvPacientes.Columns["colEdad"].DataPropertyName = "Edad";    
            dgvPacientes.Columns["colEstado"].DataPropertyName = "Estado";
        }

        // Maneja el evento de clic en el contenido de una celda del DataGridView
        private void dgvPacientes_CellContentClick(object s, DataGridViewCellEventArgs evento)
        {
            // Ignorar clics en encabezados o fuera de filas válidas
            if (evento.RowIndex < 0) return;

            // Si se hizo clic en el botón "Ver" de la columna de acción
            if (dgvPacientes.Columns[evento.ColumnIndex].Name == "colAccion")
            {
                // Obtener el paciente seleccionado
                var paciente = enlacePacientes[evento.RowIndex] as PacienteListadoDto;
                if (paciente == null) return;

                // Traer el detalle desde negocio/datos
                var detalle = pacienteService.ObtenerDetalle(paciente.Id);
                
                if (detalle == null)
                {
                    MessageBox.Show("No se encontró el paciente seleccionado.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Disparar el evento hacia el contenedor (Form/Padre) para que lo muestre
                VerPacienteSolicitado?.Invoke(this, detalle);
            }
        }

        // ===================== FILTRADO =====================

        // Carga las opciones de filtro en el ComboBox
        private void CargarOpcionesDeFiltro()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "DNI", "Estado" });
            cboCampo.SelectedIndex = 0;
        }

        // Aplica el filtro basado en el campo y el texto ingresado
        private void AplicarFiltro(string campo, string texto)
        {
            // Normaliza el texto para comparación
            string busqueda = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<PacienteListadoDto> query = listaPacientes;

            // Si hay texto, aplica el filtro según el campo seleccionado
            if (!string.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t => (t.Paciente ?? "").ToLower().Contains(busqueda));
                        break;
                    case "DNI":
                        int dniBuscado = int.Parse(busqueda);
                        query = query.Where(t => t.DNI == dniBuscado);
                        break;  
                    case "Estado":
                        query = query.Where(t => (t.Estado ?? "").ToLower().Contains(busqueda));
                        break;
                    default:
                        query = query.Where(t =>
                            (t.Paciente ?? "").ToLower().Contains(busqueda) ||
                            t.DNI.ToString().Contains(busqueda) ||
                            (t.Estado ?? "").ToLower().Contains(busqueda));
                        break;
                }
            }

            enlacePacientes.DataSource = query.OrderBy(t => t.Paciente).ToList();
            enlacePacientes.ResetBindings(false);
        }

        // Limpia el filtro cuando se hace clic en el botón Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            enlacePacientes.DataSource = listaPacientes.OrderBy(t => t.Paciente).ToList();
            enlacePacientes.ResetBindings(false);
        }

        // Aplica el filtro cuando se hace clic en el botón Buscar
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }
    }
}
