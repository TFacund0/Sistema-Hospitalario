using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO.EstadoPacienteDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
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

            ConfigurarUISoloLectura();
            CargarDatosLectura(_turno);

        }

        private void ConfigurarUISoloLectura()
        {
            ToggleEdicion(false);
        }

        private void CargarDatosLectura(TurnoDTO p_turno)
        {
            txtPaciente.Text = p_turno.Paciente;
            txtMedico.Text = p_turno.Medico;
            txtProcedimiento.Text = p_turno.Procedimiento;
            txtCorreo.Text = p_turno.Correo ?? string.Empty;
            txtTelefono.Text = p_turno.Telefono ?? string.Empty;
            dtpFechaTurno.Value = p_turno.FechaTurno;
            txtObservaciones.Text = p_turno.Observaciones ?? string.Empty;
        }

        private void ToggleEdicion(bool habilitar)
        {
            _modoEdicion = habilitar;

            txtPaciente.ReadOnly = !habilitar;
            txtMedico.ReadOnly = !habilitar;
            txtProcedimiento.ReadOnly = !habilitar;
            txtCorreo.ReadOnly = !habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtObservaciones.ReadOnly = !habilitar;
            dtpFechaTurno.Enabled = habilitar;

            txtPaciente.Visible = !habilitar;
            txtMedico.Visible = !habilitar;
            txtProcedimiento.Visible = !habilitar;

            cbPaciente.Visible = habilitar;
            cbPaciente.Enabled = habilitar;
            cbMedico.Visible = habilitar;
            cbMedico.Enabled = habilitar;
            cbProcedimiento.Visible = habilitar;
            cbProcedimiento.Enabled = habilitar;

            btnModificar.Text = habilitar ? "Guardar" : "Modificar";
            btnEliminar.Enabled = !habilitar;
        }

        /*
        private void CargarEstadosEnCombo()
        {
            var estados = _turnoService.ListarEstados(); // List<EstadoPacienteDto> { Id, Nombre }

            var lista = new List<EstadoPacienteDto>
            {
                new EstadoPacienteDto { Id = 0, Nombre = "— Seleccioná —" }
            };
            lista.AddRange(estados);

            cbEstadoInicial.DisplayMember = nameof(EstadoPacienteDto.Nombre);
            cbEstadoInicial.ValueMember = nameof(EstadoPacienteDto.Id);
            cbEstadoInicial.DataSource = lista;

            cbEstadoInicial.SelectedIndex = 0;

            cbEstadoInicial.DropDownStyle = ComboBoxStyle.DropDownList;
        }*/

        /*
        private void SeleccionarEstadoPorNombre(string nombreEstado)
        {
            if (string.IsNullOrWhiteSpace(nombreEstado))
            {
                cbEstadoInicial.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < cbEstadoInicial.Items.Count; i++)
            {
                var it = cbEstadoInicial.Items[i] as EstadoPacienteDto;
                if (it != null && string.Equals(it.Nombre, nombreEstado, StringComparison.OrdinalIgnoreCase))
                {
                    cbEstadoInicial.SelectedIndex = i;
                    return;
                }
            }
        }*/

        private void VolcarEnDTO()
        {
            _turno.Paciente = txtPaciente.Text.Trim();
            _turno.Medico = txtMedico.Text.Trim();
            _turno.Procedimiento = txtProcedimiento.Text.Trim();
            _turno.Correo = txtCorreo.Text.Trim();
            _turno.Telefono = txtTelefono.Text.Trim();
            _turno.FechaTurno = dtpFechaTurno.Value;
            _turno.Observaciones = txtObservaciones.Text.Trim();
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
