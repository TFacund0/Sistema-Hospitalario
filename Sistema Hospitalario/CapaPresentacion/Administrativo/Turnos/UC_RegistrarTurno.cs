using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos
{
    public partial class UC_RegistrarTurno : UserControl
    {
        public UC_RegistrarTurno()
        {
            InitializeComponent();
        }


        // ========================= VALIDACIONES (Validating) =========================

        private void txtPaciente_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPaciente.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPaciente, "El paciente es obligatorio.");
            }
            else if (txtPaciente.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPaciente, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtPaciente, "");
            }
        }

        private void txtMedico_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedico.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMedico, "El médico es obligatorio.");
            }
            else if (txtMedico.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMedico, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtMedico, "");
            }
        }

        private void txtProcedimiento_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProcedimiento.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProcedimiento, "El procedimiento es obligatorio.");
            }
            else if (txtProcedimiento.Text.Length > 80)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProcedimiento, "Máximo 80 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtProcedimiento, "");
            }
        }

        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCorreo, "El correo es obligatorio.");
            }
            else if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCorreo, "Formato de correo no válido.");
            }
            else if (correo.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCorreo, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtCorreo, "");
            }
        }

        private void txtDni_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text) || !long.TryParse(txtDni.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI es obligatorio y numérico.");
            }
            else if (txtDni.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "Máximo 15 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtDni, "");
            }
        }

        private void txtTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !long.TryParse(txtTelefono.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "El teléfono es obligatorio y numérico.");
            }
            else if (txtTelefono.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "Máximo 20 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtTelefono, "");
            }
        }

        private void dtpFechaTurno_Validating(object sender, CancelEventArgs e)
        {
            // Configuración visual/ límites
            dtpFechaTurno.Format = DateTimePickerFormat.Custom;
            dtpFechaTurno.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaTurno.MinDate = DateTime.Today; // no turnos en pasado
            dtpFechaTurno.MaxDate = DateTime.Today.AddYears(2);

            // Validación
            if (dtpFechaTurno.Value < DateTime.Now.AddMinutes(-1))
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaTurno, "La fecha/hora de turno no puede ser en el pasado.");
            }
            else
            {
                errorProvider1.SetError(dtpFechaTurno, "");
            }
        }

        private void dtpFechaRegistro_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaRegistro.Format = DateTimePickerFormat.Short;
            dtpFechaRegistro.MaxDate = DateTime.Today;                 // registro no futuro
            dtpFechaRegistro.MinDate = DateTime.Today.AddYears(-5);

            if (dtpFechaRegistro.Value.Date > DateTime.Today)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaRegistro, "La fecha de registro no puede ser futura.");
            }
            else if (dtpFechaRegistro.Value.Date > dtpFechaTurno.Value.Date)
            {
                // opcional: el registro no debería ser posterior al turno
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaRegistro, "La fecha de registro no puede ser posterior al turno.");
            }
            else
            {
                errorProvider1.SetError(dtpFechaRegistro, "");
            }
        }

        private void txtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            if (txtObservaciones.Text.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Máximo 300 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObservaciones, "");
            }
        }

        // ========================= RESTRICCIONES DE TECLAS (KeyPress) =========================

        private void txtPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txtMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // ================================ BOTONES ================================

        // Notifica al contenedor (MenuAdministrativo) que se pidió cancelar
        public event EventHandler CancelarTurnoSolicitado;

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            CancelarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // Ejecuta todas las validaciones de Validating
            if (this.ValidateChildren())
            {
                MessageBox.Show("Turno registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Aquí armarías el objeto Turno y lo guardarías (cuando conectes BD)
            }
            else
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            txtPaciente.Clear();
            txtMedico.Clear();
            txtProcedimiento.Clear();
            txtCorreo.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtObservaciones.Clear();

            dtpFechaTurno.Value = DateTime.Now.AddHours(1);
            dtpFechaRegistro.Value = DateTime.Today;

            errorProvider1.Clear();
        }
    }
}
