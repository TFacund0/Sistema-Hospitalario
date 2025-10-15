using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_Turnos : UserControl
    {
        // Acceso a los servicios de Turnos
        TurnoService _turnoService = new TurnoService();

        // Listado de turnos
        List<ListadoTurno> _listadoTurnos = new List<ListadoTurno>();

        // Enlace de datos para el DataGridView
        BindingSource enlaceTurnos = new BindingSource();

        // ============== EVENTOS ==============
        public event EventHandler<TurnoDTO> VerTurnoSolicitado;
        public event EventHandler RegistrarTurnoSolicitado;


        // ============== CONSTRUCTOR UC TURNOS ==============
        public UC_Turnos()
        {
            InitializeComponent();     

            ConfigurarActividad();
            ConfigurarLabelsDatosTurno();

            ConfigurarEnlazadoDatosTurnoColumnas();
            CargarTurnosDGV();
            CargarOpcionesDeFiltro();

            dgvTurnos.CellContentClick += dgvTurnos_CellContentClick;
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

            dgvTurnos.DataSource = null;

            dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTurnos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTurnos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvTurnos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTurnos.ColumnHeadersHeight = 35;
            dgvTurnos.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        // ===================== CONFIGURAR DATOS CAJAS DE TEXTO =====================
        private void ConfigurarLabelsDatosTurno()
        {
            lblTurnosPendientes.Text = _turnoService.CantidadTurnosPendientes().ToString();
            lblTurnosCompletados.Text = _turnoService.CantidadTurnosPorEstado("Atendido").ToString();
            lblTurnosCurso.Text = _turnoService.CantidadTurnosPorEstado("En Curso").ToString();
            lblTurnosHoy.Text = _turnoService.CantidadTurnosPorEstado("Pendiente").ToString();
        }
        

        // ===================== ENLAZADO DE DATOS =====================
        // Configura el enlace de datos entre las columnas del DataGridView y las propiedades del objeto ListadoTurno
        private void ConfigurarEnlazadoDatosTurnoColumnas()
        {
            dgvTurnos.AutoGenerateColumns = false;

            dgvTurnos.Columns["colPaciente"].DataPropertyName = "Paciente";
            dgvTurnos.Columns["colMedico"].DataPropertyName = "Medico";
            dgvTurnos.Columns["colHora"].DataPropertyName = "FechaTurno";
            dgvTurnos.Columns["colEstado"].DataPropertyName = "Estado";
        }

        // ===================== CARGA DE DATOS =====================
        // Carga los turnos en el DataGridView
        public void CargarTurnosDGV()
        {
            _listadoTurnos = _turnoService.ListarTurnos();
            enlaceTurnos.DataSource = _listadoTurnos;
            dgvTurnos.DataSource = enlaceTurnos;
        }

        // ===================== BOTÓN BUSCAR TURNO =====================
        // Filtra los turnos según el campo y el texto ingresado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var campo = cboCampoFiltroTurno.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscarTurno.Text;
            AplicarFiltro(campo, texto);
        }

        // ===================== BOTÓN LIMPIAR FILTRO =====================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarTurno.Clear();
            if (cboCampoFiltroTurno != null) cboCampoFiltroTurno.SelectedIndex = 0;

            enlaceTurnos.DataSource = _listadoTurnos.OrderBy(t => t.FechaTurno).ToList();
            enlaceTurnos.ResetBindings(false);
        }


        // ===================== MÉTODOS DE FILTRADO =====================
        // Carga las opciones de filtro en el ComboBox
        private void CargarOpcionesDeFiltro()
        {
            if (cboCampoFiltroTurno == null) return;

            cboCampoFiltroTurno.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampoFiltroTurno.Items.Clear();
            cboCampoFiltroTurno.Items.AddRange(new string[] { "Todos", "Paciente", "Fecha", "Estado" });
            cboCampoFiltroTurno.SelectedIndex = 0;
        }

        // Aplica el filtro basado en el campo y el texto ingresado
        private void AplicarFiltro(string campo, string texto)
        {
            // Normaliza el texto para comparación
            string busqueda = (texto ?? "").Trim().ToLowerInvariant();
            IEnumerable<ListadoTurno> query = _listadoTurnos;

            // Si hay texto, aplica el filtro según el campo seleccionado
            if (!string.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Paciente":
                        query = query.Where(t => (t.Paciente ?? "").ToLower().Contains(busqueda));
                        break;
                    case "Hora":
                        query = query.Where(t => t.FechaTurno.ToString("g").ToLower().Contains(busqueda));
                        break;
                    case "Estado":
                        query = query.Where(t => (t.Estado ?? "").ToLower().Contains(busqueda));
                        break;
                    default:
                        query = query.Where(t =>
                            (t.Paciente ?? "").ToLower().Contains(busqueda) ||
                            t.FechaTurno.ToString("g").ToLower().Contains(busqueda) ||
                            (t.Estado ?? "").ToLower().Contains(busqueda));
                        break;
                }
            }

            // Actualiza el BindingSource con los resultados filtrados
            enlaceTurnos.DataSource = query.OrderBy(t => t.FechaTurno).ToList();
            enlaceTurnos.ResetBindings(false);
        }

        // ===================== EVENTO CLIC EN CELDA DEL DATAGRIDVIEW =====================
        private void dgvTurnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorar clics en encabezados
            if (e.RowIndex < 0) return;

            // Si se hizo clic en el botón "Ver" de la columna de acción
            if (e.RowIndex >= 0 && e.RowIndex < enlaceTurnos.Count)
                {
                var turno = enlaceTurnos[e.RowIndex] as ListadoTurno;
                if (turno == null) return;

                // Traer el detalle desde negocio/datos
                var detalle = _turnoService.ObtenerDetalle(turno.Id_turno);

                if (detalle == null)
                {
                    MessageBox.Show("No se encontró el paciente seleccionado.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Disparar el evento hacia el contenedor (Form/Padre) para que lo muestre
                VerTurnoSolicitado?.Invoke(this, detalle);
            }
        }
    }
}
