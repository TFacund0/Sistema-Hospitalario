using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.usuarios
{
    public partial class UC_registrarUsuario : UserControl
    {
        public UC_registrarUsuario()
        {
            InitializeComponent();
        }
        private void TBNOMBRE_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBNOMBRE.Text))
            {  // Validar que no esté vacío o solo espacios
                e.Cancel = true;
                errorProvider1.SetError(TBNOMBRE, "El nombre es obligatorio.");
            }
            else if (TBNOMBRE.Text.Length > 50)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBNOMBRE, "Máximo 50 caracteres.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBNOMBRE, "");
            }
        }

        private void TBAPELLIDO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBAPELLIDO.Text))
            { // Validar que no esté vacío o solo espacios
                e.Cancel = true;
                errorProvider1.SetError(TBAPELLIDO, "El apellido es obligatorio.");
            }
            else if (TBAPELLIDO.Text.Length > 50)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBAPELLIDO, "Máximo 50 caracteres.");
            }
            else    // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBAPELLIDO, "");
            }
        }

        private void TBDNI_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDNI.Text) || !int.TryParse(TBDNI.Text, out _))
            { // Validar que no esté vacío y sea numérico
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "El DNI es obligatorio y numérico.");
            }
            else if (TBDNI.Text.Length > 15)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "Máximo 15 caracteres.");
            }
            else   // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBDNI, "");
            }
        }

        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            // Limita la fecha a hoy y 120 años atrás
            dtpNacimiento.MaxDate = DateTime.Today;
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-120);

            if (dtpNacimiento.Value.Date > DateTime.Today)
            { // Validar que no sea futura
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser futura.");
            }
            else if (dtpNacimiento.Value.Date < DateTime.Today.AddYears(-120))
            { // Validar que no sea mayor a 120 años atrás
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha no puede ser mayor a 120 años atrás.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(dtpNacimiento, "");
            }
        }

        private void TBTELEFONO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBTELEFONO.Text) || !long.TryParse(TBTELEFONO.Text, out _))
            { // Validar que no esté vacío y sea numérico
                e.Cancel = true;
                errorProvider1.SetError(TBTELEFONO, "El teléfono es obligatorio y numérico.");
            }
            else if (TBTELEFONO.Text.Length > 15)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBTELEFONO, "Máximo 15 caracteres.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBTELEFONO, "");
            }
        }

        private void TBCORREO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCORREO.Text))
            { // Validar que no esté vacío
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "El correo electrónico es obligatorio.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(
                         TBCORREO.Text,
                         @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { // Validar formato básico de correo
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "Formato de correo inválido.");
            }
            else if (TBCORREO.Text.Length > 100)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "Máximo 100 caracteres.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBCORREO, "");
            }
        }

        // ============================= BOTONES =============================

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            { // Si todas las validaciones pasan
                MessageBox.Show("Usuario registrado con éxito.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  // Si alguna validación falla
            {
                MessageBox.Show("Corrija los errores antes de guardar.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        { // Limpiar todos los campos y errores
            TBNOMBRE.Clear();
            TBAPELLIDO.Clear();
            TBDNI.Clear();
            TBTELEFONO.Clear();
            TBCORREO.Clear();
            dtpNacimiento.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        

    }
}
