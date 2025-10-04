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
    public partial class UC_Hospitalizacion : UserControl
    {
        // Evento para notificar cuando se solicita registrar una internación
        public event EventHandler RegistrarInternacionSolicitada;        

        // ============================ CONSTRUCTOR DEL UC HOSPITALIZACIÓN ============================
        public UC_Hospitalizacion()
        {
            InitializeComponent();

            ConfigurarTablaHabitaciones();            
        }

        // ============================ BOTÓN REGISTRAR INTERNACIÓN ============================
        private void btnRegistrarInternacion_Click(object sender, EventArgs e)
        {
            RegistrarInternacionSolicitada?.Invoke(this, EventArgs.Empty);
        }

        // Método que configura el DataGridView de Habitaciones en el UC Hospitalización
        private void ConfigurarTablaHabitaciones()
        {
            dgvHabitaciones.AutoGenerateColumns = false; // Desactiva la generación automática de columnas
            dgvHabitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de fila completa

            dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajusta el tamaño de las columnas para llenar el espacio disponible
            dgvHabitaciones.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Establece la fuente predeterminada para las celdas
            dgvHabitaciones.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248); // Color de fondo para filas alternas

            dgvHabitaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente en negrita para los encabezados de columna
            dgvHabitaciones.ColumnHeadersHeight = 35; // Altura de los encabezados de columna
            dgvHabitaciones.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke; // Color de fondo para los encabezados de columna
        }
    }
}
