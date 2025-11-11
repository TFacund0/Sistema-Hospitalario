using Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService;
using Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService;
using Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService;
using Sistema_Hospitalario.CapaNegocio.Servicios.EstadisticasService;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


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
                CargarGraficoPacientesSemana();
                CargarGraficoCamas();
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

            lblPacientesEgresos.Text = _pacienteService.ListarPacienteEgresados().Count().ToString();
        }

        private void CargarGraficoPacientesSemana()
        {
            var _estadisticasService = new EstadisticasService();

            chartPacientes.Series.Clear();
            chartPacientes.ChartAreas[0].AxisX.Title = "Día";
            chartPacientes.ChartAreas[0].AxisY.Title = "Cantidad de Pacientes";

            // 👉 Pedimos los datos ya procesados al servicio
            var datosSemana = _estadisticasService.ObtenerPacientesSemana();

            Series serieActivos = new Series("Activos")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            Series serieAltas = new Series("Altas")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            foreach (var dia in datosSemana.OrderBy(d => d.Fecha))
            {
                string etiqueta = dia.Fecha.ToString("dd/MM");

                serieActivos.Points.AddXY(etiqueta, dia.CantActivos);
                serieAltas.Points.AddXY(etiqueta, dia.CantAltas);
            }

            chartPacientes.Series.Add(serieActivos);
            chartPacientes.Series.Add(serieAltas);

            serieActivos["PointWidth"] = "0.4";
            serieAltas["PointWidth"] = "0.4";
        }


        private void CargarGraficoCamas()
        {
            var _estadisticasService = new EstadisticasService();
            chartCamas.Series.Clear();

            Series serieCamas = new Series("Camas")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            // 👉 Pedimos al servicio la distribución de camas
            var distribucion = _estadisticasService.ObtenerDistribucionCamas();

            int total = distribucion.Ocupadas + distribucion.Disponibles;
            if (total == 0)
                return;

            serieCamas.Points.AddXY("Ocupadas", distribucion.Ocupadas);
            serieCamas.Points.AddXY("Disponibles", distribucion.Disponibles);

            serieCamas.Label = "#VALX\n#PERCENT{P0}";
            serieCamas.LegendText = "#VALX";

            if (chartCamas.Legends.Count > 0)
                chartCamas.Legends[0].Enabled = true;

            chartCamas.Series.Add(serieCamas);
        }
    }
}