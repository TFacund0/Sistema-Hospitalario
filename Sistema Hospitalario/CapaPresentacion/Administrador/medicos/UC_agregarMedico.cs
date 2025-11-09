using Sistema_Hospitalario.CapaDatos;
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
using System.Windows.Controls;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.medicos
{
    public partial class UC_agregarMedico : System.Windows.Forms.UserControl
    {
        private static readonly MedicoService medicoService = new MedicoService();
        private readonly MedicoService _service = medicoService;
        public UC_agregarMedico()
        {
            InitializeComponent();
            CargarEspecialidades();
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

        private void BtnGuardar_Click_1(object sender, EventArgs e)
{
    try
    {
        if (!this.ValidateChildren())
        {
          MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        string nombre = TBNOMBRE.Text.Trim();
        string apellido = TBAPELLIDO.Text.Trim();
        string dni = TBDNI.Text.Trim();
        string direccion = TBDIRECCION.Text.Trim();
        string matricula = TBMATRICULA.Text.Trim();
        string correo = TBCORREO.Text.Trim();
        
        int idEspecialidad = (int)comboBox1.SelectedValue;  

        // Guardar en base de datos
        _service.AgregarMedico(nombre, apellido, dni, direccion, matricula, correo, idEspecialidad);

        MessageBox.Show("Médico registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Opcional: Limpiar el formulario después de guardar
        BtnLimpiar_Click_1(sender, e); 

    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al registrar el médico: " + ex.Message);
    }
}

        private void BtnLimpiar_Click_1(object sender, EventArgs e)
        {
            TBNOMBRE.Clear();
            TBAPELLIDO.Clear();
            TBDNI.Clear();
            TBDIRECCION.Clear();
            TBMATRICULA.Clear();
            TBCORREO.Clear();
            TBCORREO.Clear();
            TBDIRECCION.Clear();
            TBMATRICULA.Clear();

            errorProvider1.Clear();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            MenuModer parentForm = this.FindForm() as MenuModer;

            parentForm.AbrirUserControl(new UC_Medicos()); 
        }

        private void CargarEspecialidades()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var especialidades = db.especialidad
                    .Select(e => new { e.id_especialidad, e.nombre })
                    .ToList();

                comboBox1.DisplayMember = "nombre";
                comboBox1.ValueMember = "id_especialidad";
                comboBox1.DataSource = especialidades;

                comboBox1.SelectedIndex = 10;
            }
        }

    }
}
