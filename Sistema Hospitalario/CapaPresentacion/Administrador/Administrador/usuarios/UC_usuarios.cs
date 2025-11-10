using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.usuarios
{
    public partial class UC_usuarios : UserControl
    {
        public UC_usuarios()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_registrarUsuario());
        }
    }
}
