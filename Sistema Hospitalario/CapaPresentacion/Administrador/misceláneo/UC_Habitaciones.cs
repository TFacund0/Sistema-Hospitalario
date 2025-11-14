using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO; 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo
{
    public partial class UC_Habitaciones : UserControl
    {
        // ===================== INSTANCIAS DE SERVICIOS =====================
        private readonly HabitacionService _service;
        public UC_Habitaciones()
        {
            InitializeComponent();
            _service = new HabitacionService();
            CargarHabitaciones();
            CargarComboBox();
        }

        // ===================== MÉTODOS AUXILIARES =====================
        // Carga de habitaciones en el DataGridView
        private void CargarHabitaciones()
        {
            dgvHabitaciones.DataSource = _service.ObtenerHabitaciones();
            dgvHabitaciones.Columns["IdTipoHabitacion"].Visible = false;
            dgvHabitaciones.Columns["TotalCamas"].Visible = false;
            dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHabitaciones.RowHeadersVisible = false;
            dgvHabitaciones.BackgroundColor = Color.White;
            dgvHabitaciones.BorderStyle = BorderStyle.None;
            dgvHabitaciones.EnableHeadersVisualStyles = false;

            dgvHabitaciones.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvHabitaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHabitaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvHabitaciones.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvHabitaciones.DefaultCellStyle.ForeColor = Color.Black;
            dgvHabitaciones.DefaultCellStyle.Font = new Font("Segoe UI", 9);

        }

        // Carga de tipos de habitación en el ComboBox
        private void CargarComboBox()
        {
            List<TiposHabitacionDTO> listaHabitacion = _service.ListarTiposHabitacion();

            comboBox1.DataSource = listaHabitacion;
            comboBox1.DisplayMember = "nombre";  
            comboBox1.ValueMember = "IdTipoHabitacion";
            
        }

        // ===================== EVENTOS DEL FORMULARIO =====================
        // Validación del campo de piso de habitación
        private void TBPISOHABITACION_Validating(object sender, CancelEventArgs e)
        {
            // Intentar convertir el texto a int de forma segura
            if (!int.TryParse(TBPISOHABITACION.Text.Trim(), out int nroPiso))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPISOHABITACION, "Debe ingresar un número válido para el piso.");
                return;
            }

            // Validar vacío o negativo
            if (nroPiso < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPISOHABITACION, "El número de piso no puede ser negativo.");
            }
            // Validar longitud (máximo 50 caracteres en el texto)
            else if (TBPISOHABITACION.Text.Trim().Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPISOHABITACION, "Máximo 50 caracteres.");
            }
            // Validar que solo sean números (ya validado con TryParse, pero se mantiene la estructura)
            else if (!System.Text.RegularExpressions.Regex.IsMatch(TBPISOHABITACION.Text.Trim(), @"^\d+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBPISOHABITACION, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(TBPISOHABITACION, "");
            }
        }

        // Botón Limpiar
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TBPISOHABITACION.Clear();
        }

        // Botón Agregar
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                try
                {
                    int IdTipoHabitacion = (int)comboBox1.SelectedValue;
                    if (int.TryParse(TBPISOHABITACION.Text.Trim(), out int nroPiso)) { 
                    _service.AgregarHabitacion(nroPiso, IdTipoHabitacion);
                    MessageBox.Show("habitacion agregada con éxito.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                     TBPISOHABITACION.Clear();
                    CargarHabitaciones();
                    }// Recarga la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar habitacion: " + ex.Message);
                }
        }
        
        // Doble clic en una fila para eliminar la habitación
        private void DgvHabitaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace doble clic en el encabezado o una fila vacía
            if (e.RowIndex < 0) return;

            // Obtener el nombre y el id de la especialidad seleccionada
            var row = dgvHabitaciones.Rows[e.RowIndex];
            int nroPiso = Convert.ToInt32(row.Cells["NroPiso"].Value);
            int nroHabitacion = Convert.ToInt32(row.Cells["NroHabitacion"].Value);

            // Confirmar eliminación
            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar la habitacion {nroHabitacion} del piso {nroPiso}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminar de la base de datos
                    _service.EliminarHabitacion(nroPiso, nroHabitacion);

                    // Refrescar la grilla
                    CargarHabitaciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la habitacion: " + ex.Message);
                }
            }
        }

    }
}
