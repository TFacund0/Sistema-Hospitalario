using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.EstadisticasDTO
{
    /// <summary>
    /// Representa el conteo de pacientes (activos y altas) para una fecha específica.
    /// </summary>
    public class PacientesPorDiaDto
    {
        /// <summary>
        /// Obtiene o establece la fecha del reporte.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Cantidad de pacientes con estado 'Activo'.
        /// </summary>
        public int CantActivos { get; set; }

        /// <summary>
        /// Cantidad de pacientes con estado 'Alta'.
        /// </summary>
        public int CantAltas { get; set; }
    }

    /// <summary>
    /// Representa la disponibilidad actual de camas en el hospital.
    /// </summary>
    public class CamasDistribucionDto
    {
        /// <summary>
        /// Número de camas marcadas como 'Ocupada'.
        /// </summary>
        public int Ocupadas { get; set; }

        /// <summary>
        /// Número de camas marcadas como 'Disponible'.
        /// </summary>
        public int Disponibles { get; set; }
    }

    /// <summary>
    /// Estructura para el gráfico de volumen de turnos por día.
    /// </summary>
    public class TurnosPorDiaDto
    {
        /// <summary>
        /// Fecha del día consultado.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Conteo total de turnos (independientemente del estado) para ese día.
        /// </summary>
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Distribución de turnos según su estado actual para reportes estadísticos.
    /// </summary>
    public class TurnosEstadosDistribucionDto
    {
        /// <summary>
        /// Cantidad de turnos en estado 'Pendiente'.
        /// </summary>
        public int Pendientes { get; set; }

        /// <summary>
        /// Cantidad de turnos en estado 'Atendido'.
        /// </summary>
        public int Atendidos { get; set; }

        /// <summary>
        /// Cantidad de turnos en estado 'Cancelado'.
        /// </summary>
        public int Cancelados { get; set; }
    }

    /// <summary>
    /// Estructura para visualizar el crecimiento de la base de pacientes registrados.
    /// </summary>
    public class PacientesRegistradosPorDiaDto
    {
        /// <summary>
        /// Fecha de registro.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Cantidad de pacientes nuevos registrados en esta fecha.
        /// </summary>
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Distribución global de pacientes según su estado clínico o administrativo actual.
    /// </summary>
    public class PacientesEstadosDistribucionDto
    {
        /// <summary>
        /// Total de pacientes en estado 'Activo'.
        /// </summary>
        public int Activos { get; set; }

        /// <summary>
        /// Total de pacientes actualmente 'Internados'.
        /// </summary>
        public int Internados { get; set; }

        /// <summary>
        /// Total de pacientes que han recibido el 'Alta'.
        /// </summary>
        public int Altas { get; set; }
    }
}

