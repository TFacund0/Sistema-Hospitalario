using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.EstadisticasDTO
{
    public class PacientesPorDiaDto
    {
        public DateTime Fecha { get; set; }
        public int CantActivos { get; set; }
        public int CantAltas { get; set; }
    }

    public class CamasDistribucionDto
    {
        public int Ocupadas { get; set; }
        public int Disponibles { get; set; }
    }

    public class TurnosPorDiaDto
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
    }

    public class TurnosEstadosDistribucionDto
    {
        public int Pendientes { get; set; }
        public int Atendidos { get; set; }
        public int Cancelados { get; set; }
    }
}

