using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaPresentacion.Medico.Turnos
{
    internal class UC_VisualizarTurno : UserControl
    {
        private TurnoDto turno;

        public UC_VisualizarTurno(TurnoDto turno)
        {
            this.turno = turno;
        }
    }
}