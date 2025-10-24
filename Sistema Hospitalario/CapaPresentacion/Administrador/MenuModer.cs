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

        public void AbrirUserControl(UserControl uc)
        {
            panelContenedor.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelContenedor.Controls.Add(uc);   // Lo agrega al panel
            uc.BringToFront();                  // Lo trae al frente
        }

        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_usuarios());
        }

        private void btn_home_Click(object sender, EventArgs e)
        {

        }

        private void btnHospitalizacion_Click(object sender, EventArgs e)
        {

        }

        private void btnProcedimientos_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void btn_medico_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Medicos());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Especialidades());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);

            AbrirUserControl(new UC_Camas());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Habitaciones());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Procedimientos());
        }
    }
}
