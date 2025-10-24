using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Hospitalización
{
    public partial class UC_RegistrarInternacion : UserControl
    {
        // Servicios para manejar la lógica de negocio
        private readonly PacienteService _servicioPaciente = new PacienteService();
        private readonly HabitacionService _servicioHabitacion = new HabitacionService();
        private readonly CamaService _servicioCama = new CamaService();
        private readonly MedicoService _servicioMedico = new MedicoService(new MedicoRepository());
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

            cbHabitacion.TextUpdate -= cbHabitacion_TextUpdate;
            cbHabitacion.TextUpdate += cbHabitacion_TextUpdate;

            cbHabitacion.Enter += (s, ev) => cbHabitacion.DroppedDown = true;
            cbHabitacion.MouseDown += (s, ev) => cbHabitacion.DroppedDown = true;

            cbCama.Enter += (s, ev) => cbCama.DroppedDown = true;
            cbCama.MouseDown += (s, ev) => cbCama.DroppedDown = true;

            cbPaciente.TextUpdate += cbPaciente_TextUpdate;
            cbMedico.TextUpdate += CbMedico_TextUpdate;
            cbProcedimiento.TextUpdate += CbProcedimiento_TextUpdate;

            DatosComboBoxPaciente();
            DatosComboBoxMedico();
            DatosComboBoxProcedimiento();
            SincronizarHabilitacionControles();

            ConfigurarFechaInicioConHora();
            ConfigurarFechaFinNull();
        }

        // ============================= COMBO PACIENTE =============================
        private void DatosComboBoxPaciente()
        {
            listaPacientes = _servicioPaciente.ListarAllDatosPaciente() ?? new List<PacienteDto>();

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

        // ============================= COMBO MEDICO =============================
        private void DatosComboBoxMedico()
        {
            listaMedicos = _servicioMedico.ListarMedicos() ?? new List<MedicoDto>();
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

        // ============================= COMBO PROCEDIMIENTO =============================
        private void DatosComboBoxProcedimiento()
        {
            listaProcedimientos = _servicioProcedimiento.ListarProcedimientos() ?? new List<ProcedimientoDto>();

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

        // ============================= COMBO HABITACIÓN =============================
        private void CargarHabitacionesPorPiso(string pisoTexto)
        {
            _maestroHabitacion.Clear(); // FIX: limpiamos en todos los casos

            if (!string.IsNullOrWhiteSpace(pisoTexto) && int.TryParse(pisoTexto, out var piso) && piso > 0)
            {
                listaHabitaciones = _servicioHabitacion.ListarHabitacionesXPiso(pisoTexto) ?? new List<HabitacionDto>();

                foreach (var unaHabitacion in listaHabitaciones)
                    _maestroHabitacion.Add(unaHabitacion.Nro_habitacion.ToString());
            }

            var seleccionadoAntes = cbHabitacion.Text; // FIX: conservamos por texto (porque trabajamos con string)

            try
            {
                _actualizandoInterno = true;
                cbHabitacion.BeginUpdate();

                cbHabitacion.DataSource = null;
                cbHabitacion.DataSource = new BindingList<string>(_maestroHabitacion);

                cbHabitacion.SelectedIndex = -1;
                cbHabitacion.Text = string.Empty;

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
            _maestroCama.Clear();

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

        // ============== FILTRO PACIENTE ==============
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

        // ============== FILTRO HABITACION ===============
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

        // ================= FILTRO MÉDICO ===================
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

        // ================= FILTRO PROCEDIMIENTO ===================
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

        // ============================= EVENTOS DE TEXTO QUE AFECTAN A OTROS CONTROLES =============================
        private void txtPiso_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPiso.Text, out var piso) && piso > 0)
                CargarHabitacionesPorPiso(txtPiso.Text);
            else
                CargarHabitacionesPorPiso("");

            SincronizarHabilitacionControles();
        }

        private void cbHabitacion_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbHabitacion.Text, out var habitacion) && habitacion > 0)
                CargarCamaPorHabitacion(cbHabitacion.Text);
            else
                CargarCamaPorHabitacion("");

            SincronizarHabilitacionControles(); // ← agregar
        }

        // ============================= HELPERS (opcionales, pero útiles) =============================
        private bool TextoCoincideConLista(ComboBox cb, List<string> maestro)
        {
            var t = cb.Text?.Trim();
            if (string.IsNullOrEmpty(t) || t == "— sin coincidencias —") return false;

            return maestro.Any(x => string.Equals(x?.Trim(), t, StringComparison.OrdinalIgnoreCase));
        }

        private bool TextoEsEnteroPositivo(string s, out int valor)
            => int.TryParse(s, out valor) && valor > 0;


        // ============================= VALIDACION PACIENTE =============================
        private void cbPaciente_Validating(object sender, CancelEventArgs e)
        {
            if (!TextoCoincideConLista(cbPaciente, _maestroPaciente))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbPaciente, "Seleccioná un paciente válido de la lista.");
            }
            else
            {
                errorProvider1.SetError(cbPaciente, "");
            }
        }

        // ============================= VALIDACION MÉDICO =============================
        private void cbMedico_Validating(object sender, CancelEventArgs e)
        {
            if (!TextoCoincideConLista(cbMedico, _maestroMedico))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbMedico, "Seleccioná un médico válido de la lista.");
            }
            else
            {
                errorProvider1.SetError(cbMedico, "");
            }
        }

        // ============================= VALIDACION PROCEDIMIENTO =============================
        private void cbProcedimiento_Validating(object sender, CancelEventArgs e)
        {
            var texto = cbProcedimiento.Text?.Trim() ?? "";

            // 3.1 primero: validar contra el maestro cargado
            bool coincide = _maestroProcedimiento.Any(x =>
                string.Equals(x?.Trim(), texto, StringComparison.OrdinalIgnoreCase));

            if (!coincide)
            {
                e.Cancel = true;
                errorProvider1.SetError(cbProcedimiento, "Seleccioná un procedimiento válido de la lista.");
                return;
            }

            if (cbProcedimiento.SelectedValue == null)
            {
                var proc = listaProcedimientos.FirstOrDefault(p =>
                    string.Equals(p.Name?.Trim(), texto, StringComparison.OrdinalIgnoreCase));

                if (proc != null)
                {
                    var fuente = listaProcedimientos.Select(p => new { Id = p.Id, Display = p.Name }).ToList();
                    cbProcedimiento.BeginUpdate();
                    try
                    {
                        cbProcedimiento.DataSource = fuente;
                        cbProcedimiento.DisplayMember = "Display";
                        cbProcedimiento.ValueMember = "Id";
                        cbProcedimiento.SelectedValue = proc.Id; // ← ahora sí queda vinculado
                    }
                    finally
                    {
                        cbProcedimiento.EndUpdate();
                    }
                }
            }

            errorProvider1.SetError(cbProcedimiento, "");
        }

        // ============================= VALIDACION PISO =============================
        private void txtPiso_Validating(object sender, CancelEventArgs e)
        {
            if (!TextoEsEnteroPositivo(txtPiso.Text, out var piso))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Piso obligatorio y numérico (>0).");
                return;
            }

            if (txtPiso.Text.Length > 3)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPiso, "Máximo 3 dígitos para el piso.");
                return;
            }

            errorProvider1.SetError(txtPiso, "");
        }


        // ============================= VALIDACION HABITACIÓN =============================
        private void cbHabitacion_Validating(object sender, CancelEventArgs e)
        {
            if (!TextoCoincideConLista(cbHabitacion, _maestroHabitacion)
                || !TextoEsEnteroPositivo(cbHabitacion.Text, out var nroHabitacion))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbHabitacion, "Seleccioná una habitación válida (número > 0 de la lista).");
            }
            else
            {
                errorProvider1.SetError(cbHabitacion, "");
            }
        }


        // ============================= VALIDACION CAMA =============================
        private void cbCama_Validating(object sender, CancelEventArgs e)
        {
            if (!TextoCoincideConLista(cbCama, _maestroCama)
                || !TextoEsEnteroPositivo(cbCama.Text, out var nroCama))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbCama, "Seleccioná una cama válida (número > 0 de la lista).");
            }
            else
            {
                errorProvider1.SetError(cbCama, "");
            }
        }


        // ============================= VALIDACION FECHA INICIO =============================
        private void dtpFechaInicio_Validating(object sender, CancelEventArgs e)
        {
            var ahora = DateTime.Now;
            if (dtpFechaInicio.Value > ahora)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha/hora de inicio no puede ser futura.");
                return;
            }

            var limiteAntiguedad = ahora.AddYears(-10);
            if (dtpFechaInicio.Value < limiteAntiguedad)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaInicio, "La fecha de inicio es demasiado antigua (máx. 10 años).");
                return;
            }

            errorProvider1.SetError(dtpFechaInicio, "");
        }


        // ============================= VALIDACION FECHA FIN =============================
        private void dtpFechaFin_Validating(object sender, CancelEventArgs e)
        {
            if (!dtpFechaFin.Checked)
            {
                errorProvider1.SetError(dtpFechaFin, "");
                return;
            }

            var ahora = DateTime.Now;

            if (dtpFechaFin.Value < dtpFechaInicio.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpFechaFin, "La fecha de egreso debe ser posterior al inicio.");
                return;
            }

            errorProvider1.SetError(dtpFechaFin, "");
        }

        // ============================= VALIDACION OBSERVACIONES =============================
        private void txtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            var texto = txtObservaciones.Text?.Trim() ?? "";
            if (texto.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Máximo 300 caracteres.");
                return;
            }

            errorProvider1.SetError(txtObservaciones, "");
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
        // Valida todo el formulario y simula guardar (aquí iría la lógica real)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validaciones básicas: que haya selección
            if (cbPaciente.SelectedValue == null || cbMedico.SelectedValue == null || cbProcedimiento.SelectedValue == null)
            {
                MessageBox.Show("Falta seleccionar Paciente, Médico o Procedimiento.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idPaciente = (int)cbPaciente.SelectedValue;
            int idMedico = (int)cbMedico.SelectedValue;
            int idProcedimiento = (int)cbProcedimiento.SelectedValue;

            int nroHabitacion = int.Parse(cbHabitacion.Text);
            int nroCama = int.Parse(cbCama.Text);

            var dto = new InternacionDto
            {
                Id_paciente = idPaciente,
                Id_medico = idMedico,          
                Id_procedimiento = idProcedimiento,
                Nro_habitacion = nroHabitacion,
                Id_cama = nroCama,
                Fecha_ingreso = dtpFechaInicio.Value,
                Fecha_egreso = dtpFechaFin.Checked ? (DateTime?)dtpFechaFin.Value : null,
                Diagnostico = txtObservaciones.Text?.Trim()
            };

            // Llamá a tu capa de negocio
            var servicio = new InternacionService(); // o el que uses
            servicio.altaInternacion(dto);

            MessageBox.Show("Internación registrada con éxito.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Limpia todos los campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetFormulario();
        }

        // Notifica al menú administrativo que se solicitó cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

        // ============================= RESETEA TODA LA INFORMACION DEL FORMULARIO =============================
        private void ResetFormulario()
        {
            _actualizandoInterno = true;
            try
            {
                // Textos
                txtPiso.Clear();
                txtObservaciones.Clear();

                // Fechas
                dtpFechaInicio.Value = DateTime.Now; 
                dtpFechaFin.Value = DateTime.Today;
                dtpFechaFin.Checked = false;
                dtpFechaFin.CustomFormat = " "; // que quede visualmente vacío

                // Paciente / Médico / Procedimiento
                LimpiarSeleccionCombo(cbPaciente);
                LimpiarSeleccionCombo(cbMedico);
                LimpiarSeleccionCombo(cbProcedimiento);

                // Habitación y Cama (vaciar, limpiar y deshabilitar en cascada)
                VaciarCombo(cbHabitacion);
                VaciarCombo(cbCama);

                errorProvider1.Clear();

                // Refrescar habilitación por dependencias
                SincronizarHabilitacionControles();
            }
            finally
            {
                _actualizandoInterno = false;
            }
        }

        // ============================= HELPERS DE COMBOBOX =============================
        // Limpia la selección de un combo
        private void LimpiarSeleccionCombo(ComboBox cb)
        {
            cb.SelectedIndex = -1;
            cb.Text = string.Empty;
        }

        // Vacía un combo (sin tocar el maestro)
        private void VaciarCombo(ComboBox cb)
        {
            cb.DataSource = null;
            cb.Items.Clear();
            cb.SelectedIndex = -1;
            cb.Text = string.Empty;
        }

        // ============================= SINCRONIZACIÓN DE HABILITACIÓN DE CONTROLES =============================
        private void SincronizarHabilitacionControles()
        {
            bool pisoValido = !string.IsNullOrWhiteSpace(txtPiso.Text)
                              && int.TryParse(txtPiso.Text, out var p) && p > 0;

            cbHabitacion.Enabled = pisoValido;

            bool habitacionValida = pisoValido
                                    && !string.IsNullOrWhiteSpace(cbHabitacion.Text)
                                    && int.TryParse(cbHabitacion.Text, out var h) && h > 0;

            cbCama.Enabled = habitacionValida;

            if (!pisoValido)
            {
                VaciarCombo(cbHabitacion);
                VaciarCombo(cbCama);
            }
            else if (!habitacionValida)
            {
                VaciarCombo(cbCama);
            }
        }

        // ============================= CONFIGURACIONES ESPECIALES DE CONTROLES DE FECHA =============================
        // Fecha inicio con hora (sin calendario desplegable)
        private void ConfigurarFechaInicioConHora()
        {
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm"; 
            dtpFechaInicio.ShowUpDown = true;                 
        }

        // Fecha fin opcional (check para "sin fecha")
        private void ConfigurarFechaFinNull()
        {
            dtpFechaFin.ShowCheckBox = true;                 
            dtpFechaFin.Checked = false;                     
            dtpFechaFin.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.CustomFormat = " ";                  
            dtpFechaFin.ValueChanged -= dtpFechaFin_ValueChanged;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
        }
        
        // Evento para actualizar el formato cuando se tilda/desmarca el check
        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFin.Checked)
                dtpFechaFin.CustomFormat = "dd/MM/yyyy HH:mm"; // o el formato que uses
            else
                dtpFechaFin.CustomFormat = " ";                // sin fecha visible
        }

    }
}
