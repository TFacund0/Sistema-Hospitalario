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
            ConfigurarEstilosGrilla();
        }


        // ===================== CARGAR TURNOS INICIALES =====================
        private void cargarTurnos()
        {
            if (SesionUsuario.IdMedicoAsociado.HasValue)
            {
                _idMedicoLogueado = SesionUsuario.IdMedicoAsociado.Value;
            }
            else
            {
                MessageBox.Show("Error: No se pudo identificar al médico logueado.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RefrescarAgenda(null);
        }

        // ===================== REFRESCAR AGENDA Y CONTADORES =====================
        private void RefrescarAgenda(DateTime? fecha)
        {

            try
            {
                var turnosMedico = _turnoService.ListarTurnos()
                    .Where(t => t.Id_medico == _idMedicoLogueado) // o IdMedico según tu DTO
                    .ToList();
                if (fecha.HasValue)
                    turnosMedico = turnosMedico.Where (t => t.Fecha_Del_Turno.Date == fecha).ToList();

                turnosMedico = turnosMedico.OrderBy(t => t.Fecha_Del_Turno).ToList();

                dgvTurnos.DataSource = null;
                dgvTurnos.DataSource = turnosMedico;

                if (dgvTurnos.Columns["Id_turno"] != null)
                {
                    dgvTurnos.Columns["Id_turno"].Visible = false;
                }

                if (dgvTurnos.Columns["Id_medico"] != null)
                {
                    dgvTurnos.Columns["Id_medico"].Visible = false;
                }
                // contadores coherentes al mismo filtro
                lblTotalPendientes.Text = turnosMedico.Count(t => t.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase)).ToString();
                lblTotalCompletadas.Text = turnosMedico.Count(t => t.Estado.Equals("atendido", StringComparison.OrdinalIgnoreCase)).ToString();
                lblTotalRecanceladas.Text = turnosMedico.Count(t => t.Estado.Equals("cancelado", StringComparison.OrdinalIgnoreCase)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la agenda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ===================== CONFIGURACIÓN ESTILOS GRILLA =====================
        private void ConfigurarEstilosGrilla()
        {

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
            RefrescarAgenda(null);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = dtpFecha.Value.Date;
            RefrescarAgenda(fechaSeleccionada);
        }

        private void dgvTurnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Si hace clic en el header

            int idTurno = (int)dgvTurnos.Rows[e.RowIndex].Cells["Id_turno"].Value;
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
                        DateTime fechaSeleccionada = dtpFecha.Value.Date;
                        RefrescarAgenda(fechaSeleccionada);
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
