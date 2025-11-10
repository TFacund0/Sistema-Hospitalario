using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaPresentacion.Administrativo;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización;
using Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;

namespace Sistema_Hospitalario.CapaPresentacion.Gerente
{
    public partial class MenuGerente : Form
    {
        // ======================= CONSTRUCTOR DEL MENÚ GERENTE =======================
        public MenuGerente()
        {
            InitializeComponent();
            this.Text = "Menú Gerente"; // título del formulario
        }

        // ======================= NAVEGACIÓN CENTRAL =======================

        // Método común para mostrar un UserControl en el panel contenedor.
        private void AbrirUserControl(UserControl uc)
        {
            try
            {
                foreach (Control c in panelContenedor.Controls) c.Dispose();
                panelContenedor.Controls.Clear();

                uc.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ======================= HOME =======================
        private void btnHome_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_HomeGerente());
        }

        // ======================= PACIENTES =======================
        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_PacientesGerente());
        }

        // ======================= TURNOS =======================
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_TurnosGerente());
        }

        // ======================= SALIR =======================
        private void btnSalir_Click(object sender, EventArgs e)
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
