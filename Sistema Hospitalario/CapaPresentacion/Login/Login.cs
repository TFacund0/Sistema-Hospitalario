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
            string contraseña = txtContraseña.Text.Trim();

            try
            {
                // ================== 1) Usuarios reales en BD ==================
                UsuarioLoginResultadoDTO resultadoLogin = _usuarioService.ValidarCredenciales(usuario, contraseña);

                // Si el servicio devuelve null o LoginExitoso == false, credenciales inválidas
                if (resultadoLogin == null || !resultadoLogin.LoginExitoso)
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Si llegamos acá, el login fue exitoso
                SesionUsuario.Login(resultadoLogin);

                this.Hide();

                switch (resultadoLogin.NombreRol.ToLower())
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al inicializar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
