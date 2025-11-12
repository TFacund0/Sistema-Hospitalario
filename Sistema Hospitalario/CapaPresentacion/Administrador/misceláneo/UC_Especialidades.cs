using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sistema_Hospitalario.CapaNegocio.Servicios.EspecialidadService;


namespace Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo
{
    public partial class UC_Especialidades : UserControl
    {
        // ===================== INSTANCIAS DE SERVICIOS =====================
        private readonly EspecialidadService _service; 

        public UC_Especialidades()
        {
            InitializeComponent();
            _service = new EspecialidadService();
            CargarEspecialidades();
        }

        // ===================== MÉTODOS AUXILIARES =====================
        // Carga de especialidades en el DataGridView
        private void CargarEspecialidades()
        {
            dgvEspecialidades.DataSource = _service.ObtenerEspecialidades();
            dgvEspecialidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEspecialidades.RowHeadersVisible = false;
            dgvEspecialidades.BackgroundColor = Color.White;
            dgvEspecialidades.BorderStyle = BorderStyle.None;
            dgvEspecialidades.EnableHeadersVisualStyles = false;

            dgvEspecialidades.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvEspecialidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEspecialidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvEspecialidades.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvEspecialidades.DefaultCellStyle.ForeColor = Color.Black;
            dgvEspecialidades.DefaultCellStyle.Font = new Font("Segoe UI", 9);

        }

        // ===================== EVENTOS DEL FORMULARIO =====================
        private void TBESPECIALIDAD_Validating(object sender, CancelEventArgs e)
        {
            string nombre = TBESPECIALIDAD.Text.Trim();

            // Validar vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBESPECIALIDAD, "El nombre de la especialidad es obligatorio.");
            }
            // Validar longitud
            else if (nombre.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBESPECIALIDAD, "Máximo 50 caracteres.");
            }
            // Validar solo letras y espacios (opcional)
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nombre, @"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBESPECIALIDAD, "Solo se permiten letras y espacios.");
            }
            // Validar duplicado en la base de datos
            else
            {
                try
                {
                    // Verificar si ya existe en la base de datos
                    var existe = _service.ObtenerEspecialidades()
                                   .Any(row => row.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

                    if (existe)
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(TBESPECIALIDAD, "Esa especialidad ya existe.");
                    }
                    else
                    {
                        errorProvider1.SetError(TBESPECIALIDAD, "");
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TBESPECIALIDAD, "Error al verificar en la base de datos: " + ex.Message);
                }
            }
        }

        // Botón Limpiar
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TBESPECIALIDAD.Clear();
        }

        // Botón Agregar
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                try
                {
                    string nombreEspecialidad = TBESPECIALIDAD.Text.Trim().ToLower();
                    _service.AgregarEspecialidad(nombreEspecialidad);
                    MessageBox.Show("Especialidad agregada con éxito.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TBESPECIALIDAD.Clear();
                    CargarEspecialidades(); // Recarga la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar especialidad: " + ex.Message);
                }
        }

        // Doble clic en la grilla para eliminar una especialidad
        private void DgvEspecialidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace doble clic en el encabezado o una fila vacía
            if (e.RowIndex < 0) return;

            // Obtener el nombre y el id de la especialidad seleccionada
            var row = dgvEspecialidades.Rows[e.RowIndex];
            string nombreEspecialidad = row.Cells["nombre"].Value.ToString();

            // Confirmar eliminación
            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar la especialidad '{nombreEspecialidad}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminar de la base de datos
                    _service.EliminarEspecialidad(nombreEspecialidad);

                    // Refrescar la grilla
                    CargarEspecialidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la especialidad: " + ex.Message);
                }
            }
        }
    }
}
