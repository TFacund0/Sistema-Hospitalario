using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;

using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización
{
    public partial class UC_RegistrarInternacion : UserControl
    {
        // Servicios para manejar la lógica de negocio
        private readonly PacienteService _servicioPaciente = new PacienteService();
        private readonly HabitacionService _servicioHabitacion = new HabitacionService();
        private readonly CamaService _servicioCama = new CamaService();
        private readonly MedicoService _servicioMedico = new MedicoService();
        private readonly ProcedimientoService _servicioProcedimiento = new ProcedimientoService();

        // Atributos que almacenan la informacion de la base de datos a traves de los DTOs
        private List<PacienteDto> listaPacientes = new List<PacienteDto>();
        private List<HabitacionDto> listaHabitaciones = new List<HabitacionDto>();
        private List<CamaDto> listaCamas = new List<CamaDto>();
        private List<MedicoDto> listaMedicos = new List<MedicoDto>();
        private List<ProcedimientoDto> listaProcedimientos = new List<ProcedimientoDto>();

        // Listas de texto (lo que se muestra en los combos)
        private readonly List<string> _maestroPaciente = new List<string>();
        private readonly List<string> _maestroHabitacion = new List<string>();
        private readonly List<string> _maestroCama = new List<string>();
        private readonly List<string> _maestroMedico = new List<string>();
        private readonly List<string> _maestroProcedimiento = new List<string>();

        // Bandera para evitar reentrancia en eventos de texto
        private bool _actualizandoInterno = false;

        // Evento para notificar al menuAdministrativo que se solicitó cancelar el registro
        public event EventHandler CancelarInternacionSolicitada;

        // ============================= CONSTRUCTOR =============================
        public UC_RegistrarInternacion()
        {
            InitializeComponent();

            // Inputs
            txtPiso.TextChanged += txtPiso_TextChanged;

            // FIX: engancho también filtros de Habitación (si no los pusiste en el Designer)
            cbHabitacion.TextUpdate -= cbHabitacion_TextUpdate; // evitar suscripciones duplicadas
            cbHabitacion.TextUpdate += cbHabitacion_TextUpdate;

            // UX opcional para abrir listas
            cbHabitacion.Enter += (s, ev) => cbHabitacion.DroppedDown = true;
            cbHabitacion.MouseDown += (s, ev) => cbHabitacion.DroppedDown = true;

            cbCama.Enter += (s, ev) => cbCama.DroppedDown = true;
            cbCama.MouseDown += (s, ev) => cbCama.DroppedDown = true;

            DatosComboBoxPaciente();
            DatosComboBoxMedico();
            DatosComboBoxProcedimiento();
        }

        // ============================= COMBO PACIENTE =============================
        private void DatosComboBoxPaciente()
        {
            listaPacientes = _servicioPaciente.ListarAllDatosPaciente() ?? new List<PacienteDto>();

            _maestroPaciente.Clear();
            foreach (var unPaciente in listaPacientes)
                _maestroPaciente.Add($"{unPaciente.Apellido} {unPaciente.Nombre}");

            // Config del ComboBox
            cbPaciente.DropDownStyle = ComboBoxStyle.DropDown;   // editable
            cbPaciente.AutoCompleteMode = AutoCompleteMode.None; // evitamos pelea con nuestro filtro
            cbPaciente.AutoCompleteSource = AutoCompleteSource.None;

            // Carga inicial
            cbPaciente.DataSource = new BindingList<string>(_maestroPaciente);

            // FIX: evitar doble suscripción si este método se llama más de una vez
            cbPaciente.TextUpdate -= cbPaciente_TextUpdate;
            cbPaciente.TextUpdate += cbPaciente_TextUpdate;

            // UX: abrir al enfocar/click
            cbPaciente.Enter -= CbPaciente_Enter;    // FIX: evitar duplicados
            cbPaciente.Enter += CbPaciente_Enter;
            cbPaciente.MouseDown -= CbPaciente_MouseDown; // FIX
            cbPaciente.MouseDown += CbPaciente_MouseDown;
        }

        // ============================= UX COMBO PACIENTE =============================
        private void CbPaciente_Enter(object sender, EventArgs e) => cbPaciente.DroppedDown = true;
        private void CbPaciente_MouseDown(object sender, MouseEventArgs e) => cbPaciente.DroppedDown = true;

        // ============================= COMBO MEDICO =============================
        private void DatosComboBoxMedico()
        {
            listaMedicos = _servicioMedico.ListarMedicos() ?? new List<MedicoDto>();
            
            _maestroMedico.Clear();
            foreach (MedicoDto unMedico in listaMedicos)
                _maestroMedico.Add($"{unMedico.Apellido} {unMedico.Nombre} - {unMedico.Especialidad}");

            // Config del ComboBox
            cbMedico.DropDownStyle = ComboBoxStyle.DropDown;   // editable
            cbMedico.AutoCompleteMode = AutoCompleteMode.None; // evitamos pelea con nuestro filtro
            cbMedico.AutoCompleteSource = AutoCompleteSource.None;

            // Carga inicial
            cbMedico.DataSource = new BindingList<string>(_maestroMedico);

            // FIX: evitar doble suscripción si este método se llama más de una vez
            cbMedico.TextUpdate -= CbMedico_TextUpdate;
            cbMedico.TextUpdate += CbMedico_TextUpdate;

            // UX: abrir al enfocar/click
            cbMedico.Enter -= CbMedico_Enter;
            cbMedico.Enter += CbMedico_Enter;
            cbMedico.MouseDown -= CbMedico_MouseDown; 
            cbMedico.MouseDown += CbMedico_MouseDown;
        }

        // ============================= UX COMBO MEDICO =============================
        private void CbMedico_Enter(object sender, EventArgs e) => cbMedico.DroppedDown = true;
        private void CbMedico_MouseDown(object sender, MouseEventArgs e) => cbMedico.DroppedDown = true;

        // ============================= COMBO PROCEDIMIENTO =============================
        private void DatosComboBoxProcedimiento()
        {
            listaProcedimientos = _servicioProcedimiento.ListarProcedimientos() ?? new List<ProcedimientoDto>();
            _maestroProcedimiento.Clear();
            
            foreach (var unProcedimiento in listaProcedimientos)
                _maestroProcedimiento.Add(unProcedimiento.Name);
            
            // Config del ComboBox
            cbProcedimiento.DropDownStyle = ComboBoxStyle.DropDown;   // editable
            cbProcedimiento.AutoCompleteMode = AutoCompleteMode.None; // evitamos pelea con nuestro filtro
            cbProcedimiento.AutoCompleteSource = AutoCompleteSource.None;
            
            // Carga inicial
            cbProcedimiento.DataSource = new BindingList<string>(_maestroProcedimiento);
            
            // FIX: evitar doble suscripción si este método se llama más de una vez
            cbProcedimiento.TextUpdate -= CbProcedimiento_TextUpdate;
            cbProcedimiento.TextUpdate += CbProcedimiento_TextUpdate;
            
            // UX: abrir al enfocar/click
            cbProcedimiento.Enter -= CbProcedimiento_Enter;
            cbProcedimiento.Enter += CbProcedimiento_Enter;
            cbProcedimiento.MouseDown -= CbProcedimiento_MouseDown;
            cbProcedimiento.MouseDown += CbProcedimiento_MouseDown;
        }

        // ============================= UX COMBO MEDICO =============================
        private void CbProcedimiento_Enter(object sender, EventArgs e) => cbProcedimiento.DroppedDown = true;
        private void CbProcedimiento_MouseDown(object sender, MouseEventArgs e) => cbProcedimiento.DroppedDown = true;


        // ============================= COMBO HABITACIÓN =============================
        private void CargarHabitacionesPorPiso(string pisoTexto)
        {
            _maestroHabitacion.Clear(); // FIX: limpiamos en todos los casos

            if (!string.IsNullOrWhiteSpace(pisoTexto) && int.TryParse(pisoTexto, out var piso) && piso > 0)
            {
                // 1) Traer datos desde negocio
                listaHabitaciones = _servicioHabitacion.ListarHabitacionesXPiso(pisoTexto) ?? new List<HabitacionDto>();

                foreach (var unaHabitacion in listaHabitaciones)
                    _maestroHabitacion.Add(unaHabitacion.Nro_habitacion.ToString());
            }

            var seleccionadoAntes = cbHabitacion.Text; // FIX: conservamos por texto (porque trabajamos con string)

            try
            {
                _actualizandoInterno = true;
                cbHabitacion.BeginUpdate();

                // 3) Refrescar lista sin provocar loops
                cbHabitacion.DataSource = null;
                cbHabitacion.DataSource = new BindingList<string>(_maestroHabitacion);

                // 4) Reset de selección
                cbHabitacion.SelectedIndex = -1;
                cbHabitacion.Text = string.Empty;

                // 5) Restaurar (si tiene sentido)
                if (!string.IsNullOrEmpty(seleccionadoAntes) && _maestroHabitacion.Contains(seleccionadoAntes))
                    cbHabitacion.Text = seleccionadoAntes;

                if (cbHabitacion.Focused) cbHabitacion.DroppedDown = true;
            }
            finally
            {
                cbHabitacion.EndUpdate();
                _actualizandoInterno = false;
            }
        }

        // ============================= COMBO CAMA =============================
        private void CargarCamaPorHabitacion(string habitacionText)
        {
            _maestroCama.Clear(); // FIX

            if (!string.IsNullOrWhiteSpace(habitacionText) &&
                int.TryParse(habitacionText, out var habitacion) && habitacion > 0)
            {
                listaCamas = _servicioCama.ListarCamasXHabitacion(habitacionText) ?? new List<CamaDto>();

                foreach (var unaCama in listaCamas)
                    _maestroCama.Add(unaCama.NroCama.ToString());
            }

            try
            {
                _actualizandoInterno = true;
                cbCama.BeginUpdate();

                cbCama.DataSource = null;
                cbCama.DataSource = new BindingList<string>(_maestroCama);
                cbCama.SelectedIndex = -1;
                cbCama.Text = string.Empty;

                if (cbCama.Focused) cbCama.DroppedDown = true;
            }
            finally
            {
                cbCama.EndUpdate();
                _actualizandoInterno = false;
            }
        }

        // ============================= FILTROS =============================

        // ============================= FILTRO PACIENTE =============================
        private void cbPaciente_TextUpdate(object sender, EventArgs e)
        {
            if (_actualizandoInterno) return;

            string texto = cbPaciente.Text?.Trim() ?? "";
            int caret = cbPaciente.SelectionStart;

            var filtrados = string.IsNullOrEmpty(texto)
                ? _maestroPaciente
                : _maestroPaciente
                    .Where(p => p.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            if (filtrados.Count == 0)
                filtrados.Add("— sin coincidencias —");

            try
            {
                _actualizandoInterno = true;
                cbPaciente.BeginUpdate();

                cbPaciente.DataSource = null;
                cbPaciente.DataSource = new BindingList<string>(filtrados);

                cbPaciente.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            finally
            {
                cbPaciente.EndUpdate();
                cbPaciente.Text = texto;
                cbPaciente.SelectionStart = Math.Min(caret, cbPaciente.Text.Length);
                _actualizandoInterno = false;
            }
        }

        // ============================= FILTRO HABITACIÓN =============================
        private void cbHabitacion_TextUpdate(object sender, EventArgs e)
        {
            if (_actualizandoInterno) return;

            string texto = cbHabitacion.Text?.Trim() ?? "";
            int caret = cbHabitacion.SelectionStart;

            var filtrados = string.IsNullOrEmpty(texto)
                ? _maestroHabitacion
                : _maestroHabitacion
                    .Where(p => p.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            try
            {
                _actualizandoInterno = true;
                cbHabitacion.BeginUpdate();

                cbHabitacion.DataSource = null;
                cbHabitacion.DataSource = new BindingList<string>(filtrados);

                cbHabitacion.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            finally
            {
                cbHabitacion.EndUpdate();
                cbHabitacion.Text = texto;
                cbHabitacion.SelectionStart = Math.Min(caret, cbHabitacion.Text.Length);
                _actualizandoInterno = false;
            }
        }

        // ============================= FILTRO MÉDICO =============================
        private void CbMedico_TextUpdate(object sender, EventArgs e)
        {
            if (_actualizandoInterno) return;
            string texto = cbMedico.Text?.Trim() ?? "";
            int caret = cbMedico.SelectionStart;
            var filtrados = string.IsNullOrEmpty(texto)
                ? _maestroMedico
                : _maestroMedico
                    .Where(p => p.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            if (filtrados.Count == 0)
                filtrados.Add("— sin coincidencias —");
            try
            {
                _actualizandoInterno = true;
                cbMedico.BeginUpdate();
                cbMedico.DataSource = null;
                cbMedico.DataSource = new BindingList<string>(filtrados);
                cbMedico.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            finally
            {
                cbMedico.EndUpdate();
                cbMedico.Text = texto;
                cbMedico.SelectionStart = Math.Min(caret, cbMedico.Text.Length);
                _actualizandoInterno = false;
            }
        }

        // ============================= FILTRO PROCEDIMIENTO =============================
        private void CbProcedimiento_TextUpdate(object sender, EventArgs e)
        {
            if (_actualizandoInterno) return;
            
            string texto = cbProcedimiento.Text?.Trim() ?? "";
            int caret = cbProcedimiento.SelectionStart;
            
            var filtrados = string.IsNullOrEmpty(texto)
                ? _maestroProcedimiento
                : _maestroProcedimiento
                    .Where(p => p.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            
            if (filtrados.Count == 0)
                filtrados.Add("— sin coincidencias —");
            
            try
            {
                _actualizandoInterno = true;
                cbProcedimiento.BeginUpdate();
                cbProcedimiento.DataSource = null;
                cbProcedimiento.DataSource = new BindingList<string>(filtrados);
                cbProcedimiento.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            finally
            {
                cbProcedimiento.EndUpdate();
                cbProcedimiento.Text = texto;
                cbProcedimiento.SelectionStart = Math.Min(caret, cbProcedimiento.Text.Length);
                _actualizandoInterno = false;
            }
        }

        // ============================= REACCIONES A CAMBIOS =============================
        private void txtPiso_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPiso.Text, out var piso) && piso > 0)
                CargarHabitacionesPorPiso(txtPiso.Text);
            else
                CargarHabitacionesPorPiso("");
        }

        private void cbHabitacion_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbHabitacion.Text, out var habitacion) && habitacion > 0)
                CargarCamaPorHabitacion(cbHabitacion.Text);
            else
                CargarCamaPorHabitacion("");
        }

        // ============================= VALIDACIONES =============================
        private void dtpFechaInicio_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaInicio.Value > DateTime.Now)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha/hora de inicio no puede ser futura.");
            }
            else
            {
                errorProvider1.SetError(dtpFechaInicio, "");
            }
        }

        private void dtpFechaFin_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFin.Checked)
            {
                if (dtpFechaFin.Value < dtpFechaInicio.Value)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dtpFechaFin, "La fecha de egreso debe ser posterior al inicio.");
                    return;
                }
                if (dtpFechaFin.Value > DateTime.Now)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dtpFechaFin, "La fecha/hora de egreso no puede ser futura.");
                    return;
                }
            }
            errorProvider1.SetError(dtpFechaFin, "");
        }

        private void txtPiso_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPiso.Text) || !int.TryParse(txtPiso.Text, out int piso) || piso <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Piso obligatorio y numérico (>0).");
            }
            else if (txtPiso.Text.Length > 3)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Máximo 3 dígitos.");
            }
            else
            {
                errorProvider1.SetError(txtPiso, "");
            }
        }

        private void txtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            if (txtObservaciones.Text.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Máximo 300 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObservaciones, "");
            }
        }

        // ============================= REESTRICCIONES DE ENTRADA DE DATOS =============================
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // ============================= BOTONES =============================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPiso.Clear();
            txtObservaciones.Clear();
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }
    }
}
