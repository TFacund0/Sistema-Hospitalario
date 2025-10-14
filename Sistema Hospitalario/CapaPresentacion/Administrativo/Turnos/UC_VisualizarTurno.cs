using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;


namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos
{
    public partial class UC_VisualizarTurno : UserControl
    {
        private TurnoDTO _turno;        
        private bool _modoEdicion = false;

        public event EventHandler CancelarVisualizacionSolicitada;
        public event EventHandler<TurnoDTO> TurnoActualizado;
        public event EventHandler<TurnoDTO> TurnoEliminado;

        public UC_VisualizarTurno(TurnoDTO turno)
        {
            InitializeComponent();
            _turno = turno ?? throw new ArgumentNullException(nameof(turno));

            CargarDatos(_turno);
            ToggleEdicion(false);   
        }

        private void CargarDatos(TurnoDTO t)
        {
            txtPaciente.Text = t.Paciente;
            txtMedico.Text = t.Medico;
            txtProcedimiento.Text = t.Procedimiento;
            txtCorreo.Text = t.Correo;
            txtDni.Text = t.DNI;
            txtTelefono.Text = t.Telefono;
            dtpFechaTurno.Value = t.FechaTurno == default ? DateTime.Now : t.FechaTurno;
            dtpFechaRegistro.Value = t.FechaRegistro == default ? DateTime.Now : t.FechaRegistro;
            txtObservaciones.Text = t.Observaciones;
        }

        private void VolcarEnDTO()
        {
            _turno.Paciente = txtPaciente.Text.Trim();
            _turno.Medico = txtMedico.Text.Trim();
            _turno.Procedimiento = txtProcedimiento.Text.Trim();
            _turno.Correo = txtCorreo.Text.Trim();
            _turno.DNI = txtDni.Text.Trim();
            _turno.Telefono = txtTelefono.Text.Trim();
            _turno.FechaTurno = dtpFechaTurno.Value;
            _turno.FechaRegistro = dtpFechaRegistro.Value;
            _turno.Observaciones = txtObservaciones.Text.Trim();
        }

        private void ToggleEdicion(bool habilitar)
        {
            _modoEdicion = habilitar;
            
            txtPaciente.ReadOnly = !habilitar;
            txtMedico.ReadOnly = !habilitar;
            txtProcedimiento.ReadOnly = !habilitar;
            txtCorreo.ReadOnly = !habilitar;
            txtDni.ReadOnly = !habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtObservaciones.ReadOnly = !habilitar;

            dtpFechaTurno.Enabled = habilitar;
            dtpFechaRegistro.Enabled = habilitar;

            // Botones
            btnModificar.Text = habilitar ? "Guardar" : "Modificar";
            btnEliminar.Enabled = !habilitar; // mientras edito, no dejo eliminar
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                // Pasar a edición
                ToggleEdicion(true);
            }
            else
            {
                // Guardar cambios
                if (string.IsNullOrWhiteSpace(txtPaciente.Text) ||
                    string.IsNullOrWhiteSpace(txtMedico.Text))
                {
                    MessageBox.Show("Paciente y Médico son obligatorios.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                VolcarEnDTO();
                ToggleEdicion(false);

                // Notificar al contenedor (para persistir y/o refrescar lista)
                TurnoActualizado?.Invoke(this, _turno);

                MessageBox.Show("Cambios guardados.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("¿Eliminar este turno?",
                                     "Confirmación",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                TurnoEliminado?.Invoke(this, _turno);
                // volver automáticamente a la lista
                CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }

}
