using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private void CargarInformacionPaneles()
        {
            string cantPacientes = "350";
            string cantCamasOcupadas = "50";
            string totalCamas = "100";
            string cantConsultas = "115";

            if (
                int.TryParse(cantPacientes, out int valorPaciente) && valorPaciente >= 0 && valorPaciente < 1000 &&
                int.TryParse(cantCamasOcupadas, out int valorCamasOcupadas) && valorCamasOcupadas >= 0 && valorCamasOcupadas < 1000 &&
                int.TryParse(totalCamas, out int valorCamas) && valorCamas >= 0 && valorCamas < 1000 && valorCamasOcupadas <= valorCamas &&
                int.TryParse(cantConsultas, out int valorConsultas) && valorConsultas >= 0 && valorConsultas < 1000
                )
            {
                string porcentajeCamas = (((float)valorCamasOcupadas / (float)valorCamas) * 100).ToString() + "%";

                lblPacientesActivos.Text = cantPacientes;
                lblCamasOcupadas.Text = cantCamasOcupadas + "/" + totalCamas;
                lblPorcentajeCamas.Text = porcentajeCamas + " de ocupación";
                lblCantidadConsultas.Text = cantConsultas;
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