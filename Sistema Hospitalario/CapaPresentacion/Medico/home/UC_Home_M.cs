using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico.home
{
    public partial class UC_Home_M : UserControl
    {
        public UC_Home_M()
        {
            InitializeComponent();

            InformacionPaneles();
            ConfigurarActividad();
            CargarFilasEjemplo();
        }

        private void InformacionPaneles() // Simula la carga de datos en los paneles (estos datos deberían venir de la base de datos)
        {
            string cantPacientes = "350";
            string cantCamasOcupadas = "50";
            string totalCamas = "100";
            string cantConsultas = "115";
            string cantEmergencias = "15";

            if (int.TryParse(cantPacientes, out int valorPaciente) && valorPaciente >= 0 && valorPaciente < 1000 &&
                int.TryParse(cantCamasOcupadas, out int valorCamasOcupadas) && valorCamasOcupadas >= 0 && valorCamasOcupadas < 1000 &&
                int.TryParse(totalCamas, out int valorCamas) && valorCamas >= 0 && valorCamas < 1000 && valorCamasOcupadas <= valorCamas &&
                int.TryParse(cantConsultas, out int valorConsultas) && valorConsultas >= 0 && valorConsultas < 1000 &&
                int.TryParse(cantEmergencias, out int valorEmergencias) && valorEmergencias >= 0 && valorEmergencias < 1000)
            {
                string porcentajeCamas = (((float)valorCamasOcupadas / (float)valorCamas) * 100).ToString() + "%";

                lblPacientesActivos.Text = cantPacientes;
                lblCamasOcupadas.Text = cantCamasOcupadas + "/" + totalCamas;
                lblPorcentajeCamas.Text = porcentajeCamas + " de ocupación";
                lblCantidadConsultas.Text = cantConsultas;
                lblEmergencias.Text = cantEmergencias;
            }
            else
            {
                // Validación fallida, manejar el error adecuadamente
                MessageBox.Show("Error: Los valores de los paneles deben ser números enteros no negativos y dentro del rango permitido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarActividad()
        {
            // Estilos generales
            dgvActividad.ReadOnly = true;
            dgvActividad.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActividad.RowHeadersVisible = false;
            dgvActividad.AllowUserToResizeRows = false;

            dgvActividad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActividad.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvActividad.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            dgvActividad.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvActividad.ColumnHeadersHeight = 35;
            dgvActividad.EnableHeadersVisualStyles = false;
            dgvActividad.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }


        private void CargarFilasEjemplo()
        {
            // Ejemplo de datos, en la práctica estos vendrían de la base de datos
            dgvActividad.Rows.Add("María", "González", "Ingreso a emergencias", "10:30", "Urgencia");
            dgvActividad.Rows.Add("Carlos", "Rodríguez", "Alta médica", "09:05", "Info");
            dgvActividad.Rows.Add("Susana", "Pérez", "Consulta programada", "08:45", "Consulta");
        }
    }
}
