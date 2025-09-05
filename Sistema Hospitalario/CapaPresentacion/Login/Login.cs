using Sistema_Hospitalario.CapaPresentacion.Administrativo;
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
        public Login()
        {
            InitializeComponent();
        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BotonIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // Validación básica
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
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panelFormLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
