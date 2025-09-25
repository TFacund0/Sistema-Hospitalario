using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico.Pacientes
{
    public partial class ConsultaMedica : UserControl
    {
        public ConsultaMedica()
        {
            InitializeComponent();
        }

        // ============================= VALIDACIONES =============================

        private void TBDNI_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDNI.Text) && string.IsNullOrWhiteSpace(TBAFILIADO.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "Ingrese DNI o Nro de Afiliado.");
            }
            else if (!string.IsNullOrWhiteSpace(TBDNI.Text))
            {
                if (!int.TryParse(TBDNI.Text, out _) || TBDNI.Text.Length > 15)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TBDNI, "DNI numérico (máx. 15).");
                }
                else
                {
                    errorProvider1.SetError(TBDNI, "");
                }
            }
            else
            {
                errorProvider1.SetError(TBDNI, "");
            }
        }

        private void TBAFILIADO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDNI.Text) && string.IsNullOrWhiteSpace(TBAFILIADO.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBAFILIADO, "Ingrese DNI o Nro de Afiliado.");
            }
            else if (!string.IsNullOrWhiteSpace(TBAFILIADO.Text))
            {
                if (!int.TryParse(TBAFILIADO.Text, out _) || TBAFILIADO.Text.Length > 20)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TBAFILIADO, "Número de afiliado numérico (máx. 20).");
                }
                else
                {
                    errorProvider1.SetError(TBAFILIADO, "");
                }
            }
            else
            {
                errorProvider1.SetError(TBAFILIADO, "");
            }
        }

        private void TBMOTIVO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBMOTIVO.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBMOTIVO, "El motivo es obligatorio.");
            }
            else if (TBMOTIVO.Text.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBMOTIVO, "Máximo 300 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBMOTIVO, "");
            }
        }

        private void TBDX_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDX.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDX, "El diagnóstico es obligatorio.");
            }
            else if (TBDX.Text.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDX, "Máximo 300 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBDX, "");
            }
        }

        private void TBTYR_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBTYR.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBTYR, "El tratamiento es obligatorio.");
            }
            else if (TBTYR.Text.Length > 300)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBTYR, "Máximo 300 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBTYR, "");
            }
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Today)
            {
             e.Cancel = true;
             errorProvider1.SetError(dateTimePicker1, "La fecha de la consulta no puede ser futura.");
            }
            else
            {
                errorProvider1.SetError(dateTimePicker1, "");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                MessageBox.Show("Consulta guardada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Corrija los errores antes de guardar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            TBDNI.Clear();
            TBAFILIADO.Clear();
            TBMOTIVO.Clear();
            TBDX.Clear();
            TBTYR.Clear();
            dateTimePicker1.Value = DateTime.Today;
            errorProvider1.Clear();
        }

        public event EventHandler CancelarRegistroSolicitado;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegistroSolicitado?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
