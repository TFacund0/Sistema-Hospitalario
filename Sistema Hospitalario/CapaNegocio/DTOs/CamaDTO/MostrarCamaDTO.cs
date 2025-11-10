using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO
{
    public class MostrarCamaDTO
    {
        public int NroCama { get; set; }
        public int NroHabitacion { get; set; }
        public int IdEstadoCama { get; set; }
        public string Estado { get; set; }
    }
}
