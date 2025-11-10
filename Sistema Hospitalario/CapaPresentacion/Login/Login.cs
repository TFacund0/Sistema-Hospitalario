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

namespace WindowsFormsInicio_de_sesion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BotonIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            try
            {
                // Validación básica de credenciales (para demostración)
                if (usuario == "admin" && contraseña == "1234")
                {
                    this.Hide();

                    MenuAdministrativo ventanaPrincipal = new MenuAdministrativo();
                    ventanaPrincipal.ShowDialog();

                    this.Close();

                }
                else if (usuario == "medico" && contraseña == "1234")
                {
                    this.Hide();

                    MenuMedicos ventanaPrincipal = new MenuMedicos();
                    ventanaPrincipal.ShowDialog();

                    this.Close();
                }
                else if (usuario == "mod" && contraseña == "1234")
                {
                    this.Hide();
                    MenuModer ventanaPrincipal = new MenuModer();
                    ventanaPrincipal.ShowDialog();
                    this.Close();
                }
                else if (usuario == "gerente" && contraseña == "1234")
                {
                    this.Hide();
                    MenuGerente ventanaPrincipal = new MenuGerente();
                    ventanaPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Mensaje de error si las credenciales son incorrectas
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
