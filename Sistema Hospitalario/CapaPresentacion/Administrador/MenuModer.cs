using Sistema_Hospitalario.CapaPresentacion.Administrador.Backups;
using Sistema_Hospitalario.CapaPresentacion.Administrador.medicos;
using Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo;
using Sistema_Hospitalario.CapaPresentacion.Administrador.usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador
{
    public partial class MenuModer : Form
    {
        public MenuModer()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            // sender es el botón que se clickeó
            Button btnClic = (Button)sender;

            // Recorre todos los botones dentro del mismo contenedor
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button b)
                    b.BackColor = Color.FromArgb(49, 163, 166);
            }

            // Marca el botón presionado
            btnClic.BackColor = Color.LightSeaGreen;
        }

        // Método para abrir UserControls dentro del panel contenedor
        public void AbrirUserControl(UserControl uc)
        {
            try
            {
                // Limpia el contenedor correctamente
                foreach (Control c in panelContenedor.Controls)
                    c.Dispose();

                panelContenedor.Controls.Clear();

                // Configura el UserControl
                uc.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la sección: {ex.Message}",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ===================== EVENTOS DE BOTONES DEL MENÚ =====================
        // Pacientes
        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_usuarios());
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            {
                DialogResult dr = MessageBox.Show("¿Seguro que desea salir?",
                                          "Confirmación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        // Médicos
        private void btn_medico_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Medicos());
        }

        // Especialidades
        private void button1_Click_1(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Especialidades());
        }

        // Camas
        private void button3_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);

            AbrirUserControl(new UC_Camas());
        }

        // Habitaciones
        private void button2_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Habitaciones());
        }

        // Procedimientos
        private void button4_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Procedimientos());
        }

        // Backups
        private void btn_backup_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Backups());
        }

    }
}
