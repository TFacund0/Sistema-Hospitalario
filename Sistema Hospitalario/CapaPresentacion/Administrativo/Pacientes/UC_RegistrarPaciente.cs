using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;
using Sistema_Hospitalario.CapaNegocio.Servicios;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_RegistrarPaciente : UserControl
    {
        // Servicio para interactuar con la capa de negocio
        private readonly PacienteService _pacienteService = new PacienteService();

        // Evento para notificar al menuAdministrativo que se solicitó cancelar el registro
        public event EventHandler CancelarRegistroSolicitado;

        // ============================= CONSTRUCTOR DEL UC REGISTRAR PACIENTE =============================
        public UC_RegistrarPaciente()
        {
            InitializeComponent();
        }

        // ============================= VALIDACIONES DE CAMPOS PACIENTE =============================
        
        // ========== VALIDACIÓN NOMBRE ==========
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "El nombre es obligatorio.");
            }
            else if (txtNombre.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtNombre, "");
            }
        }

        // ========== VALIDACIÓN DIRECCIÓN ==========
        private void txtDireccion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDireccion, "La dirección es obligatoria.");
            }
            else if (txtDireccion.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDireccion, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtDireccion, "");
            }
        }

        // ========== VALIDACIÓN APELLIDO ==========
        private void txtApellido_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "El apellido es obligatorio.");
            }
            else if (txtApellido.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtApellido, "");
            }
        }

        // ========== VALIDACIÓN ESTADO INICIAL ==========
        private void txtInicial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInicial.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "El valor inicial es obligatorio.");
            }
            else if (txtInicial.Text.Length > 10)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "Máximo 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtInicial, "");
            }
        }


        // ========== VALIDACIÓN FECHA DE NACIMIENTO ==========
        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpNacimiento.Format = DateTimePickerFormat.Short; 
            dtpNacimiento.MaxDate = DateTime.Today;            
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-120); 

            if (dtpNacimiento.Value > DateTime.Today)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser futura.");
            }
            else if (dtpNacimiento.Value < DateTime.Today.AddYears(-120))
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser mayor a 120 años.");
            }
            else
            {
                errorProvider1.SetError(dtpNacimiento, "");
            }
        }

        // ========== VALIDACIÓN DNI ==========
        private void txtDni_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text) || !int.TryParse(txtDni.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI es obligatorio y númerico.");
            }
            else if (txtDni.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "Máximo 15 caracteres.");
            }
            else if (int.TryParse(txtDni.Text, out int dni) && dni <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI debe ser un número positivo.");
            }
            else
            {
                errorProvider1.SetError(txtDni, "");
            }
        }

        // ========== VALIDACIÓN TELÉFONO ==========
        private void txtTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !int.TryParse(txtTelefono.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "El teléfono es obligatorio y númerico.");
            }
            else if (txtTelefono.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "Máximo 15 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtTelefono, "");
            }
        }

        // ========== VALIDACIÓN OBSERVACIONES ==========
        private void txtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            if (txtObservaciones.Text.Length > 200)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Máximo 200 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObservaciones, "");
            }
        }

        //============================= RESTRICCIONES DE TECLADO =============================

        // Permitir solo letras, espacios y teclas de control
        private void SoloLetras(object s, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        // Permitir solo números y teclas de control
        private void SoloNumeros(object s, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        // ============================ BOTÓN GUARDAR =============================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var dto = new CapaNegocio.DTOs.PacienteAltaDto
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Dni = int.TryParse(txtDni.Text.Trim(), out int dni) ? dni : 0,
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                FechaNacimiento = dtpNacimiento.Value,
                Observaciones = txtObservaciones.Text.Trim(),
                EstadoInicial = txtInicial.Text.Trim()
            };

            var r = _pacienteService.Alta(dto);

            if (r.Ok)
            {
                MessageBox.Show($"Paciente registrado con éxito. ID: {r.IdGenerado}", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpiar_Click(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show(r.Error, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================ BOTÓN LIMPIAR =============================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtDni.Clear();
            txtDireccion.Clear();            
            txtDni.Clear();
            txtInicial.Clear();
            txtObservaciones.Clear();
            dtpNacimiento.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        // ============================ BOTÓN CANCELAR =============================
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
