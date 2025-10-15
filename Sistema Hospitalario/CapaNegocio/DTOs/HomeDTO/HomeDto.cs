using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HomeDTO
{
    public class HomeDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Accion { get; set; }
        public DateTime Horario { get; set; }
        public string Tipo { get; set; }
    }
}
