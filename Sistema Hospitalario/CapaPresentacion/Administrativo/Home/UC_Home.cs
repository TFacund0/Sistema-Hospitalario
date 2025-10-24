using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.HomeDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HomeService;
using Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService;
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
    public partial class UC_HomeGerente : UserControl
    {
        // ========= Campos/miembros del UC/Form =========
        private List<HomeDto> listaActividad = new List<HomeDto>();   // Cargada desde HomeService
        private BindingSource enlaceActividad = new BindingSource();  // DataSource del DataGridView


        // ============================ CONSTRUCTOR DEL UC HOME ADMINISTRATIVO ============================
        public UC_HomeGerente()
        {
            InitializeComponent();

            CargarInformacionPaneles();
            ConfigurarTablaActividad();
            ConfigurarEnlazadoDatosColumnas();

            // Asegura que se ejecute el Load
            this.Load += Home_Load;
        }

        // Método que configuran la información de las estadísticas en el UC Home Gerente
        private async void CargarInformacionPaneles()
        {
            PacienteService pacienteService = new PacienteService();
            int cantidadPacientes = await pacienteService.ContarPorEstadoIdAsync(1);

            CamaService camaService = new CamaService();
            int cantidadCamasOcupadas = await camaService.TotalCamasXEstado(9, "Ocupada");
            int cantidadCamas = await camaService.TotalCamas();

            InternacionService internacionService = new InternacionService();
            int cantidadInternaciones = internacionService.listadoInternacionDtos().Count;

            string cantPacientes = cantidadPacientes.ToString();
            string cantCamasOcupadas = cantidadCamasOcupadas.ToString();
            string totalCamas = cantidadCamas.ToString();
            string cantInternaciones = cantidadInternaciones.ToString();

            if (
                int.TryParse(cantPacientes, out int valorPaciente) && valorPaciente >= 0 && valorPaciente < 1000 &&
                int.TryParse(cantCamasOcupadas, out int valorCamasOcupadas) && valorCamasOcupadas >= 0 && valorCamasOcupadas < 1000 &&
                int.TryParse(totalCamas, out int valorCamas) && valorCamas >= 0 && valorCamas < 1000 && valorCamasOcupadas <= valorCamas &&
                int.TryParse(cantInternaciones, out int valorInternaciones) && valorInternaciones >= 0 && valorInternaciones < 1000
                )
            {
                string porcentajeCamas = (((float)valorCamasOcupadas / (float)valorCamas) * 100).ToString() + "%";

                lblPacientesActivos.Text = cantPacientes;
                lblCamasOcupadas.Text = cantCamasOcupadas + "/" + totalCamas;
                lblPorcentajeCamas.Text = porcentajeCamas + " de ocupación";
                lblInternaciones.Text = cantInternaciones;
            }
            else
            {
                MessageBox.Show("Error: Los valores de los paneles deben ser números enteros no negativos y dentro del rango permitido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método que configura el DataGridView de Actividad Reciente en el UC Home Gerente
        private void ConfigurarTablaActividad()
        {
            dgvActividad.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de fila completa

            dgvActividad.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Establece la fuente predeterminada para las celdas
            dgvActividad.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248); // Color de fondo para filas alternas

            dgvActividad.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente en negrita para los encabezados de columna
            dgvActividad.ColumnHeadersHeight = 35; // Altura de los encabezados de columna
            dgvActividad.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke; // Color de fondo para los encabezados de columna
        }

        private void ConfigurarEnlazadoDatosColumnas()
        {
            dgvActividad.AutoGenerateColumns = false;

            dgvActividad.Columns["colNombre"].DataPropertyName = "Nombre";
            dgvActividad.Columns["colApellido"].DataPropertyName = "Apellido";
            dgvActividad.Columns["colAccion"].DataPropertyName = "Accion";
            dgvActividad.Columns["colHorario"].DataPropertyName = "Horario";
            dgvActividad.Columns["colTipo"].DataPropertyName = "Tipo";
        }

        private async Task CargarActividadRecienteAsync()
        {
            var homeService = new HomeService();

            // Cargamos la lista de la clase
            var datos = await homeService.ListarActividadRecienteAsync(100);
            listaActividad = datos ?? new List<HomeDto>();

            // Usamos el BindingSource de la clase
            enlaceActividad.DataSource = listaActividad
                .OrderByDescending(t => t.Horario)
                .ToList();

            dgvActividad.AutoGenerateColumns = false;
            dgvActividad.DataSource = enlaceActividad;

            dgvActividad.Columns["colHorario"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }


        private async void Home_Load(object sender, EventArgs e)
        {
            await CargarActividadRecienteAsync();
            CargarOpcionesDeFiltroHome();
        }

        // ===================== BOTÓN BUSCAR =====================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltroHome(campo, texto);
        }

        // ===================== BOTÓN LIMPIAR =====================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            enlaceActividad.DataSource = listaActividad
                .OrderByDescending(t => t.Horario)
                .ToList();
            enlaceActividad.ResetBindings(false);
        }

        // ===================== OPCIONES DE FILTRO =====================
        private void CargarOpcionesDeFiltroHome()
        {
            if (cboCampo == null) return;

            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Clear();
            // Mismos nombres que verás en el Combo
            cboCampo.Items.AddRange(new[] { "Todos", "Paciente", "Acción", "Tipo", "Fecha" });
            cboCampo.SelectedIndex = 0;
        }

        // ===================== APLICAR FILTRO =====================
        private void AplicarFiltroHome(string campo, string texto)
        {
            string busqueda = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<HomeDto> query = listaActividad;

            if (!string.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t =>
                        {
                            var nombre = (t.Nombre ?? "").ToLowerInvariant();
                            var apellido = (t.Apellido ?? "").ToLowerInvariant();
                            var full1 = (nombre + " " + apellido).Trim();
                            var full2 = (apellido + " " + nombre).Trim();
                            return nombre.Contains(busqueda) ||
                                   apellido.Contains(busqueda) ||
                                   full1.Contains(busqueda) ||
                                   full2.Contains(busqueda);
                        });
                        break;

                    case "Acción":
                        query = query.Where(t => (t.Accion ?? "").ToLowerInvariant().Contains(busqueda));
                        break;

                    case "Tipo":
                        // Puede venir null cuando es "Paciente registrado"
                        query = query.Where(t => (t.Tipo ?? "").ToLowerInvariant().Contains(busqueda));
                        break;

                    case "Fecha":
                        // Busca por coincidencia de texto en la fecha formateada,
                        // y si el texto parece fecha válida, filtra por igualdad de día.
                        query = query.Where(t => t.Horario.ToString("dd/MM/yyyy HH:mm").ToLowerInvariant()
                                                        .Contains(busqueda)
                                               || t.Horario.ToString("dd/MM/yyyy").ToLowerInvariant()
                                                        .Contains(busqueda));

                        if (DateTime.TryParse(texto, out var fechaBuscada))
                        {
                            var f = fechaBuscada.Date;
                            query = query.Where(t => t.Horario.Date == f);
                        }
                        break;

                    default: // "Todos"
                        query = query.Where(t =>
                            ((t.Nombre ?? "").ToLowerInvariant() + " " + (t.Apellido ?? "").ToLowerInvariant())
                                .Contains(busqueda)
                            || (t.Accion ?? "").ToLowerInvariant().Contains(busqueda)
                            || (t.Tipo ?? "").ToLowerInvariant().Contains(busqueda)
                            || t.Horario.ToString("dd/MM/yyyy HH:mm").ToLowerInvariant().Contains(busqueda)
                        );
                        break;
                }
            }

            // Siempre mostrar lo más reciente primero
            enlaceActividad.DataSource = query
                .OrderByDescending(t => t.Horario)
                .ToList();

            enlaceActividad.ResetBindings(false);
        }

    }
}