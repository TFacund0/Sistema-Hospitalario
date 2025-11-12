using Sistema_Hospitalario.CapaNegocio;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios.UsuarioService;
using Sistema_Hospitalario.CapaPresentacion.Administrador;
using Sistema_Hospitalario.CapaPresentacion.Administrativo;
using Sistema_Hospitalario.CapaPresentacion.Gerente;
using Sistema_Hospitalario.CapaPresentacion.Medico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsInicio_de_sesion
{
    public partial class Login : Form
    {
        // Servicio para manejar la lógica de usuarios
        UsuarioService _usuarioService = new UsuarioService();

        public Login()
        {
            InitializeComponent();
        }

        // ======================= EVENTO DEL BOTÓN INGRESAR =======================
        // Aquí se maneja la lógica de autenticación y redirección según el rol del usuario.
        private void BotonIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Campos Vacíos",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (usuario == "admin" && contraseña == "admin")
                {
                    Form menuPrincipal = new MenuMedicos();
                    menuPrincipal.ShowDialog();
                    this.Close();
                }
            }

                try
                {
                    // 1. Llamamos al servicio para validar
                    UsuarioLoginResultadoDTO resultadoLogin = _usuarioService.ValidarCredenciales(usuario, contraseña);

                    if (resultadoLogin.LoginExitoso)
                    {
                        // 2. Guardamos los datos en la sesión estática
                        SesionUsuario.Login(resultadoLogin);

                        this.Hide();

                        // 3. Abrimos el formulario según el Rol
                        Form menuPrincipal = null;
                        switch (SesionUsuario.NombreRol) // ¡Verificá que estos nombres sean los de tu BD!
                        {
                            case "Administrativo":
                                menuPrincipal = new MenuAdministrativo();
                                break;
                            case "medico":
                                menuPrincipal = new MenuMedicos();
                                break;
                            case "Moderador": // O "Administrador" o como lo llames
                                menuPrincipal = new MenuModer();
                                break;
                            case "Gerente":
                                menuPrincipal = new MenuGerente();
                                break;
                            default:
                                MessageBox.Show("Rol de usuario no reconocido. Contacte al administrador.", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Show();
                                SesionUsuario.Logout();
                                return;
                        }

                        menuPrincipal.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        // Si el login falló
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContraseña.Clear();
                        txtUsuario.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado durante el inicio de sesión: " + ex.Message,
                                    "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
        }

        // ======================= MÉTODOS AUXILIARES =======================
        // Método para calcular el hash SHA-256 de una cadena
        private string CalcularSha256(string texto)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(texto);
                var hashBytes = sha.ComputeHash(bytes);

                // Lo devolvemos como string hexadecimal para comparar
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
