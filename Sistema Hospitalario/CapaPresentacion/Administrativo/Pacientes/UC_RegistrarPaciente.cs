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

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_RegistrarPaciente : UserControl
    {
        public UC_RegistrarPaciente()
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

        private void txtObraSocial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtObraSocial.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObraSocial, "La obra social es obligatoria.");
            }
            else if (txtObraSocial.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObraSocial, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObraSocial, "");
            }
        }

        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpNacimiento.Format = DateTimePickerFormat.Short; // muestra 01/01/2025
            dtpNacimiento.MaxDate = DateTime.Today;            // no permite fechas futuras
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-120); // no permite más de 120 años atrás

            //Validaciones
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

        private void txtAfiliado_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAfiliado.Text) || !int.TryParse(txtAfiliado.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAfiliado, "El número de afiliado es obligatorio.");
            }
            else if (txtAfiliado.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAfiliado, "Máximo 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtAfiliado, "");
            }
        }

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
            else
            {
                errorProvider1.SetError(txtDni, "");
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
            if (string.IsNullOrWhiteSpace(txtInicial.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "El valor inicial es obligatorio.");
            }
            else if (txtInicial.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "Máximo 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtInicial, "");
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
            txtApellido.Clear();
            txtTelefono.Clear();
            txtDni.Clear();
            txtDireccion.Clear();
            txtObraSocial.Clear();
            txtAfiliado.Clear();
            txtHabitacion.Clear();
            txtInicial.Clear();
            txtObservaciones.Clear();
            dtpNacimiento.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        // Evento para notificar al formulario padre que se solicitó cancelar el registro
        public event EventHandler CancelarRegistroSolicitado;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
