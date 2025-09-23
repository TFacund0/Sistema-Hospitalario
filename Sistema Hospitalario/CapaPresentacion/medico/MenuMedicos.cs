using Sistema_Hospitalario.CapaPresentacion.Medico;
using Sistema_Hospitalario.CapaPresentacion.Medico.home;
using Sistema_Hospitalario.CapaPresentacion.Medico.Pacientes;
using Sistema_Hospitalario.CapaPresentacion.Medico.procedimientos;
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

        public void AbrirUserControl(UserControl uc)
        {
            panelContenedor.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelContenedor.Controls.Add(uc);   // Lo agrega al panel
            uc.BringToFront();                  // Lo trae al frente
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            // sender es el botón que se clickeó
            Button btnClic = (Button)sender;

            // Recorre todos los botones dentro del mismo contenedor
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button b)
                    b.BackColor =  Color.FromArgb(49, 163, 166);
            }

            // Marca el botón presionado
            btnClic.BackColor = Color.LightSeaGreen;
        }
        private void btn_home_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Home_M());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_pacientes_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
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
            Boton_Click(sender, e);
            AbrirUserControl(new UC_TurnosM());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new ConsultaMedica());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new UC_Procedimiento());
        }
    }
}
