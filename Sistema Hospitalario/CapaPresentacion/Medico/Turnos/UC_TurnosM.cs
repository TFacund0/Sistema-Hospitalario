using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;
using Sistema_Hospitalario.CapaPresentacion.Medico.Turnos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    public partial class panel1 : UserControl
    {
        private TurnoService _turnoService = new TurnoService(new TurnoRepository());
        private int _idMedicoLogueado;
        public panel1()
        {
            InitializeComponent();
            cargarTurnos();
        }
        private void cargarTurnos()
        {
            if (SesionUsuario.IdMedicoAsociado.HasValue)
            {
                _idMedicoLogueado = SesionUsuario.IdMedicoAsociado.Value;
            }
            else
            {
                MessageBox.Show("Error: No se pudo identificar al médico logueado.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // O deshabilitar el formulario
            }

            // Asumimos que el DateTimePicker se llama 'dtpFecha'
            dtpFecha.Value = DateTime.Today;
            RefrescarAgenda();
            ConfigurarEstilosGrilla();
        }
        private void RefrescarAgenda()
        {
            DateTime fechaSeleccionada = dtpFecha.Value.Date;

            // 1. Cargar la Grilla
            try
            {
                // Asumimos que el DGV se llama 'dgvTurnos'
                var listaTurnos = _turnoService.ObtenerTurnosParaAgenda(_idMedicoLogueado, fechaSeleccionada);
                dgvTurnos.DataSource = listaTurnos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la agenda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 2. Cargar los Contadores
            try
            {
                var contadores = _turnoService.ObtenerContadoresAgenda(_idMedicoLogueado, fechaSeleccionada);

                // Reemplazá con los nombres de tus Labels
                lblTotalPendientes.Text = contadores.Pendientes.ToString();
                lblTotalCompletadas.Text = contadores.Completadas.ToString();
                lblTotalRecanceladas.Text = contadores.Canceladas.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los contadores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblTotalPendientes.Text = "N/A";
                lblTotalCompletadas.Text = "N/A";
                lblTotalRecanceladas.Text = "N/A";
            }
        }

        private void ConfigurarEstilosGrilla()
        {
            if (dgvTurnos.Columns.Contains("IdTurno"))
            {
                dgvTurnos.Columns["IdTurno"].Visible = false;
            }
            dgvTurnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTurnos.RowHeadersVisible = false;
            dgvTurnos.BackgroundColor = Color.White;
            dgvTurnos.BorderStyle = BorderStyle.None;
            dgvTurnos.EnableHeadersVisualStyles = false;

            dgvTurnos.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvTurnos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTurnos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvTurnos.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvTurnos.DefaultCellStyle.ForeColor = Color.Black;
            dgvTurnos.DefaultCellStyle.Font = new Font("Segoe UI", 9);

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Today;
            // (Limpiá los otros filtros de texto/combo si los usás)
            RefrescarAgenda();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RefrescarAgenda();
        }

        private void dgvTurnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Si hace clic en el header

            int idTurno = (int)dgvTurnos.Rows[e.RowIndex].Cells["IdTurno"].Value;
            string estadoActual = (string)dgvTurnos.Rows[e.RowIndex].Cells["Estado"].Value;

            Form_CambiarEstadoTurno formDialogo = new Form_CambiarEstadoTurno(estadoActual);
            DialogResult resultado = formDialogo.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                try
                {
                    int nuevoEstadoId = formDialogo.NuevoEstadoId;
                    if (_turnoService.ActualizarEstadoTurno(idTurno, nuevoEstadoId))
                    {
                        RefrescarAgenda(); // ¡Actualizamos todo!
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el turno (quizás fue eliminado).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
