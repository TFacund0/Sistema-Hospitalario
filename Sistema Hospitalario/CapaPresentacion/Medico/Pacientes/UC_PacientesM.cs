using Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService;
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

        private MedicoService _service = new MedicoService();
        public UC_PacientesM()
        {
            InitializeComponent();
            RefrescarGrilla();
            ConfigurarEstilosGrilla();
            CargarContadores();
        }

        private void RefrescarGrilla(string nombre = null, string apellido = null, string dni = null, DateTime? fechaTurno = null)
        {
            try
            {
                var lista = _service.ObtenerPacientes(nombre, apellido, dni, fechaTurno);
                dgvPacientes.DataSource = lista;

                if (dgvPacientes.Columns.Contains("IdPaciente"))
                {
                    dgvPacientes.Columns["IdPaciente"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEstilosGrilla()
        {
            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.RowHeadersVisible = false;
            dgvPacientes.BackgroundColor = Color.White;
            dgvPacientes.BorderStyle = BorderStyle.None;
            dgvPacientes.EnableHeadersVisualStyles = false;

            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvPacientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPacientes.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvPacientes.DefaultCellStyle.ForeColor = Color.Black;
            dgvPacientes.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            
            if (dgvPacientes.Columns.Contains("Historial"))
            {
                dgvPacientes.Columns["Historial"].DefaultCellStyle.NullValue = "Ver";
            }
        }

        private void DgvPacientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow fila = dgvPacientes.Rows[e.RowIndex];
                
                MenuMedicos menu = this.FindForm() as MenuMedicos;
                PacienteDTO paciente = fila.DataBoundItem as PacienteDTO;

                if (menu != null)
                {
                    menu.AbrirUserControl(new UC_DetallePaciente(paciente));
                }
            }
        }
        private void CargarContadores()
        {
            try
            {
                lblTotalPacientes.Text = _service.ObtenerConteoTotalPacientes().ToString();
            }
            catch (Exception ex)
            {
                lblTotalPacientes.Text = "N/A";
                MessageBox.Show("Error al cargar contadores: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DateTime? fecha = null;
            if (dtpFechaTurno.Checked)
            {
                fecha = dtpFechaTurno.Value.Date;
            }
            RefrescarGrilla(txtNombre.Text, txtApellido.Text, txtDni.Text, fecha);
        }
    }
}
