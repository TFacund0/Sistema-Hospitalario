using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    public partial class MenuMedicos : Form
    {
        public MenuMedicos()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.Size = new Size(1220, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void AbrirUserControl(UserControl uc)
        {
            panelContenedor.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelContenedor.Controls.Add(uc);   // Lo agrega al panel
            uc.BringToFront();                  // Lo trae al frente
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_HomeM());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_PacientesM());
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_TurnosM());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_ProcedimientosM());
        }
    }
}
