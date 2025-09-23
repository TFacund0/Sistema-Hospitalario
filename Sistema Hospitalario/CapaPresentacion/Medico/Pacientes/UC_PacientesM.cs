using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sistema_Hospitalario.CapaPresentacion.Administrativo.UC_Pacientes;


namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    
    public partial class UC_PacientesM : UserControl

    {

        private List<PacienteDTO> pacienteDTOs = new List<PacienteDTO>();
        private readonly BindingSource _bs = new BindingSource();
        public UC_PacientesM()
        {
            InitializeComponent();
            CargarFilasEjemplo();
            ConfigurarActividad();
        }
        
        private void ConfigurarEnlazadoDeColumnas()
        {
            dgvPacientes.AutoGenerateColumns = false;

            dgvPacientes.Columns["colPaciente"].DataPropertyName = "Paciente";   // string calculado: nombre + apellido
            dgvPacientes.Columns["colDNI"].DataPropertyName = "DNI";        // string (dni.ToString())
            dgvPacientes.Columns["colEdad"].DataPropertyName = "Edad";       // int calculado por fecha nac.
            dgvPacientes.Columns["colEstado"].DataPropertyName = "Estado";
            dgvPacientes.Columns["colHabitacion"].DataPropertyName = "Habitacion"; // string ("" si 0)
        }

        private void CargarFilasEjemplo()
        {
            pacienteDTOs = new List<PacienteDTO>
            {
                new PacienteDTO()
                {
                    nombre = "Juan",
                    apellido = "Pérez",
                    direccion = "Calle Falsa 123",
                    obraSocial = "OSDE",
                    nroAfiliado = 123456,
                    dni = 12345678,
                    habitacion = 101,
                    telefono = 123456789,
                    observaciones = "Ninguna",
                    Estado = "Internado",
                    FechaNacimiento = new DateTime(1980, 5, 15)
                },
                new PacienteDTO()
                {
                    nombre = "María",
                    apellido = "Gómez",
                    direccion = "Avenida Siempre Viva 742",
                    obraSocial = "Swiss Medical",
                    nroAfiliado = 654321,
                    dni = 87654321,
                    habitacion = 202,
                    telefono = 987654321,
                    observaciones = "Alergia a la penicilina",
                    Estado = "Consulta",
                    FechaNacimiento = new DateTime(2024, 11, 22)
                },
            }
            ;

            _bs.DataSource = pacienteDTOs;
            dgvPacientes.DataSource = _bs;
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

        private void UC_PacientesM_Load(object sender, EventArgs e)
        {

        }

        private void DgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // asegurarse que no sea header
            {
                // Obtenemos la fila seleccionada
                DataGridViewRow fila = dgvPacientes.Rows[e.RowIndex];
                
                MenuMedicos menu = this.FindForm() as MenuMedicos;
                // Obtenemos el paciente asociado a la fila
                PacienteDTO paciente = fila.DataBoundItem as PacienteDTO;

                if (menu != null)
                {
                    // Llamamos al método de MenuMedicos
                    menu.AbrirUserControl(new UC_DetallePaciente(paciente));
                }
            }
        }
    }
}
