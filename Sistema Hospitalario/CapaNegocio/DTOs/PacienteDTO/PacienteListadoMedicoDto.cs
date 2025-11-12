using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO
{
    public class PacienteListadoMedicoDto
    {
        public int IdPaciente { get; set; } 
        public DateTime FechaNacim { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public string Habitacion { get; set; } // "101" o "Ambulatorio"
        public string Telefono { get; set; }
        public string Observacion { get; set; } // Asumimos 'observaciones'
        public string Estado { get; set; } // "Internado", "Consulta"
    }
}
