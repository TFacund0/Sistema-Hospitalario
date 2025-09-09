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
    public partial class UC_Pacientes : UserControl
    {
        public UC_Pacientes()
        {
            InitializeComponent();

            InformacionPaneles();
            ConfigurarActividad();
            CargarFilasEjemplo();
        }

        private void InformacionPaneles()
        {
            string cantPacientes = "5";
            string cantInternados = "3";
            string cantEmergencias = "1";
            string cantAmbulatorio = "3";
            string cantAlta = "2";

            if (int.TryParse(cantPacientes, out int valorPaciente) && valorPaciente >= 0 && valorPaciente < 1000 &&
                int.TryParse(cantInternados, out int valorInternados) && valorInternados >= 0 && valorInternados < 1000 &&
                int.TryParse(cantAmbulatorio, out int valorAmbulatorio) && valorAmbulatorio >= 0 && valorAmbulatorio < 1000 &&
                int.TryParse(cantAlta, out int valorAlta) && valorAlta >= 0 && valorAlta < 1000 &&
                int.TryParse(cantEmergencias, out int valorEmergencias) && valorEmergencias >= 0 && valorEmergencias < 1000)
            {
                lblPacientes.Text = cantPacientes;
                lblInternados.Text = cantInternados;
                lblEmergencias.Text = cantEmergencias;
                lblAmbulatorio.Text = cantAmbulatorio;
                lblAlta.Text = cantAlta;

            }
            else
            {
                MessageBox.Show("Error: Los valores de los paneles deben ser números enteros no negativos y dentro del rango permitido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarActividad()
        {
            // Estilos generales
            dgvPacientes.ReadOnly = true;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.RowHeadersVisible = false;
            dgvPacientes.AllowUserToResizeRows = false;

            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPacientes.ColumnHeadersHeight = 35;
            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }


        private void CargarFilasEjemplo()
        {
            dgvPacientes.Rows.Add("María González", "12345678", 45, "3794-123456", "101", "Internada", "Dr. Pérez", "Ver");
            dgvPacientes.Rows.Add("Carlos Rodríguez", "23456789", 60, "3794-654321", "Emergencias", "Urgencia", "Dra. López", "Ver");
            dgvPacientes.Rows.Add("Susana Pérez", "34567890", 30, "3794-987654", "Ambulatorio", "Consulta", "Dr. García", "Ver");
        }

        // Evento para manejar el clic en el botón "Registrar Paciente"
        public event EventHandler RegistrarPacienteSolicitado;

        private void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            // Dispara un evento que tiene que manejar el formulario del Menu Administrativo
            RegistrarPacienteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ExportarDatosSolicitado;

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarDatosSolicitado?.Invoke(this, EventArgs.Empty);
        }
    }
}
