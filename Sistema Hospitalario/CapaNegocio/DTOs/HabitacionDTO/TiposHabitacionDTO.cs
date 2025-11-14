using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO
{
    public class TiposHabitacionDTO
    {
        public int IdTipoHabitacion { get; set; }
        public string Nombre { get; set; }
        public int? LimiteCamas { get; set; }
    }
}
