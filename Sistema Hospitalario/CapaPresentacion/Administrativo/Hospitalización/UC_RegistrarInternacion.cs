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

        // ========== PACIENTE ==========
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "El nombre del paciente es obligatorio.");
            }
            else if (txtNombre.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtNombre, "");
            }
        }

        // (Opcional) Apellido del paciente
        private void txtApellido_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "El apellido del paciente es obligatorio.");
            }
            else if (txtApellido.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtApellido, "");
            }
        }

        // ========== MÉDICO ==========
        private void txtMedico_Validating(object sender, CancelEventArgs e)
        {   
            if (txtMedico.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMedico, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtMedico, "");
            }
        }

        // ========== ENFERMERO ==========
        private void txtEnfermero_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEnfermero.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEnfermero, "El enfermero/a asignado es obligatorio.");
            }
            else if (txtEnfermero.Text.Length > 60)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEnfermero, "Máximo 60 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtEnfermero, "");
            }
        }

        // ========== FECHAS ==========
        private void dtpFechaInicio_Validating(object sender, CancelEventArgs e)
        {
            // La internación no puede iniciar en el futuro
            if (dtpFechaInicio.Value > DateTime.Now)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha/hora de inicio no puede ser futura.");
            }
            else
            {
                errorProvider1.SetError(dtpFechaInicio, "");
            }
        }

        // Si tenés dtpFechaFin en el formulario (opcional)
        // Requiere que dtpFechaFin sea igual o posterior a inicio y no futura
        private void dtpFechaFin_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFin.Checked) // si usás ShowCheckBox=true
            {
                if (dtpFechaFin.Value < dtpFechaInicio.Value)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dtpFechaFin, "La fecha de egreso debe ser posterior al inicio.");
                    return;
                }
                if (dtpFechaFin.Value > DateTime.Now)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dtpFechaFin, "La fecha/hora de egreso no puede ser futura.");
                    return;
                }
            }
            errorProvider1.SetError(dtpFechaFin, "");
        }

        // ========== UBICACIÓN ==========
        private void txtPiso_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPiso.Text) || !int.TryParse(txtPiso.Text, out int piso) || piso <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Piso obligatorio y numérico (>0).");
            }
            else if (txtPiso.Text.Length > 3)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Máximo 3 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtPiso, "");
            }
        }

        private void txtHabitacion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHabitacion.Text) || !int.TryParse(txtHabitacion.Text, out int hab) || hab <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "Habitación obligatoria y numérica (>0).");
            }
            else if (txtHabitacion.Text.Length > 5)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtHabitacion, "Máximo 5 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtHabitacion, "");
            }
        }

        // ========== ESTADO ==========
        private static readonly string[] ESTADOS_VALIDOS =
            { "Ocupada", "Disponible", "Reservada", "Limpieza", "Mantenimiento" };

        private void txtEstado_Validating(object sender, CancelEventArgs e)
        {
            string val = (txtEstado.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(val))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEstado, "El estado es obligatorio.");
            }
            else if (!ESTADOS_VALIDOS.Any(s => s.Equals(val, StringComparison.OrdinalIgnoreCase)))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEstado, $"Estado inválido. Valores: {string.Join(", ", ESTADOS_VALIDOS)}.");
            }
            else
            {
                errorProvider1.SetError(txtEstado, "");
            }
        }


        // ========== OBSERVACIONES ==========
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

        // ============================= VALIDACIONES DE TECLAS =============================
        // Solo letras, espacio y teclas de control (para nombres)
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        // Solo dígitos (para piso/habitación/teléfono)
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
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
            txtHabitacion.Clear();
            txtApellido.Clear();
            txtMedico.Clear();
            txtPiso.Clear();
            txtHabitacion.Clear();
            txtEstado.Clear();
            txtObservaciones.Clear();
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today;
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
