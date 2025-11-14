using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO
{
    public class MostrarHabitacionDTO
    {
        public int NroPiso { get; set; }
        public int NroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public int IdTipoHabitacion { get; set; }
        public int? TotalCamas { get; set; }
    }
}
