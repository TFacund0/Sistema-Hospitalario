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

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.medicos
{
    public partial class UC_Medicos : UserControl
    {
        public UC_Medicos()
        {
            InitializeComponent();
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_agregarMedico());
        }
    }
}
