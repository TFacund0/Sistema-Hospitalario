using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO.EstadoPacienteDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Turnos;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;


namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos
{
    public partial class UC_VisualizarTurno : UserControl
    {
        // Estado del turno que se está visualizando
        private TurnoDTO _turno;
        
        // Modo edición
        private bool _modoEdicion = false;

        // Notifica al contenedor (MenuAdministrativo) que se pidió cancelar
        public event EventHandler CancelarVisualizacionSolicitada;

        public UC_VisualizarTurno(TurnoDTO turno)
        {
            InitializeComponent();
            _turno = turno ?? throw new ArgumentNullException(nameof(turno));

            ConfigurarUISoloLectura();
        }

        // ======================== CONFIGURACION DEL FORMULARIO ========================
        // Configura la UI para modo solo lectura
        private void ConfigurarUISoloLectura()
        {
            ToggleEdicion(false);
            CargarDatosLectura(_turno);
            CargarCombosBox(_turno);
        }

        // Carga los datos del turno en los controles de solo lectura
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

        // Carga los datos en los ComboBox
        private void CargarCombosBox(TurnoDTO p_turno)
        {
            CargarComboPaciente(p_turno);
            CargarComboMedico(p_turno);
            CargarComboProcedimiento(p_turno);
        }

        // Habilita o deshabilita los controles para edición
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

        // ======================== CARGA DE COMBOS ========================
        // Carga los pacientes en el ComboBox
        private void CargarComboPaciente(TurnoDTO p_turno)
        {
            PacienteService _servicioPaciente = new PacienteService();
            
            List<PacienteListadoDto> listaPacientes = _servicioPaciente.ListarPacientes();

            var fuente = listaPacientes
                .Where(p => p.Estado == "Activo" || p.Estado == "Internado")
                .Select(p => new {
                    Id = p.Id,
                    Display = $"{p.Paciente} ({p.DNI})"
                })
                .ToList();

            cbPaciente.DropDownStyle = ComboBoxStyle.DropDown;
            cbPaciente.DataSource = fuente;
            cbPaciente.DisplayMember = "Display";
            cbPaciente.ValueMember = "Id";

            if (p_turno != null && p_turno.Id_paciente > 0)
            {
                cbPaciente.SelectedValue = p_turno.Id_paciente;
                
                cbPaciente.Text = ((dynamic)cbPaciente.SelectedItem)?.Display ?? string.Empty;
            }
        }

        // Carga los médicos en el ComboBox
        private void CargarComboMedico(TurnoDTO p_turno)
        {
            MedicoService _servicioMedico = new MedicoService();

            List<MedicoDto> listaMedicos = _servicioMedico.ListarMedicos() ?? new List<MedicoDto>();
            
            var fuente = listaMedicos
                .Select(m => new {
                    Id = m.Id,
                    Display = $"{m.Apellido} {m.Nombre} ({m.Especialidad})"
                })
                .ToList();
            
            cbMedico.DropDownStyle = ComboBoxStyle.DropDown;
            cbMedico.DataSource = fuente;
            cbMedico.DisplayMember = "Display";
            cbMedico.ValueMember = "Id";

            if (p_turno != null && p_turno.Id_medico > 0)
            {
                cbMedico.SelectedValue = p_turno.Id_medico;

                cbMedico.Text = ((dynamic)cbMedico.SelectedItem)?.Display ?? string.Empty;
            }
        }

        // Carga los procedimientos en el ComboBox
        private void CargarComboProcedimiento(TurnoDTO p_turno)
        {
            ProcedimientoService _servicioProcedimiento = new ProcedimientoService();
            List<ProcedimientoDto> listaProcedimientos = _servicioProcedimiento.ListarProcedimientos() ?? new List<ProcedimientoDto>();

            var fuente = listaProcedimientos
                .Select(p => new {
                    Id = p.Id,
                    Display = p.Name
                })
                .ToList();

            cbProcedimiento.DropDownStyle = ComboBoxStyle.DropDown;
            cbProcedimiento.DataSource = fuente;
            cbProcedimiento.DisplayMember = "Display";
            cbProcedimiento.ValueMember = "Id";

            if (p_turno != null && p_turno.Id_procedimiento > 0)
            {
                cbProcedimiento.SelectedValue = p_turno.Id_procedimiento;

                cbProcedimiento.Text = ((dynamic)cbProcedimiento.SelectedItem)?.Display ?? string.Empty;
            }
        }

        // ======================== GUARDAR CAMBIOS ========================
        // Actualiza el objeto turno con los datos de los controles
        private void ActualizarTurno()
        {
            _turno.Paciente = cbPaciente.Text.Trim();
            _turno.Medico = cbMedico.Text.Trim();
            _turno.Procedimiento = cbProcedimiento.Text.Trim();
            _turno.Id_paciente = (int)(cbPaciente.SelectedValue ?? 0);
            _turno.Id_medico = (int)(cbMedico.SelectedValue ?? 0);
            _turno.Id_procedimiento = (int)(cbProcedimiento.SelectedValue ?? 0);
            _turno.Correo = txtCorreo.Text.Trim();
            _turno.Telefono = txtTelefono.Text.Trim();
            _turno.FechaTurno = dtpFechaTurno.Value;
            _turno.Observaciones = txtObservaciones.Text.Trim();
        }

        // Guarda los cambios del turno en la base de datos
        private void GuardarCambios()
        {
            TurnoService turnoService = new TurnoService();
            turnoService.ActualizarTurno(_turno.Id_turno, _turno);
        }

        // ======================== EVENTOS BOTONES ========================
        // Botón Modificar/Guardar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                // Pasar a edición
                ToggleEdicion(true);
            }
            else
            {
                // Mensaje de confirmación
                var confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas guardar este turno?",
                    "Confirmar guardado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion != DialogResult.Yes)
                    return;

                // Guardar cambios
                if (string.IsNullOrWhiteSpace(txtPaciente.Text) ||
                    string.IsNullOrWhiteSpace(txtMedico.Text))
                {
                    MessageBox.Show("Paciente y Médico son obligatorios.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ActualizarTurno();
                GuardarCambios();
                ConfigurarUISoloLectura();

                MessageBox.Show("Cambios guardados.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            TurnoService turnoService = new TurnoService();
            var dr = MessageBox.Show("¿Eliminar este turno?",
                                     "Confirmación",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                turnoService.EliminarTurno(_turno.Id_turno);
                CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
            }            
        }

        // Botón Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            CancelarVisualizacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }

}
