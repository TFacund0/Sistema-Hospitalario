using Sistema_Hospitalario.CapaDatos.ModerRepos;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;
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
        private static readonly UsuarioService usuarioService = new UsuarioService(new UsuarioRepository());
        private readonly UsuarioService _service = usuarioService;
        public UC_registrarUsuario()
        {
            InitializeComponent();
            CargarRoles();
            CargarEstados();
        }

        private void CargarRoles()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var roles = db.rol
                              .Select(r => new { IdRol = r.id_rol, NombreRol = r.nombre })
                              .ToList();
                cboRol.DisplayMember = "NombreRol";
                cboRol.ValueMember = "IdRol";
                cboRol.DataSource = roles;
            }
        }

        private void CargarEstados()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var estados = db.estado_usuario
                                .Select(e => new { IdEstado = e.id_estado_usuario, NombreEstado = e.nombre })
                                .ToList();
                cboEstado.DisplayMember = "NombreEstado";
                cboEstado.ValueMember = "IdEstado";
                cboEstado.DataSource = estados;
            }
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
            else if (TBPASSWORD.Text.Length > 65)
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

        private void BtnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error de Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener IDs de los ComboBox seleccionados
                int idRol = (int)cboRol.SelectedValue; // Asume ComboBox cboRol
                int idEstado = (int)cboEstado.SelectedValue; // Asume ComboBox cboEstado

                // Crear el DTO de alta
                UsuarioAltaDTO nuevoUsuario = new UsuarioAltaDTO
                {
                    Nombre = TBNOMBRE.Text.Trim(),
                    Apellido = TBAPELLIDO.Text.Trim(),
                    NombreUsuario = TBUSERNAME.Text.Trim(),
                    Password = TBPASSWORD.Text.Trim(),
                    Correo = TBCORREO.Text.Trim(),
                    IdRol = idRol,
                    IdEstado = idEstado
                };

                // Llamar al servicio para agregar el usuario
                var (Ok, IdGenerado, Error) = _service.AgregarUsuario(nuevoUsuario);

                if (Ok)
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLimpiar_Click_1(sender, e); // Limpiar el formulario
                }
                else
                {
                    // Mostrar el error específico devuelto por el repositorio/servicio
                    MessageBox.Show("Error al registrar el usuario: " + Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al registrar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnLimpiar_Click_1(object sender, EventArgs e)
        {
            TBNOMBRE.Clear();
            TBAPELLIDO.Clear();
            TBUSERNAME.Clear();
            TBPASSWORD.Clear();
            TBCORREO.Clear();
            errorProvider1.Clear();
            cboRol.SelectedIndex = 0; // Seleccionar el primer rol por defecto
            cboEstado.SelectedIndex = 0; // Seleccionar el primer estado por defecto
            errorProvider1.Clear();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_usuarios());
        }
    }
}
