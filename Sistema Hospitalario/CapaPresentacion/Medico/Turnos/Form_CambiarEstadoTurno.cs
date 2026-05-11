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
    /// <summary>
    /// Ventana de diálogo que permite al médico cambiar el estado de un turno (ej: de Pendiente a En Proceso o Atendido).
    /// </summary>
    public partial class Form_CambiarEstadoTurno : Form
    {
        /// <summary>ID del nuevo estado seleccionado por el usuario.</summary>
        public int NuevoEstadoId { get; private set; }
        /// <summary>Estado actual del turno antes del cambio.</summary>
        private string _estadoActual;

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="Form_CambiarEstadoTurno"/>.
        /// </summary>
        /// <param name="estadoActual">Nombre del estado actual del turno para pre-seleccionarlo en el combo.</param>
        public Form_CambiarEstadoTurno(string estadoActual)
        {
            InitializeComponent();
            CargarEstados();
            _estadoActual = estadoActual;

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
