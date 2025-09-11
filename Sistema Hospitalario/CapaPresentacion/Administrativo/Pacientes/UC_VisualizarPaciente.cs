using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Permite acceder a la clase PacienteDTO
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_VisualizarPaciente : UserControl
    {
        private PacienteDTO _paciente;          // paciente mostrado
        private bool _modoEdicion = false;
        
        // Eventos para comunicar acciones al contenedor
        public event EventHandler CancelarVisualizacionSolicitada;
        public event EventHandler<PacienteDTO> PacienteActualizado;

        public UC_VisualizarPaciente(PacienteDTO paciente)
        {
            InitializeComponent();
            _paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));
            CargarDatos(_paciente);
            ToggleEdicion(false);   // Arranca solo lectura
        }

        private void CargarDatos(PacienteDTO p_paciente)
        {
            txtNombre.Text = p_paciente.nombre;
            txtApellido.Text = p_paciente.apellido;
            txtDni.Text = p_paciente.dni.ToString();
            txtObraSocial.Text = p_paciente.obraSocial;
            txtAfiliado.Text = p_paciente.nroAfiliado.ToString();
            txtDireccion.Text = p_paciente.direccion;
            txtTelefono.Text = p_paciente.telefono.ToString();
            dtpNacimiento.Value = p_paciente.FechaNacimiento == default ? DateTime.Now : p_paciente.FechaNacimiento;
            txtObservaciones.Text = p_paciente.observaciones;
            txtHabitacion.Text = p_paciente.habitacion.ToString();
            txtEstado.Text = p_paciente.Estado;
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
            dtpNacimiento.Enabled = habilitar;
            txtObservaciones.ReadOnly = !habilitar;
            txtHabitacion.ReadOnly = !habilitar;
            txtEstado.ReadOnly = !habilitar;

            btnEditar.Text = habilitar ? "Guardar" : "Editar";
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                // Paso de solo lectura → edición
                ToggleEdicion(true);
            }
            else
            {
                // Paso de edición → guardar
                if (ValidarDatos())
                {
                    _paciente.nombre = txtNombre.Text;
                    _paciente.apellido = txtApellido.Text;
                    _paciente.dni = int.Parse(txtDni.Text);
                    _paciente.obraSocial = txtObraSocial.Text;
                    _paciente.nroAfiliado = int.Parse(txtAfiliado.Text);
                    _paciente.direccion = txtDireccion.Text;
                    _paciente.telefono = int.Parse(txtTelefono.Text);
                    _paciente.FechaNacimiento = dtpNacimiento.Value;
                    _paciente.observaciones = txtObservaciones.Text;
                    _paciente.habitacion = int.Parse(txtHabitacion.Text);
                    _paciente.Estado = txtEstado.Text;

                    // Notificamos al contenedor
                    PacienteActualizado?.Invoke(this, _paciente);

                    // Vuelve a modo solo lectura
                    ToggleEdicion(false);

                    // Mensaje de éxito
                    MessageBox.Show("Paciente editado con éxito.",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor, corrija los errores en el formulario.",
                                    "Error de validación",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                !int.TryParse(txtDni.Text, out _) ||
                !int.TryParse(txtAfiliado.Text, out _) ||
                !int.TryParse(txtTelefono.Text, out _) ||
                !int.TryParse(txtHabitacion.Text, out _))
            {
                return false;
            }
            // Validación de fecha de nacimiento
            if (dtpNacimiento.Value > DateTime.Now)
            {
                return false;
            }
            return true;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Revertir cambios y salir del modo edición
            CargarDatos(_paciente);
            ToggleEdicion(false);

            CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }
}
