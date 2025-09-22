using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.Servicios;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_VisualizarPaciente : UserControl
    {
        // DTO de detalle mostrado / editado en pantalla
        private PacienteDetalleDto _paciente;
        private bool _modoEdicion = false;

        // Servicio de negocio
        private readonly PacienteService _pacienteService = new PacienteService();

        // Eventos para que el contenedor reaccione
        public event EventHandler CancelarVisualizacionSolicitada;
        public event EventHandler<PacienteDetalleDto> PacienteActualizado;

        public UC_VisualizarPaciente(PacienteDetalleDto paciente)
        {
            InitializeComponent();
            _paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));

            ConfigurarUiSoloLectura();
            CargarDatos(_paciente);

            if (btnEditar != null)
            {
                btnEditar.Visible = true;
                btnEditar.Enabled = true;
                btnEditar.Text = "Editar";
                btnEditar.Click -= btnEditar_Click; // evitar doble suscripción si el diseñador ya lo hizo
                btnEditar.Click += btnEditar_Click;
            }

            if (btnCancelar != null)
            {
                btnCancelar.Click -= btnCancelar_Click;
                btnCancelar.Click += btnCancelar_Click;
            }
        }

        // ========================= CARGA / UI =========================

        private void CargarDatos(PacienteDetalleDto p)
        {
            txtNombre.Text = p.Nombre ?? string.Empty;
            txtApellido.Text = p.Apellido ?? string.Empty;
            txtObraSocial.Text = p.ObraSocial ?? string.Empty;
            txtDni.Text = p.DNI.ToString() ?? string.Empty;
            txtAfiliado.Text = p.NumeroAfiliado?.ToString() ?? string.Empty;
            txtDireccion.Text = p.Direccion ?? string.Empty;
            txtTelefono.Text = p.Telefono ?? string.Empty;
            txtObservaciones.Text = p.Observaciones ?? string.Empty;
            txtEstado.Text = p.Estado ?? string.Empty;

            var fecha = (p.FechaNacimiento == DateTime.MinValue) ? DateTime.Today : p.FechaNacimiento;
            dtpNacimiento.Value = fecha;
        }

        private void ConfigurarUiSoloLectura()
        {
            ToggleEdicion(false);
        }

        private void ToggleEdicion(bool habilitar)
        {
            _modoEdicion = habilitar;

            txtNombre.ReadOnly = !habilitar;
            txtApellido.ReadOnly = !habilitar;
            txtDni.ReadOnly = !habilitar;
            txtObraSocial.ReadOnly = !habilitar;
            txtAfiliado.ReadOnly = !habilitar;
            txtDireccion.ReadOnly = !habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtObservaciones.ReadOnly = !habilitar;
            txtEstado.ReadOnly = !habilitar;

            dtpNacimiento.Enabled = habilitar;

            if (btnEditar != null)
                btnEditar.Text = habilitar ? "Guardar" : "Editar";
        }

        // ========================= VALIDACIÓN =========================

        private bool ValidarDatos(out string error)
        {
            error = null;

            // Validaciones básicas
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

            if (!string.IsNullOrWhiteSpace(txtAfiliado.Text))
            {
                if (txtAfiliado.Text.Length > 20 || !Regex.IsMatch(txtAfiliado.Text, @"^\d+$"))
                { error = "N° de afiliado debe ser numérico y de hasta 20 dígitos."; return false; }
            }

            if (dtpNacimiento.Value > DateTime.Today)
            { error = "La fecha de nacimiento no puede ser futura."; return false; }

            return true;
        }

        // ========================= ACCIONES =========================

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                // Pasar a modo edición
                ToggleEdicion(true);
                return;
            }

            // Guardar
            if (!ValidarDatos(out var msg))
            {
                MessageBox.Show(msg, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar cambios al DTO local
            _paciente.Nombre = txtNombre.Text.Trim();
            _paciente.Apellido = txtApellido.Text.Trim();
            _paciente.DNI = int.TryParse(txtDni.Text.Trim(), out var dni) ? dni : 0;
            _paciente.ObraSocial = txtObraSocial.Text.Trim();
            _paciente.NumeroAfiliado = int.TryParse(txtAfiliado.Text.Trim(), out var nro) ? nro : (int?)null;
            _paciente.Direccion = txtDireccion.Text.Trim();
            _paciente.Telefono = txtTelefono.Text.Trim();
            _paciente.Observaciones = txtObservaciones.Text.Trim();
            _paciente.Estado = txtEstado.Text.Trim();
            _paciente.FechaNacimiento = dtpNacimiento.Value;

            try
            {
                // Llamada a NEGOCIO para actualizar en BD
                var r = _pacienteService.Editar(_paciente);

                if (!r.Ok)
                {
                    MessageBox.Show(r.Error ?? "No se pudo actualizar el paciente.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // OK → volver a solo lectura
                ToggleEdicion(false);

                // Disparar evento hacia el contenedor para refrescos externos si quiere
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Revertir cambios visibles volviendo a cargar el DTO local
            CargarDatos(_paciente);
            ToggleEdicion(false);

            CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }
}
