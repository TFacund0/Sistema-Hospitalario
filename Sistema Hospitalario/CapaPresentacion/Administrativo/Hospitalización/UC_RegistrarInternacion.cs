using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización
{
    public partial class UC_RegistrarInternacion : UserControl
    {
        public UC_RegistrarInternacion()
        {
            InitializeComponent();
        }

        // ============================= VALIDACIONES DE CAMPOS =============================

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

        private void txtApellido_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEnfermero.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEnfermero, "El apellido es obligatorio.");
            }
            else if (txtEnfermero.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEnfermero, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtEnfermero, "");
            }
        }


        private void txtEnfemero_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "La dirección es obligatoria.");
            }
            else if (txtApellido.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtApellido, "");
            }
        }
        private void txtObraSocial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedico.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMedico, "La obra social es obligatoria.");
            }
            else if (txtMedico.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMedico, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtMedico, "");
            }
        }

        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaInicio.Format = DateTimePickerFormat.Short; // muestra 01/01/2025
            dtpFechaInicio.MaxDate = DateTime.Today;            // no permite fechas futuras
            dtpFechaInicio.MinDate = DateTime.Today.AddYears(-120); // no permite más de 120 años atrás

            //Validaciones
            if (dtpFechaInicio.Value > DateTime.Today)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha de nacimiento no puede ser futura.");
            }
            else if (dtpFechaInicio.Value < DateTime.Today.AddYears(-120))
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha de nacimiento no puede ser mayor a 120 años.");
            }
            else
            {
                errorProvider1.SetError(dtpFechaInicio, "");
            }
        }

        private void txtAfiliado_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPiso.Text) || !int.TryParse(txtPiso.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "El número de afiliado es obligatorio.");
            }
            else if (txtPiso.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Máximo 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtPiso, "");
            }
        }

        private void txtDni_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHabitacion.Text) || !int.TryParse(txtHabitacion.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "El DNI es obligatorio y númerico.");
            }
            else if (txtHabitacion.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "Máximo 15 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtHabitacion, "");
            }
        }

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

        private void txtInicial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEstado, "El valor inicial es obligatorio.");
            }
            else if (txtEstado.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEstado, "Máximo 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtEstado, "");
            }
        }

        private void txtHabitacion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHabitacion.Text) && int.TryParse(txtHabitacion.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "El número de habitación es obligatorio y númerico.");
            }
            else if (txtHabitacion.Text.Length > 10)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "Máximo 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtHabitacion, "");
            }
        }

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

        // ============================= VALIDACIONES DE TECLAS =============================

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si no es letra, tecla de control (backspace, etc.) o espacio → cancelar
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // ignora la tecla
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si no es letra, tecla de control (backspace, etc.) o espacio → cancelar
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // ignora la tecla
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (Backspace, Delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtAfiliado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (Backspace, Delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (Backspace, Delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtHabitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (Backspace, Delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        //============================= BOTONES =============================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Guarda toda la información del paciente
            if (this.ValidateChildren())
            {
                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia todos los campos del formulario
            txtNombre.Clear();
            txtEnfermero.Clear();
            txtTelefono.Clear();
            txtHabitacion.Clear();
            txtApellido.Clear();
            txtMedico.Clear();
            txtPiso.Clear();
            txtHabitacion.Clear();
            txtEstado.Clear();
            txtObservaciones.Clear();
            dtpFechaInicio.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        // Evento para notificar al formulario padre que se solicitó cancelar el registro
        public event EventHandler CancelarInternacionSolicitada;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }
}
