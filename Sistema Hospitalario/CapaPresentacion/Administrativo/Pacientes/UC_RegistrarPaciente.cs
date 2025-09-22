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

using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;
using Sistema_Hospitalario.CapaNegocio.Servicios;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo.Pacientes
{
    public partial class UC_RegistrarPaciente : UserControl
    {
        private readonly PacienteService _pacienteService = new PacienteService();
        public event EventHandler CancelarRegistroSolicitado;

        public UC_RegistrarPaciente()
        {
            InitializeComponent();
        }

    // ============================= VALIDACIONES DE CAMPOS =============================

    private void txtNombre_Validating(object sender, CancelEventArgs e)
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

        private void txtDireccion_Validating(object sender, CancelEventArgs e)
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

        private void txtApellido_Validating(object sender, CancelEventArgs e)
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

        private void txtInicial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInicial.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "El valor inicial es obligatorio.");
            }
            else if (txtInicial.Text.Length > 10)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInicial, "Máximo 10 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtInicial, "");
            }
        }


        private void txtObraSocial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtObraSocial.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObraSocial, "La obra social es obligatoria.");
            }
            else if (txtObraSocial.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtObraSocial, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(txtObraSocial, "");
            }
        }

        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpNacimiento.Format = DateTimePickerFormat.Short; 
            dtpNacimiento.MaxDate = DateTime.Today;            
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-120); 

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

        private void txtAfiliado_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAfiliado.Text) || !int.TryParse(txtAfiliado.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAfiliado, "El número de afiliado es obligatorio.");
            }
            else if (txtAfiliado.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAfiliado, "Máximo 20 caracteres.");
            }
            else if (int.TryParse(txtAfiliado.Text, out int nro) && nro <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAfiliado, "El número de afiliado debe ser un número positivo.");
            }
            else
            {
                errorProvider1.SetError(txtAfiliado, "");
            }
        }

        private void txtDni_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text) || !int.TryParse(txtDni.Text, out int _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI es obligatorio y númerico.");
            }
            else if (txtDni.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "Máximo 15 caracteres.");
            }
            else if (int.TryParse(txtDni.Text, out int dni) && dni <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDni, "El DNI debe ser un número positivo.");
            }
            else
            {
                errorProvider1.SetError(txtDni, "");
            }
        }

        private void txtTelefono_Validating(object sender, CancelEventArgs e)
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

        private void txtObservaciones_Validating(object sender, CancelEventArgs e)
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
        private void SoloLetras(object s, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void SoloNumeros(object s, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNombre_KeyPress(object s, KeyPressEventArgs e) => SoloLetras(s, e);
        private void txtApellido_KeyPress(object s, KeyPressEventArgs e) => SoloLetras(s, e);
        private void txtTelefono_KeyPress(object s, KeyPressEventArgs e) => SoloNumeros(s, e);
        private void txtAfiliado_KeyPress(object s, KeyPressEventArgs e) => SoloNumeros(s, e);
        private void txtDni_KeyPress(object s, KeyPressEventArgs e) => SoloNumeros(s, e);


        //============================= BOTONES =============================
        private bool ValidarFormulario()
        {
            errorProvider1.Clear();
            bool ok = this.ValidateChildren(ValidationConstraints.Enabled);

            if (!string.IsNullOrWhiteSpace(txtObraSocial.Text) && string.IsNullOrWhiteSpace(txtAfiliado.Text))
            {
                errorProvider1.SetError(txtAfiliado, "Si cargás Obra Social, el N° de afiliado es obligatorio.");
                ok = false;
            }

            if (dtpNacimiento.Value > DateTime.Today)
            {
                errorProvider1.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser futura.");
                ok = false;
            }

            if (!ok)
            {
                foreach (Control c in this.Controls)
                {
                    if (!string.IsNullOrEmpty(errorProvider1.GetError(c)))
                    { c.Focus(); break; }
                }
            }
            return ok;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
            {
                MessageBox.Show("Por favor, corregí los errores antes de guardar.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dto = new CapaNegocio.DTOs.PacienteAltaDto
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Dni = int.TryParse(txtDni.Text.Trim(), out int dni) ? dni : 0,
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                ObraSocial = txtObraSocial.Text.Trim(),
                NumeroAfiliado = int.TryParse(txtAfiliado.Text.Trim(), out int nro) ? nro : 0,
                FechaNacimiento = dtpNacimiento.Value,
                Observaciones = txtObservaciones.Text.Trim(),
                EstadoInicial = txtInicial.Text.Trim()
            };

            var r = _pacienteService.Alta(dto);

            if (r.Ok)
            {
                MessageBox.Show($"Paciente registrado con éxito. ID: {r.IdGenerado}", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpiar_Click(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show(r.Error, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtDni.Clear();
            txtDireccion.Clear();
            txtObraSocial.Clear();
            txtAfiliado.Clear();
            txtDni.Clear();
            txtInicial.Clear();
            txtObservaciones.Clear();
            dtpNacimiento.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
