using Sistema_Hospitalario.CapaPresentacion.Administrador.medicos;
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


        // ============================= Validaciones ===========================
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
            if (string.IsNullOrWhiteSpace(TBUSERNAME.Text) || !int.TryParse(TBUSERNAME.Text, out _))
            { // Validar que no esté vacío y sea numérico
                e.Cancel = true;
                errorProvider1.SetError(TBUSERNAME, "El DNI es obligatorio y numérico.");
            }
            else if (TBUSERNAME.Text.Length > 15)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBUSERNAME, "Máximo 15 caracteres.");
            }
            else   // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBUSERNAME, "");
            }
        }

        private void TBTELEFONO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPASSWORD.Text) || !long.TryParse(TBPASSWORD.Text, out _))
            { // Validar que no esté vacío y sea numérico
                e.Cancel = true;
                errorProvider1.SetError(TBPASSWORD, "El teléfono es obligatorio y numérico.");
            }
            else if (TBPASSWORD.Text.Length > 15)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBPASSWORD, "Máximo 15 caracteres.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBPASSWORD, "");
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
        private void TBUSERNAME_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBUSERNAME.Text))
            { // Validar que no esté vacío o solo espacios
                e.Cancel = true;
                errorProvider1.SetError(TBUSERNAME, "El nombre de usuario es obligatorio.");
            }
            else if (TBUSERNAME.Text.Length > 50)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBUSERNAME, "Máximo 50 caracteres.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(
                         TBUSERNAME.Text, @"^[a-zA-Z0-9_]+$"))
            { // Validar caracteres permitidos (opcional)
                e.Cancel = true;
                errorProvider1.SetError(TBUSERNAME, "Solo se permiten letras, números y guión bajo.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBUSERNAME, "");
            }
        }

        private void TBPASSWORD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPASSWORD.Text))
            { // Validar que no esté vacío
                e.Cancel = true;
                errorProvider1.SetError(TBPASSWORD, "La contraseña es obligatoria.");
            }
            else if (TBPASSWORD.Text.Length > 60)
            { // Validar longitud máxima
                e.Cancel = true;
                errorProvider1.SetError(TBPASSWORD, "Máximo 60 caracteres.");
            }
            else if (TBPASSWORD.Text.Length < 6)
            { // Validar longitud mínima básica (opcional)
                e.Cancel = true;
                errorProvider1.SetError(TBPASSWORD, "Debe tener al menos 6 caracteres.");
            }
            else // Si todo está bien, limpiar el error
            {
                errorProvider1.SetError(TBPASSWORD, "");
            }
        }

        // ============================= BOTONES =============================

        private void btnGuardar_Click_1(object sender, EventArgs e)
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


        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            TBNOMBRE.Clear();
            TBAPELLIDO.Clear();
            TBUSERNAME.Clear();
            TBPASSWORD.Clear();
            TBCORREO.Clear();
            errorProvider1.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_usuarios());
        }
    }
}
