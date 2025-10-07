using Sistema_Hospitalario.CapaNegocio;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO.EstadoPacienteDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_VisualizarPaciente : UserControl
    {
        // DTO local para mantener el estado actual del paciente
        private PacienteDetalleDto _paciente;

        // Modo edición
        private bool _modoEdicion = false;

        // Servicio de negocio
        private readonly PacienteService _pacienteService = new PacienteService();
        private readonly EstadoPacienteService _estadoService = new EstadoPacienteService();

        // Evento para notificar que se canceló la visualización (volver al listado)
        public event EventHandler CancelarVisualizacionSolicitada;

        // Evento para notificar que el paciente fue actualizado
        public event EventHandler<PacienteDetalleDto> PacienteActualizado;

        // ========================= CONSTRUCTOR UC VISUALIZAR PACIENTE =========================
        public UC_VisualizarPaciente(PacienteDetalleDto paciente)
        {
            InitializeComponent();

            _paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));

            // 1) Cargar lista de estados
            CargarEstadosEnCombo();

            // 2) Configurar UI inicial
            ConfigurarUiSoloLectura();
            CargarDatos(_paciente);
            SelectEventosPaciente();

            if (btnEditar != null)
            {
                btnEditar.Visible = true;
                btnEditar.Enabled = true;
                btnEditar.Text = "Editar";
                btnEditar.Click -= btnEditar_Click;
                btnEditar.Click += btnEditar_Click;
            }

            if (btnCancelar != null)
            {
                btnCancelar.Click -= btnCancelar_Click;
                btnCancelar.Click += btnCancelar_Click;
            }
        }

        private void CargarDatos(PacienteDetalleDto p)
        {
            txtNombre.Text = p.Nombre ?? string.Empty;
            txtApellido.Text = p.Apellido ?? string.Empty;
            txtDni.Text = p.DNI.ToString();
            txtEmail.Text = p.Email ?? string.Empty;
            txtDireccion.Text = p.Direccion ?? string.Empty;
            txtTelefono.Text = p.Telefono ?? string.Empty;
            txtObservaciones.Text = p.Observaciones ?? string.Empty;

            // ✅ Mostrar en TextBox (modo lectura)
            txtEstado.Text = (p.Estado ?? string.Empty).Trim();

            // ✅ Preseleccionar en el Combo (para cuando entres en edición)
            SeleccionarEstadoPorNombre(txtEstado.Text);

            var fecha = (p.FechaNacimiento == DateTime.MinValue) ? DateTime.Today : p.FechaNacimiento;
            dtpNacimiento.Value = fecha;
        }



        private void SelectEventosPaciente()
        {
            // Abrir automáticamente al ganar foco o click
            cbEstadoInicial.Enter += (s, e) => cbEstadoInicial.DroppedDown = true;
            cbEstadoInicial.MouseDown += (s, e) => cbEstadoInicial.DroppedDown = true;
        }

        private void CargarEstadosEnCombo()
        {
            var estados = _estadoService.ListarEstados(); // List<EstadoPacienteDto> { Id, Nombre }

            var lista = new List<EstadoPacienteDto>
            {
                new EstadoPacienteDto { Id = 0, Nombre = "— Seleccioná —" }
            };
            lista.AddRange(estados);

            // ✅ Primero Display/Value, después DataSource
            cbEstadoInicial.DisplayMember = nameof(EstadoPacienteDto.Nombre);
            cbEstadoInicial.ValueMember = nameof(EstadoPacienteDto.Id);
            cbEstadoInicial.DataSource = lista;

            cbEstadoInicial.SelectedIndex = 0;

            // Recomendado en el diseñador o acá:
            cbEstadoInicial.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SeleccionarEstadoPorNombre(string nombreEstado)
        {
            if (string.IsNullOrWhiteSpace(nombreEstado))
            {
                cbEstadoInicial.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < cbEstadoInicial.Items.Count; i++)
            {
                var it = cbEstadoInicial.Items[i] as EstadoPacienteDto;
                if (it != null && string.Equals(it.Nombre, nombreEstado, StringComparison.OrdinalIgnoreCase))
                {
                    cbEstadoInicial.SelectedIndex = i;
                    return;
                }
            }

            // Si no se encontró, dejar en “Seleccioná”
            cbEstadoInicial.SelectedIndex = 0;
        }



        // ========================= CONFIGURACIÓN UI =========================
        private void ConfigurarUiSoloLectura()
        {
            ToggleEdicion(false);
        }

        // Alternar entre modo edición y solo lectura
        private void ToggleEdicion(bool habilitar)
        {
            _modoEdicion = habilitar;

            // Textos editables
            txtNombre.ReadOnly = !habilitar;
            txtApellido.ReadOnly = !habilitar;
            txtDni.ReadOnly = !habilitar;
            txtEmail.ReadOnly = !habilitar;
            txtDireccion.ReadOnly = !habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtObservaciones.ReadOnly = !habilitar;
            dtpNacimiento.Enabled = habilitar;

            // Estado: en solo lectura mostramos txtEstado, en edición usamos el combo
            txtEstado.Visible = !habilitar;

            cbEstadoInicial.Visible = habilitar;
            cbEstadoInicial.Enabled = habilitar;

            if (habilitar)
            {
                // Cuando entro en edición, el combo debe reflejar lo que se estaba viendo
                SeleccionarEstadoPorNombre(txtEstado.Text);
            }

            if (btnEditar != null)
                btnEditar.Text = habilitar ? "Guardar" : "Editar";
        }


        // ========================= VALIDACIONES =========================
        private bool ValidarDatos(out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Length > 50)
            { error = "Nombre es obligatorio (máx. 50)."; return false; }

            if (string.IsNullOrWhiteSpace(txtApellido.Text) || txtApellido.Text.Length > 50)
            { error = "Apellido es obligatorio (máx. 50)."; return false; }

            if (string.IsNullOrWhiteSpace(txtDni.Text) || txtDni.Text.Length > 15 || !Regex.IsMatch(txtDni.Text, @"^\d+$"))
            { error = "DNI es obligatorio, numérico y de hasta 15 dígitos."; return false; }

            if (!string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                if (txtTelefono.Text.Length > 15 || !Regex.IsMatch(txtTelefono.Text, @"^\d+$"))
                { error = "Teléfono debe ser numérico y de hasta 15 dígitos."; return false; }
            }

            if (!string.IsNullOrWhiteSpace(txtDireccion.Text) && txtDireccion.Text.Length > 50)
            { error = "Dirección no puede superar 50 caracteres."; return false; }

            if (!string.IsNullOrWhiteSpace(txtObservaciones.Text) && txtObservaciones.Text.Length > 200)
            { error = "Observaciones no puede superar 200 caracteres."; return false; }

            if (_modoEdicion)
            {
                var sel = cbEstadoInicial.SelectedItem as EstadoPacienteDto;
                if (sel == null || sel.Id == 0)
                {
                    error = "Seleccioná un estado válido.";
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (txtEmail.Text.Length > 100 || !Regex.IsMatch(txtEmail.Text,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                { error = "Email debe ser válido y de hasta 100 caracteres."; return false; }
            }

            if (dtpNacimiento.Value > DateTime.Today)
            { error = "La fecha de nacimiento no puede ser futura."; return false; }

            return true;
        }

        // ========================= EVENTOS BOTONES =========================

        // Alternar entre modo edición y guardar cambios
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                ToggleEdicion(true);
                return;
            }

            if (!ValidarDatos(out var msg))
            {
                MessageBox.Show(msg, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mapear cambios al DTO local
            _paciente.Nombre = txtNombre.Text.Trim();
            _paciente.Apellido = txtApellido.Text.Trim();
            _paciente.DNI = int.TryParse(txtDni.Text.Trim(), out var dni) ? dni : 0;
            _paciente.Direccion = txtDireccion.Text.Trim();
            _paciente.Telefono = txtTelefono.Text.Trim();
            _paciente.Observaciones = txtObservaciones.Text.Trim();
            _paciente.Email = txtEmail.Text.Trim();
            _paciente.FechaNacimiento = dtpNacimiento.Value;

            // Estado: ahora viene del combo
            var estadoSel = cbEstadoInicial.SelectedItem as EstadoPacienteDto;
            if (estadoSel != null && estadoSel.Id > 0)
            {
                // Si tu PacienteDetalleDto tiene sólo el nombre:
                _paciente.Estado = estadoSel.Nombre;

                // Si además tenés Id de estado en el DTO, setéalo también:
                // _paciente.IdEstado = estadoSel.Id;
            }
            else
            {
                MessageBox.Show("Seleccioná un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var r = _pacienteService.Editar(_paciente);
                if (!r.Ok)
                {
                    MessageBox.Show(r.Error ?? "No se pudo actualizar el paciente.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Volver a solo lectura y refrescar controles visibles
                ToggleEdicion(false);
                CargarDatos(_paciente); // aseguro que txtEstado muestre el estado nuevo

                PacienteActualizado?.Invoke(this, _paciente);

                MessageBox.Show("Paciente actualizado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Cancelar edición y volver a cargar datos originales
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarDatos(_paciente); // vuelve a poner el estado viejo en txt y combo
            ToggleEdicion(false);
            CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

    }
}
