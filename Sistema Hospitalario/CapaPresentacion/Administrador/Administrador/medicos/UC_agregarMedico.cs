using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.medicos
{
    public partial class UC_agregarMedico : UserControl
    {
        public UC_agregarMedico()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear el cuadro de diálogo
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Configurar filtros (para fotos y PDF)
            openFileDialog.Filter = "Archivos permitidos|*.docx;*.pdf";

            // Mostrar el diálogo y comprobar si el usuario seleccionó un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Ruta completa del archivo seleccionado
                string rutaArchivo = openFileDialog.FileName;

                // Mostrar la ruta en un TextBox
                TBRUTAARCHIVO.Text = rutaArchivo;

                // (Opcional) Copiar el archivo a una carpeta del sistema
                //string destino = Path.Combine(@"C:\Hospital\Medicos\", Path.GetFileName(rutaArchivo));
                //File.Copy(rutaArchivo, destino, true);

                MessageBox.Show("Archivo guardado");
            }
        }
        private void TBNOMBRE_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBNOMBRE.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBNOMBRE, "El nombre es obligatorio.");
            }
            else if (TBNOMBRE.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBNOMBRE, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBNOMBRE, "");
            }
        }

        private void TBAPELLIDO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBAPELLIDO.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBAPELLIDO, "El apellido es obligatorio.");
            }
            else if (TBAPELLIDO.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBAPELLIDO, "Máximo 50 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBAPELLIDO, "");
            }
        }

        private void TBDNI_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDNI.Text) || !long.TryParse(TBDNI.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "El DNI es obligatorio y numérico.");
            }
            else if (TBDNI.Text.Length < 7 || TBDNI.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDNI, "El DNI debe tener entre 7 y 15 dígitos.");
            }
            else
            {
                errorProvider1.SetError(TBDNI, "");
            }
        }

        private void TBDIRECCION_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBDIRECCION.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDIRECCION, "La dirección es obligatoria.");
            }
            else if (TBDIRECCION.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBDIRECCION, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBDIRECCION, "");
            }
        }

        private void dtpNacimiento_Validating(object sender, CancelEventArgs e)
        {
            dtpNacimiento.MaxDate = DateTime.Today;
            dtpNacimiento.MinDate = DateTime.Today.AddYears(-120);

            if (dtpNacimiento.Value.Date > DateTime.Today)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha no puede ser futura.");
            }
            else if (dtpNacimiento.Value.Date < DateTime.Today.AddYears(-120))
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpNacimiento, "La fecha no puede ser mayor a 120 años atrás.");
            }
            else
            {
                errorProvider1.SetError(dtpNacimiento, "");
            }
        }

        private void TBMATRICULA_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBMATRICULA.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBMATRICULA, "La matrícula es obligatoria.");
            }
            else if (TBMATRICULA.Text.Length > 20)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBMATRICULA, "Máximo 20 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBMATRICULA, "");
            }
        }

        private void TBTELEFONO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBTELEFONO.Text) || !long.TryParse(TBTELEFONO.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBTELEFONO, "El teléfono es obligatorio y numérico.");
            }
            else if (TBTELEFONO.Text.Length > 15)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBTELEFONO, "Máximo 15 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBTELEFONO, "");
            }
        }

        private void TBCORREO_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCORREO.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "El correo electrónico es obligatorio.");
            }
            else if (!Regex.IsMatch(TBCORREO.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "Formato de correo inválido.");
            }
            else if (TBCORREO.Text.Length > 100)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBCORREO, "Máximo 100 caracteres.");
            }
            else
            {
                errorProvider1.SetError(TBCORREO, "");
            }
        }

        private void TBRUTAARCHIVO_Validating(object sender, CancelEventArgs e)
        {

        }

        // ============================= BOTONES =============================

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                MessageBox.Show("Usuario registrado con éxito.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Aquí va el guardado real en BD
            }
            else
            {
                MessageBox.Show("Corrija los errores antes de guardar.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            TBNOMBRE.Clear();
            TBAPELLIDO.Clear();
            TBDNI.Clear();
            TBDIRECCION.Clear();
            TBMATRICULA.Clear();
            TBTELEFONO.Clear();
            TBCORREO.Clear();
            TBRUTAARCHIVO.Clear();
            dtpNacimiento.Value = DateTime.Today;
            errorProvider1.Clear();
        }
    }
}
