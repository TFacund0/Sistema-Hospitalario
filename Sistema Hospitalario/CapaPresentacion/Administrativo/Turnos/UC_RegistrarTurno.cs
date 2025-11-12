using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;



namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos
{
    public partial class UC_RegistrarTurno : UserControl
    {
        // Servicio para interactuar con la capa de negocio
        private static readonly PacienteService pacienteService = new PacienteService();
        private readonly PacienteService _servicioPaciente = pacienteService;
        private readonly MedicoService _servicioMedico = new MedicoService();
        private readonly ProcedimientoService _servicioProcedimiento = new ProcedimientoService();

        // Listas maestras para validaciones de ComboBox
        private readonly List<string> _maestroPaciente = new List<string>();
        private readonly List<string> _maestroMedico = new List<string>();
        private readonly List<string> _maestroProcedimiento = new List<string>();

        // Notifica al contenedor (MenuAdministrativo) que se pidió cancelar
        public event EventHandler CancelarTurnoSolicitado;

        // ======================== CONSTRUCTOR UC REGISTRAR TURNO ========================
        public UC_RegistrarTurno()
        {
            InitializeComponent();

            // No permitir elegir hoy ni fechas pasadas
            dtpFechaTurno.MinDate = DateTime.Today.AddDays(1);
            dtpFechaTurno.Value = DateTime.Today.AddDays(1);

            DatosComboBoxPaciente();
            DatosComboBoxMedico();
            DatosComboBoxProcedimiento();
        }


        // ========================= COMBO BOX PACIENTE =========================
        private void DatosComboBoxPaciente()
        {
            List<PacienteDto> listaPacientes = _servicioPaciente.ListarPacientes();

            var fuente = listaPacientes
                .Where(p => p.Estado_paciente == "activo")
                .Select(p => new {
                    p.Id,
                    Display = $"{p.Apellido} {p.Nombre} ({p.Dni})"
                })
                .ToList();

            _maestroPaciente.Clear();
            _maestroPaciente.AddRange(fuente.Select(x => x.Display));

            cbPaciente.DropDownStyle = ComboBoxStyle.DropDown;
            cbPaciente.DataSource = fuente;
            cbPaciente.DisplayMember = "Display";
            cbPaciente.ValueMember = "Id";
        }

        // ========================= COMBO BOX MEDICO =========================
        private void DatosComboBoxMedico()
        {
            List<MedicoDto> listaMedicos = _servicioMedico.ListarMedicos() ?? new List<MedicoDto>();
            var fuente = listaMedicos
                .Select(m => new {
                    m.Id,
                    Display = $"{m.Apellido} {m.Nombre} ({m.Especialidad})"
                })
                .ToList();

            _maestroMedico.Clear();
            _maestroMedico.AddRange(fuente.Select(x => x.Display));

            cbMedico.DropDownStyle = ComboBoxStyle.DropDown;
            cbMedico.DataSource = fuente;
            cbMedico.DisplayMember = "Display";
            cbMedico.ValueMember = "Id";
        }

        // ========================= COMBOX BOX PROCEDIMIENTO =========================
        private void DatosComboBoxProcedimiento()
        {
            List<ProcedimientoDto> listaProcedimientos = _servicioProcedimiento.ListarProcedimientos() ?? new List<ProcedimientoDto>();

            var fuente = listaProcedimientos
                .Select(p => new {
                    p.Id,
                    Display = p.Name
                })
                .ToList();

            _maestroProcedimiento.Clear();
            _maestroProcedimiento.AddRange(fuente.Select(x => x.Display));

            cbProcedimiento.DropDownStyle = ComboBoxStyle.DropDown;
            cbProcedimiento.DataSource = fuente;
            cbProcedimiento.DisplayMember = "Display";
            cbProcedimiento.ValueMember = "Id";
        }

        // ========================= VALIDACIONES =========================
        // ==== Validacion ComboBox Paciente ====
        private void CbPaciente_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbPaciente.Text) || !_maestroPaciente.Contains(cbPaciente.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbPaciente, "Seleccione un paciente válido de la lista.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbPaciente, null);
            }
        }

        // ==== Validacion ComboBox Medico ====
        private void CbMedico_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbMedico.Text) || !_maestroMedico.Contains(cbMedico.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbMedico, "Seleccione un médico válido de la lista.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbMedico, null);
            }
        }

        // ==== Validacion ComboBox Procedimiento ====
        private void CbProcedimiento_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbProcedimiento.Text) || !_maestroProcedimiento.Contains(cbProcedimiento.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbProcedimiento, "Seleccione un procedimiento válido de la lista.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbProcedimiento, null);
            }
        }

        // ==== Validacion TextBox Observaciones ====
        private void TxtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            var texto = txtObservaciones.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Las observaciones son obligatorias.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtObservaciones, null);
            }
        }

        // ==== Validacion Correo ====
        private void TxtCorreo_Validating(object sender, CancelEventArgs e)
        {
            var correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(correo))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCorreo, "El correo es obligatorio.");
                return;
            }

            // Validación simple de formato de correo
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCorreo, "El formato del correo no es válido.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCorreo, null);
            }
        }

        // ==== Validacion DateTimePicker Fecha ====
        private void DtpFecha_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidarFechaTurno())
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        // Validación específica de la fecha del turno
        private bool ValidarFechaTurno()
        {
            if (dtpFechaTurno.Value.Date <= DateTime.Today)
            {
                errorProvider1.SetError(dtpFechaTurno, "La fecha del turno debe ser posterior a hoy.");
                return false;
            }

            errorProvider1.SetError(dtpFechaTurno, null);
            return true;
        }


        // ========================= BOTONES =========================

        // ==== Boton Cancelar ====
        private void BtnCancelar_Click_1(object sender, EventArgs e)
        {
            CancelarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // ==== Boton Guardar ====
        private void BtnGuardar_Click_1(object sender, EventArgs e)
        {
            // Confirmación previa
            var confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas guardar este turno?",
                "Confirmar guardado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
                return;

            // Ejecutar todas las validaciones de los controles (Validating de combos, correo, observaciones, etc.)
            bool controlesValidos = this.ValidateChildren();

            // Refuerzo explícito de la validación de fecha (por si el Validating no se disparó)
            bool fechaValida = ValidarFechaTurno(); //

            if (!controlesValidos || !fechaValida)
            {
                MessageBox.Show(
                    "Por favor, corrija los errores antes de guardar.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Chequeo defensivo extra por si algún SelectedValue viene en null
            if (cbPaciente.SelectedValue == null || cbMedico.SelectedValue == null || cbProcedimiento.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar paciente, médico y procedimiento.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Construir el DTO del turno
            var nuevoTurno = new TurnoDto
            {
                Id_paciente = (int)cbPaciente.SelectedValue,
                Id_medico = (int)cbMedico.SelectedValue,
                Id_procedimiento = (int)cbProcedimiento.SelectedValue,
                FechaTurno = dtpFechaTurno.Value,
                Observaciones = txtObservaciones.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),  
                Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim()         
            };

            // Guardar usando el servicio
            try
            {
                var turnoService = new TurnoService();
                turnoService.RegistrarTurno(nuevoTurno);

                MessageBox.Show(
                    "Turno registrado con éxito.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al registrar el turno. Verifique los datos o inténtelo nuevamente. ",
                    "Error: " + ex,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        // ==== Boton Limpiar ====
        private void BtnLimpiar_Click_1(object sender, EventArgs e)
        {
            cbPaciente.SelectedIndex = -1;
            cbMedico.SelectedIndex = -1;
            cbProcedimiento.SelectedIndex = -1;

            dtpFechaTurno.MinDate = DateTime.Today.AddDays(1);
            dtpFechaTurno.Value = DateTime.Today.AddDays(1);

            txtObservaciones.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            errorProvider1.Clear();
        }

    }
}
