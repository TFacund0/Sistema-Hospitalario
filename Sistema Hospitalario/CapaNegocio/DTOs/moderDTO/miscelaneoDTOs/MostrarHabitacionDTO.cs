using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO
{
    public class MostrarHabitacionDTO
    {
        public int NroPiso { get; set; }
        public int NroHabitacion { get; set; }
        public string tipo_habitacion { get; set; } 
        public int id_tipo_habitacion { get; set; }
    }
}
