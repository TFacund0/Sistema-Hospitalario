using Sistema_Hospitalario.CapaPresentacion.Medico;
using Sistema_Hospitalario.CapaPresentacion.Medico.Pacientes;
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
        }

        public void AbrirUserControl(UserControl uc)
        {
            panelMenu.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelMenu.Controls.Add(uc);   // Lo agrega al panel
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
        

        private void Btn_pacientes_Click(object sender, EventArgs e)
        {
            
            Boton_Click(sender, e);
            AbrirUserControl(new UC_PacientesM()); // Poner el UserControl de Pacientes
        }

        private void Btn_turnos_Click(object sender, EventArgs e)
        {
            
            Boton_Click(sender, e);
            AbrirUserControl(new panel1()); // Poner el UserControl de Turnos

        }

        private void Btn_consulta_Click(object sender, EventArgs e)
        {
            Boton_Click(sender, e);
            AbrirUserControl(new ConsultaMedica()); // Poner el UserControl de Consultas
        }
       
        private void btn_salir_Click(object sender, EventArgs e)
        {
            // Confirmación para salir

            DialogResult dr = MessageBox.Show("¿Seguro que desea salir?",
                                      "Confirmación",
                                      MessageBoxButtons.YesNo
                                    );

            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
