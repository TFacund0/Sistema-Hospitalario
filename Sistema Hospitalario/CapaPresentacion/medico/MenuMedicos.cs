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

        private void AbrirUserControl(UserControl uc)
        {
            panelContenedor.Controls.Clear();   // Limpia el panel
            uc.Dock = DockStyle.Fill;           // Que ocupe todo el espacio disponible
            panelContenedor.Controls.Add(uc);   // Lo agrega al panel
            uc.BringToFront();                  // Lo trae al frente
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new UC_Home());
        }
    }
}
