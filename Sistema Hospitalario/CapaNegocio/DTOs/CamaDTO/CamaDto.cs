using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO
{
    public class CamaDto
    {
        public int NroCama { get; set; }
        
        public int NroHabitacion { get; set; }
        public int IdEstadoCama { get; set; }
        public string EstadoCama { get; set; }
    }
}
