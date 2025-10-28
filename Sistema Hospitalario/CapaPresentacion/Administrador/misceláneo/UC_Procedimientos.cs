using Sistema_Hospitalario.CapaDatos.ModerRepos;
using Sistema_Hospitalario.CapaNegocio.Servicios.moder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo
{
    public partial class UC_Procedimientos : UserControl
    {
        private readonly ProcedimientoService _service;
        public UC_Procedimientos()
        {
            InitializeComponent();
            _service = new ProcedimientoService(new ProcedimientoRepository());
            CargarProcedimientos();
        }

        private void CargarProcedimientos()
        {
            dgvProcedimientos.DataSource = _service.ObtenerProcedimientos();
            dgvProcedimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProcedimientos.RowHeadersVisible = false;
            dgvProcedimientos.BackgroundColor = Color.White;
            dgvProcedimientos.BorderStyle = BorderStyle.None;
            dgvProcedimientos.EnableHeadersVisualStyles = false;

            dgvProcedimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvProcedimientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProcedimientos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvProcedimientos.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvProcedimientos.DefaultCellStyle.ForeColor = Color.Black;
            dgvProcedimientos.DefaultCellStyle.Font = new Font("Segoe UI", 9);

        }

        private void TBPROCEDIMIENTO_Validating(object sender, CancelEventArgs e)
        {
            string nombre = TBPROCEDIMIENTO.Text.Trim();

            // Validar vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPROCEDIMIENTO, "El nombre del procedimiento es obligatorio.");
            }
            // Validar longitud
            else if (nombre.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPROCEDIMIENTO, "Máximo 50 caracteres.");
            }
            // Validar solo letras y espacios (opcional)
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nombre, @"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPROCEDIMIENTO, "Solo se permiten letras y espacios.");
            }
            // Validar duplicado en la base de datos
            else
            {
                try
                {
                    // Verificar si ya existe en la base de datos
                    var existe = _service.ObtenerProcedimientos()
                                   .Any(row => row.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

                    if (existe)
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(TBPROCEDIMIENTO, "Esa especialidad ya existe.");
                    }
                    else
                    {
                        errorProvider1.SetError(TBPROCEDIMIENTO, "");
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TBPROCEDIMIENTO, "Error al verificar en la base de datos: " + ex.Message);
                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TBPROCEDIMIENTO.Clear();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                try
                {
                    string nombreProcedimiento = TBPROCEDIMIENTO.Text.Trim().ToLower();
                    _service.AgregarProcedimiento(nombreProcedimiento);
                    MessageBox.Show("Especialidad agregada con éxito.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TBPROCEDIMIENTO.Clear();
                    CargarProcedimientos(); // Recarga la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar especialidad: " + ex.Message);
                }
        }
        private void DgvProcedimientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace doble clic en el encabezado o una fila vacía
            if (e.RowIndex < 0) return;

            // Obtener el nombre y el id de la especialidad seleccionada
            var row = dgvProcedimientos.Rows[e.RowIndex];
            string nombreProcedimiento = row.Cells["nombre"].Value.ToString();

            // Confirmar eliminación
            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar la especialidad '{nombreProcedimiento}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminar de la base de datos
                    _service.EliminarProcedimiento(nombreProcedimiento);

                    // Refrescar el dgv
                    CargarProcedimientos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la especialidad: " + ex.Message);
                }
            }
        }

    
    }
}
