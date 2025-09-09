using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_Exportar : UserControl
    {
        public UC_Exportar()
        {
            InitializeComponent();
        }

        // Evento para notificar al formulario padre que se solicitó cancelar el registro
        public event EventHandler CancelarRegistroSolicitado;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
