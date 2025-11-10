using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService;
using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Gerente
{
    public partial class UC_HomeGerente : UserControl
    {
        public UC_HomeGerente()
        {
            InitializeComponent();

            try
            {
                CargarInformacionPaneles();
            }
            catch (Exception ex)
    {
                MessageBox.Show($"Error al inicializar UC_Hospitalizacion: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarInformacionPaneles()
        {
            var _turnoService = new TurnoService();
            lblCantidadConsultas.Text = _turnoService.ListarTurnos().Where(t => t.Procedimiento.ToLower() == "consulta").Count().ToString();

            CamaService camaService = new CamaService();
            float cantidadCamasOcupadas = camaService.TotalCamasXEstado("ocupada");
            float cantidadCamas = camaService.TotalCamas();

            string porcentajeCamas = ((cantidadCamasOcupadas / cantidadCamas) * 100).ToString() + "%";

            lblOcupacionCamas.Text = porcentajeCamas;

            var _pacienteService = new PacienteService();
            lblPacientesInternados.Text = _pacienteService.ListarPacientes().Where(p => p.Estado_paciente.ToLower() == "internado").Count().ToString();
            
            lblPacientesActivos.Text = _pacienteService.ListarPacientes().Where(p => p.Estado_paciente.ToLower() == "activo").Count().ToString();

            lblPacientesEgresos.Text = _pacienteService.ListarPacientes().Where(p => p.Estado_paciente.ToLower() == "alta").Count().ToString();
        }
    }
}
