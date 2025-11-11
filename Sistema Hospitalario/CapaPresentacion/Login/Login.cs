using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaPresentacion.Administrativo;
using Sistema_Hospitalario.CapaPresentacion.Medico;
using Sistema_Hospitalario.CapaPresentacion.Gerente;
using Sistema_Hospitalario.CapaPresentacion.Administrador;
using Sistema_Hospitalario.CapaNegocio.Servicios.UsuarioService;

namespace WindowsFormsInicio_de_sesion
{
    public partial class Login : Form
    {
        UsuarioService _usuarioService = new UsuarioService();

        public Login()
        {
            InitializeComponent();
        }

        private void BotonIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            try
            {
                // Obtenemos todos los usuarios desde el servicio
                var usuarios = _usuarioService.ObtenerUsuarios();

                // Buscamos un usuario que coincida con nombre de usuario y contraseña
                var usuarioValido = usuarios
                    .FirstOrDefault(u =>
                        u.NombreUsuario.Equals(usuario, StringComparison.OrdinalIgnoreCase)
                        && u.Password == CalcularSha256(contraseña));


                // Usuarios de prueba (hardcodeados)
                if (usuario == "admin" && contraseña == "1234")
                {
                    this.Hide();
                    new MenuAdministrativo().ShowDialog();
                    this.Close();
                }
                else if (usuario == "medico" && contraseña == "1234")
                {
                    this.Hide();
                    new MenuMedicos().ShowDialog();
                    this.Close();
                }
                else if (usuario == "mod" && contraseña == "1234")
                {
                    this.Hide();
                    new MenuModer().ShowDialog();
                    this.Close();
                }
                else if (usuario == "gerente" && contraseña == "1234")
                {
                    this.Hide();
                    new MenuGerente().ShowDialog();
                    this.Close();
                }

                // Usuarios reales desde la base de datos
                else if (usuarioValido != null)
                {
                    this.Hide();

                    switch (usuarioValido.Rol.ToLower())
                    {
                        case "administrativo":
                            new MenuAdministrativo().ShowDialog();
                            break;

                        case "medico":
                            new MenuMedicos().ShowDialog();
                            break;

                        case "administrador":
                        case "moderador":
                            new MenuModer().ShowDialog();
                            break;

                        case "gerente":
                            new MenuGerente().ShowDialog();
                            break;

                        default:
                            MessageBox.Show("El rol del usuario no tiene una pantalla asignada.",
                                            "Rol no configurado",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            break;
                    }

                    this.Close();
                }

                // Si no coincide ningún caso
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
