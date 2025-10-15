using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.Servicios;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService;

namespace Sistema_Hospitalario.CapaPresentacion.Administrativo
{
    public partial class UC_HomeGerente : UserControl
    {
        // ============================ CONSTRUCTOR DEL UC HOME ADMINISTRATIVO ============================
        public UC_HomeGerente()
        {
            InitializeComponent();

            CargarInformacionPaneles();
            ConfigurarTablaActividad();
        }

        // Método que configuran la información de las estadísticas en el UC Home Gerente
        private async void CargarInformacionPaneles()
        {
            PacienteService pacienteService = new PacienteService();
            int cantidadPacientes = await pacienteService.ContarPorEstadoIdAsync(1);

            CamaService camaService = new CamaService();
            int cantidadCamasOcupadas = await camaService.TotalCamasXEstado(9, "Ocupada");
            int cantidadCamas = await camaService.TotalCamas();

            InternacionService internacionService = new InternacionService();
            int cantidadInternaciones = internacionService.listadoInternacionDtos().Count;

            string cantPacientes = cantidadPacientes.ToString();
            string cantCamasOcupadas = cantidadCamasOcupadas.ToString();
            string totalCamas = cantidadCamas.ToString();
            string cantInternaciones = cantidadInternaciones.ToString();

            if (
                int.TryParse(cantPacientes, out int valorPaciente) && valorPaciente >= 0 && valorPaciente < 1000 &&
                int.TryParse(cantCamasOcupadas, out int valorCamasOcupadas) && valorCamasOcupadas >= 0 && valorCamasOcupadas < 1000 &&
                int.TryParse(totalCamas, out int valorCamas) && valorCamas >= 0 && valorCamas < 1000 && valorCamasOcupadas <= valorCamas &&
                int.TryParse(cantInternaciones, out int valorInternaciones) && valorInternaciones >= 0 && valorInternaciones < 1000
                )
            {
                string porcentajeCamas = (((float)valorCamasOcupadas / (float)valorCamas) * 100).ToString() + "%";

                lblPacientesActivos.Text = cantPacientes;
                lblCamasOcupadas.Text = cantCamasOcupadas + "/" + totalCamas;
                lblPorcentajeCamas.Text = porcentajeCamas + " de ocupación";
                lblInternaciones.Text = cantInternaciones;
            }
            else
            {
                MessageBox.Show("Error: Los valores de los paneles deben ser números enteros no negativos y dentro del rango permitido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método que configura el DataGridView de Actividad Reciente en el UC Home Gerente
        private void ConfigurarTablaActividad()
        {
            dgvActividad.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de fila completa

            dgvActividad.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Establece la fuente predeterminada para las celdas
            dgvActividad.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248); // Color de fondo para filas alternas

            dgvActividad.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente en negrita para los encabezados de columna
            dgvActividad.ColumnHeadersHeight = 35; // Altura de los encabezados de columna
            dgvActividad.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke; // Color de fondo para los encabezados de columna
        }
    }
}