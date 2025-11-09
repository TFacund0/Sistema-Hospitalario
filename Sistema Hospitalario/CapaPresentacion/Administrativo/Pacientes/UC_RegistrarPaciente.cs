using Sistema_Hospitalario.CapaNegocio;
using Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_RegistrarPaciente : UserControl
    {
        // Servicio para interactuar con la capa de negocio de paciente y estado
        private readonly PacienteService _pacienteService = new PacienteService();
        private readonly EstadoPacienteService _estadoService = new EstadoPacienteService();

        // Evento para notificar al menuAdministrativo que se solicitó cancelar el registro
        public event EventHandler CancelarRegistroSolicitado;

        // ============================= CONSTRUCTOR DEL UC REGISTRAR PACIENTE =============================
        public UC_RegistrarPaciente()
        {
            InitializeComponent();

            SelectEventosPaciente();
            CargarEstadosEnCombo();
        }

        // ============================= COMBOBOX ESTADO INICIAL =============================
        // Seleccionar el ComboBox al hacer foco o click
        private void SelectEventosPaciente()
        {
            // Abrir automáticamente al ganar foco o click
            cbEstadoInicial.Enter += (s, e) => cbEstadoInicial.DroppedDown = true;
            cbEstadoInicial.MouseDown += (s, e) => cbEstadoInicial.DroppedDown = true;
        }

        // Cargar los estados de paciente en el ComboBox
        private void CargarEstadosEnCombo()
        {
            var estados = _estadoService.ListarEstados();

            var lista = new List<EstadoPacienteDto>
        {
            new EstadoPacienteDto { Id = 0, Nombre = "— Seleccioná —" }
        };
            lista.AddRange(estados);

            cbEstadoInicial.DataSource = lista;
            cbEstadoInicial.DisplayMember = nameof(EstadoPacienteDto.Nombre);
            cbEstadoInicial.ValueMember = nameof(EstadoPacienteDto.Id);
            cbEstadoInicial.SelectedIndex = 0;
        }


        // ============================= VALIDACIONES DE CAMPOS PACIENTE =============================

        // ========== VALIDACIÓN NOMBRE ==========
        private void TxtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "El nombre es obligatorio.");
            }
            else if (txtNombre.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNombre, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtNombre, "");
            }
        }

        // ========== VALIDACIÓN APELLIDO ==========
        private void TxtApellido_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "El apellido es obligatorio.");
            }
            else if (txtApellido.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApellido, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtApellido, "");
            }
        }

        // ========== VALIDACIÓN DIRECCIÓN ==========
        private void TxtDireccion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDireccion, "La dirección es obligatoria.");
            }
            else if (txtDireccion.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDireccion, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtDireccion, "");
            }
        }

        // ========== VALIDACIÓN FECHA DE NACIMIENTO ==========
        private void DtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpNacimiento.Format = DateTimePickerFormat.Short; 
            dtpNacimiento.MaxDate = DateTime.Today;            
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-110); 

            if (dtpNacimiento.Value > DateTime.Today)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser futura.");
            }
            else if (dtpNacimiento.Value < DateTime.Today.AddYears(-120))
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser mayor a 120 años.");
            }
            else
            {
                errorProvider1.SetError(dtpNacimiento, "");
            }
        }

        // ========== VALIDACIÓN DNI ==========
        private void TxtDni_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text) || !int.TryParse(txtDni.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI es obligatorio y númerico.");
            }
            else if (txtDni.Text.Length > 8)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "Máximo 15 caracteres.");
            }
            else if (int.TryParse(txtDni.Text, out int dni) && dni <= 0 )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI debe ser un número positivo.");
            }
            else if (txtDni.Text.Length < 7)
            {
                e.Cancel= true;
                errorProvider1.SetError(txtDni, "El DNI debe ser de al menos 7 digitos.");
            }
            else
            {
                errorProvider1.SetError(txtDni, "");
            }
        }

        // ========== VALIDACIÓN TELÉFONO ==========
        private void TxtTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !int.TryParse(txtTelefono.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "El teléfono es obligatorio y númerico.");
            }
            else if (txtTelefono.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefono, "Máximo 15 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtTelefono, "");
            }
        }

        // ========= VALIDACIÓN CORREO ==========
        private void TxtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "El correo es obligatorio.");
            }
            else if (txtEmail.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Máximo 100 caracteres.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text,
                     @"^[^@\s]+@[^@\s]+\.[^@\s]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Formato de correo inválido.");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        // ========== VALIDACIÓN OBSERVACIONES ==========
        private void TxtObservaciones_Validating(object sender, CancelEventArgs e)
        {
            if (txtObservaciones.Text.Length > 200)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObservaciones, "Máximo 200 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObservaciones, "");
            }
        }

        //============================= RESTRICCIONES DE TECLADO =============================

        // Permitir solo letras, espacios y teclas de control
        private void SoloLetras(object s, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        // Permitir solo números y teclas de control
        private void SoloNumeros(object s, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        // ============================ BOTÓN GUARDAR =============================
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var dto = new PacienteAltaDto
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Dni = int.TryParse(txtDni.Text.Trim(), out int dni) ? dni : 0,
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                FechaNacimiento = dtpNacimiento.Value,
                Email = txtEmail.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim(),
                EstadoInicial = cbEstadoInicial.Text.Trim(),
            };

            var (Ok, IdGenerado, Error) = _pacienteService.Alta(dto);

            if (Ok)
            {
                MessageBox.Show($"Paciente registrado con éxito. ID: {IdGenerado}", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLimpiar_Click(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show(Error, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================ BOTÓN LIMPIAR =============================
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtDni.Clear();
            txtDireccion.Clear();            
            txtDni.Clear();
            txtEmail.Clear();
            txtObservaciones.Clear();
            dtpNacimiento.Value = DateTime.Today;
            cbEstadoInicial.SelectedIndex = 0;
            errorProvider1.Clear();
        }

        // ============================ BOTÓN CANCELAR =============================
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
