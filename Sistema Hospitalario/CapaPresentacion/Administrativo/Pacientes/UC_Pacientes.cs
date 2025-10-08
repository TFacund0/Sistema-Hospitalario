﻿using System;
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

        // Lista completa de pacientes cargada desde el servicio
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
            ConfigurarEnlazadoDatosPacienteColumnas();
            CargarPacientesDatagridview();

            dgvPacientes.CellContentClick += dgvPacientes_CellContentClick;
        }

        // ===================== CONFIGURACIONES DEL DATAGRIDVIEW EN DISEÑO =====================
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

        // ===================== CONFIGURACIÓN DE LABELS DE INFORMACIÓN =====================
        private async void ConfigurarLabelsInformacion()
        {
            lblTotalPacientes.Text = (await pacienteService.ContarPorEstadoIdAsync(1)).ToString();
            lblTotalInternados.Text = (await pacienteService.ContarPorEstadoIdAsync(2)).ToString();
            lblTotalEgresados.Text = (await pacienteService.ContarPorEstadoIdAsync(3)).ToString();
        }


        // ===================== CARGA Y ENLAZADO DE DATOS =====================
        
        // Carga la lista de pacientes desde el servicio y la enlaza al DataGridView
        private void CargarPacientesDatagridview()
        {
            // Trae la lista “plana” para el grid desde la capa de negocio (BD)
            listaPacientes = pacienteService.ListarPacientes(); // List<PacienteListadoDto>
            enlacePacientes.DataSource = listaPacientes;
            dgvPacientes.DataSource = enlacePacientes;
        }

        // Configura el enlace entre las columnas del DataGridView y las propiedades del DTO
        private void ConfigurarEnlazadoDatosPacienteColumnas()
        {
            dgvPacientes.AutoGenerateColumns = false;

            dgvPacientes.Columns["colPaciente"].DataPropertyName = "Paciente"; 
            dgvPacientes.Columns["colDNI"].DataPropertyName = "DNI";      
            dgvPacientes.Columns["colEdad"].DataPropertyName = "Edad";    
            dgvPacientes.Columns["colEstado"].DataPropertyName = "Estado";
        }


        // ===================== BOTÓN NUEVO PACIENTE =====================
        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            RegistrarPacienteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // ===================== BOTÓN BUSCAR PACIENTE =====================
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            var campo = cboCampo.SelectedItem?.ToString() ?? "Todos";
            var texto = txtBuscar.Text;
            AplicarFiltro(campo, texto);
        }


        // ===================== BOTÓN LIMPIAR BUSCADOR PACIENTE =====================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboCampo != null) cboCampo.SelectedIndex = 0;

            enlacePacientes.DataSource = listaPacientes.OrderBy(t => t.Paciente).ToList();
            enlacePacientes.ResetBindings(false);
        }

        // ===================== BUSQUEDA DE PACIENTE POR FILTRADO =====================

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

            // Actualiza el BindingSource con los resultados filtrados
            enlacePacientes.DataSource = query.OrderBy(t => t.Paciente).ToList();
            enlacePacientes.ResetBindings(false);
        }

        // ===================== VER DETALLE DE PACIENTE =====================
        // Maneja el evento de clic en el DataGridView para ver el detalle del paciente
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
    }
}
