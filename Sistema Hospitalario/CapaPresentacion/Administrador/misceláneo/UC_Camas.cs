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

namespace Sistema_Hospitalario.CapaPresentacion.Administrador.misceláneo
{
    public partial class UC_Camas : UserControl
    {
        private readonly CamaService _service;
        public UC_Camas()
        {
            InitializeComponent();
            _service = new CamaService(new CamaRepository());
            CargarCamas();
        }

        private void CargarCamas()
        {
            dgvCamas.DataSource = _service.ObtenerCamas();
            dgvCamas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCamas.RowHeadersVisible = false;
            dgvCamas.BackgroundColor = Color.White;
            dgvCamas.BorderStyle = BorderStyle.None;
            dgvCamas.EnableHeadersVisualStyles = false;

            dgvCamas.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvCamas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCamas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvCamas.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvCamas.DefaultCellStyle.ForeColor = Color.Black;
            dgvCamas.DefaultCellStyle.Font = new Font("Segoe UI", 9);

        }

        private void TBHabitacionCama_Validating(object sender, CancelEventArgs e)
        {
            // Intentar convertir el texto a int de forma segura
            int nroPiso;
            if (!int.TryParse(TBHabitacionCama.Text.Trim(), out nroPiso))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBHabitacionCama, "Debe ingresar un número válido para la habitacion.");
                return;
            }

            // Validar vacío o negativo
            if (nroPiso < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBHabitacionCama, "El número de habitacion no puede ser negativo.");
            }
            // Validar longitud (máximo 50 caracteres en el texto)
            else if (TBHabitacionCama.Text.Trim().Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(TBHabitacionCama, "Máximo 50 caracteres.");
            }
            // Validar que solo sean números (ya validado con TryParse, pero se mantiene la estructura)
            else if (!System.Text.RegularExpressions.Regex.IsMatch(TBHabitacionCama.Text.Trim(), @"^\d+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(TBHabitacionCama, "Solo se permiten números.");
            }
            else
            {
                errorProvider1.SetError(TBHabitacionCama, "");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            TBHabitacionCama.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                try
                {
                    int nroPiso;
                    if (int.TryParse(TBHabitacionCama.Text.Trim(), out nroPiso))
                    {
                        _service.AgregarCama(nroPiso);
                        MessageBox.Show("habitacion agregada con éxito.", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TBHabitacionCama.Clear();
                        CargarCamas();
                    }// Recarga la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar habitacion: " + ex.Message);
                }
        }

        private void dgvCamas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita que se ejecute si se hace doble clic en el encabezado o una fila vacía
            if (e.RowIndex < 0) return;

            // Obtener el nombre y el id de la especialidad seleccionada
            var row = dgvCamas.Rows[e.RowIndex];
            int nroCama = Convert.ToInt32(row.Cells["NroCama"].Value);
            int nroHabitacion = Convert.ToInt32(row.Cells["NroHabitacion"].Value);

            // Confirmar eliminación
            DialogResult result = MessageBox.Show(
                $"¿Desea eliminar la cama {nroCama} de la habitacion {nroHabitacion}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminar de la base de datos
                    _service.EliminarCama(nroHabitacion, nroCama);

                    // Refrescar la grilla
                    CargarCamas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la especialidad: " + ex.Message);
                }
            }
        }

    }
}
