using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
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
            _service = new CamaService();
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
                    int NroHabitacion;
                    if (int.TryParse(TBHabitacionCama.Text.Trim(), out NroHabitacion))
                    {
                        _service.AgregarCama(NroHabitacion);
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
            if (e.RowIndex < 0)
                return;

            var row = dgvCamas.Rows[e.RowIndex];
            int nroCama = Convert.ToInt32(row.Cells["NroCama"].Value);
            int nroHabitacion = Convert.ToInt32(row.Cells["NroHabitacion"].Value);

            string nombreColumna = dgvCamas.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "Estado")
            {
                string estadoActual = (string)dgvCamas.Rows[e.RowIndex].Cells["Estado"].Value;

                // Le pasamos el estado actual para que el ComboBox aparezca pre-seleccionado.
                Form_CambiarEstadoCama formDialogo = new Form_CambiarEstadoCama(estadoActual);

                // .ShowDialog() es clave: congela el formulario principal
                // y espera a que el usuario presione "Aceptar" o "Cancelar".
                DialogResult resultado = formDialogo.ShowDialog();

                // ----- 4. ACTUAR SEGÚN EL RESULTADO -----
                if (resultado == DialogResult.OK)
                {
                    // El usuario presionó "Aceptar". Obtenemos el nuevo ID del formulario.
                    int nuevoEstadoId = formDialogo.NuevoEstadoIdSeleccionado;

                    // Llamamos al servicio para que haga la magia en la BD
                    // (Crearemos este método en el Paso 4)
                    _service.CambiarEstado(nroHabitacion, nroCama, nuevoEstadoId);

                    CargarCamas();
                }
            }
            else
            {

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
}
