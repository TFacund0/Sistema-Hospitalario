using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico.Turnos
{
    public partial class Form_CambiarEstadoTurno : Form
    {
        public int NuevoEstadoId { get; private set; }
        private string _estadoActual;
        public Form_CambiarEstadoTurno(string estadoActual)
        {
            InitializeComponent();
            CargarEstados();
            _estadoActual = estadoActual;

            // Configurar propiedades del Form
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = "Cambiar Estado del Turno";
        }
        private void CargarEstados()
        {
            // (Asumimos que tenés un ComboBox 'cboEstadosTurno')
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var estados = db.estado_turno
                                .Select(e => new { Id = e.id_estado_turno, Nombre = e.nombre })
                                .ToList();

                cboEstadosTurno.DataSource = estados;
                cboEstadosTurno.DisplayMember = "Nombre";
                cboEstadosTurno.ValueMember = "Id";
                cboEstadosTurno.Text = _estadoActual; // Selecciona el estado actual
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.NuevoEstadoId = (int)cboEstadosTurno.SelectedValue;
            this.DialogResult = DialogResult.OK; // Lo asignamos manualmente
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
