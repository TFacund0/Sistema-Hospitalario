using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO
{
    /// <summary>
    /// Representa un tipo de habitación disponible (ej. Terapia Intensiva, Sala Común).
    /// </summary>
    public class TiposHabitacionDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único del tipo de habitación.
        /// </summary>
        public int IdTipoHabitacion { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre o descripción del tipo.
        /// </summary>
        public string Nombre { get; set; }
    }
}
