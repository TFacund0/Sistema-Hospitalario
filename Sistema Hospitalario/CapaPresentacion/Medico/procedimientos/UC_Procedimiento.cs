using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico.procedimientos
{
    public partial class UC_Procedimiento : UserControl
    {
        public UC_Procedimiento()
        {
            InitializeComponent();
        }

        private void TBDNI_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDNI.Text) && string.IsNullOrWhiteSpace(TBAFILIADO.Text))
            { // si ambos están vacíos 
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "Ingrese DNI o Nro de Afiliado.");
               
            }
            else if (!string.IsNullOrWhiteSpace(TBDNI.Text))
            { // si el DNI no está vacío
                if (!int.TryParse(TBDNI.Text, out _) || TBDNI.Text.Length > 15) 
                    // si no es numérico o es muy largo
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TBDNI, "DNI numérico (máx. 15).");
                }
                else // si es válido
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
            { // si ambos están vacíos
                e.Cancel = true;
                errorProvider1.SetError(TBAFILIADO, "Ingrese DNI o Nro de Afiliado.");
            }
            else if (!string.IsNullOrWhiteSpace(TBAFILIADO.Text))
            { // si el Nro de Afiliado no está vacío
                if (!int.TryParse(TBAFILIADO.Text, out _) || TBAFILIADO.Text.Length > 20)
                { // si no es numérico o es muy largo

                    e.Cancel = true;
                    errorProvider1.SetError(TBAFILIADO, "Número de afiliado numérico (máx. 20).");
                }
                else // si es válido
                {
                    errorProvider1.SetError(TBAFILIADO, "");
                }
            }
            else
            {
                errorProvider1.SetError(TBAFILIADO, "");
            }
        }
        private void TBObservaciones_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBObservaciones.Text)) // si no se ingresaron observaciones
            {
                e.Cancel = true;
                errorProvider1.SetError(TBObservaciones, "Observaciones es obligatorio.");
            }
            else if (TBObservaciones.Text.Length > 300)
            { // si es muy largo
                e.Cancel = true;
                errorProvider1.SetError(TBObservaciones, "Máximo 300 caracteres.");
            }
            else // si es válido
            {
                errorProvider1.SetError(TBObservaciones, "");
            }
        }

        private void TBTYR_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBTYR.Text))
            { // si no se ingresó tratamiento 
                e.Cancel = true;
                errorProvider1.SetError(TBTYR, "El tratamiento es obligatorio.");
            }
            else if (TBTYR.Text.Length > 300) // si es muy largo
            {
                e.Cancel = true;
                errorProvider1.SetError(TBTYR, "Máximo 300 caracteres.");
            }
            else // si es válido
            {
                errorProvider1.SetError(TBTYR, "");
            }
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Today)
            { // si la fecha es futura
                e.Cancel = true;
                errorProvider1.SetError(dateTimePicker1, "La fecha de la consulta no puede ser futura.");
            }
            else // si es válida
            {
                errorProvider1.SetError(dateTimePicker1, "");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            { // si todos los campos son válidos
                MessageBox.Show("Consulta guardada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { // si hay errores de validación
                MessageBox.Show("Corrija los errores antes de guardar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        { // limpiar todos los campos y errores
            TBDNI.Clear();
            TBAFILIADO.Clear();
            TBObservaciones.Clear();
            TBTYR.Clear();
            dateTimePicker1.Value = DateTime.Today;
            errorProvider1.Clear();
        }

    }
}
