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
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;



namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Turnos
{
    public partial class UC_RegistrarTurno : UserControl
    {
        private PacienteService _servicioPaciente = new PacienteService();
        private MedicoService _servicioMedico = new MedicoService();
        private ProcedimientoService _servicioProcedimiento = new ProcedimientoService();

        private List<string> _maestroPaciente = new List<string>();
        private List<string> _maestroMedico = new List<string>();
        private List<string> _maestroProcedimiento = new List<string>();

        // Notifica al contenedor (MenuAdministrativo) que se pidió cancelar
        public event EventHandler CancelarTurnoSolicitado;

        // ======================== CONSTRUCTOR UC REGISTRAR TURNO ========================
        public UC_RegistrarTurno()
        {
            InitializeComponent();

            DatosComboBoxPaciente();
            DatosComboBoxMedico();
            DatosComboBoxProcedimiento();
        }

        // ========================= COMBO BOX PACIENTE =========================
        private void DatosComboBoxPaciente()
        {
            List<PacienteDto> listaPacientes = _servicioPaciente.ListarAllDatosPaciente() ?? new List<PacienteDto>();

            var fuente = listaPacientes
                .Where(p => p.Estado_paciente == "Activo")
                .Select(p => new {
                    Id = p.Id,
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
                    Id = m.Id,
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
                    Id = p.Id,
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
        private void cbPaciente_Validating(object sender, CancelEventArgs e)
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
        private void cbMedico_Validating(object sender, CancelEventArgs e)
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
        private void cbProcedimiento_Validating(object sender, CancelEventArgs e)
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

        // ==== Validacion DateTimePicker Fecha ====
        private void dtpFecha_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaTurno.Value.Date < DateTime.Now.Date)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaTurno, "La fecha no puede ser en el pasado.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(dtpFechaTurno, null);
            }
        }

        // ========================= BOTONES =========================

        // ==== Boton Cancelar ====
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            CancelarTurnoSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // ==== Boton Guardar ====
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // Ejecuta todas las validaciones de Validating
            if (this.ValidateChildren())
            {
                TurnoService _servicioPaciente = new TurnoService();

                var nuevoTurno = new TurnoDto
                {
                    Id_paciente = (int)cbPaciente.SelectedValue,
                    Id_medico = (int)cbMedico.SelectedValue,
                    Id_procedimiento = (int)cbProcedimiento.SelectedValue,
                    FechaTurno = dtpFechaTurno.Value,
                    Observaciones = txtObservaciones.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                    Correo = string.IsNullOrWhiteSpace(txtCorreo.Text) ? null : txtCorreo.Text.Trim(),
                };

                _servicioPaciente.RegistrarTurno(nuevoTurno);

                MessageBox.Show("Turno registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==== Boton Limpiar ====
        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            cbPaciente.SelectedIndex = -1;
            cbMedico.SelectedIndex = -1;
            cbProcedimiento.SelectedIndex = -1;
            dtpFechaTurno.Value = DateTime.Now;
            txtObservaciones.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            errorProvider1.Clear();
        }
    }
}
