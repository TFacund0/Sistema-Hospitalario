using Sistema_Hospitalario.CapaPresentacion.Administrativo;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;

namespace Sistema_Hospitalario.CapaPresentacion.Gerente
{
    public partial class MenuGerente : Form
    {
        public MenuGerente()
        {
            InitializeComponent();
            this.Text = "Menú Gerente"; // título del formulario
        }

        // ======================= NAVEGACIÓN CENTRAL =======================
        // Método común para mostrar un UserControl en el panel contenedor.
        private void AbrirUserControl(UserControl uc)
        {
            foreach (Control c in panelContenedor.Controls) c.Dispose();
            panelContenedor.Controls.Clear();

            uc.Dock = DockStyle.Fill;   
            panelContenedor.Controls.Add(uc);
            uc.BringToFront();
        }

        // ======================= HOME =======================
        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_HomeGerente());
        }

        // ======================= PACIENTES =======================
        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_PacientesGerente());
        }

        // ======================= TURNOS =======================
        private void btn_turnos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_TurnosGerente());
        }

        // ======================= SALIR =======================
        private void btn_salir_Click(object sender, EventArgs e)
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
}
