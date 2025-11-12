using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.HistorialDTO
{
    public class HistorialItemDto
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } // Ej: "Consulta" o "Procedimiento"
        public string Motivo { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string NombreMedico { get; set; }
        public string DniMedico { get; set; } // ¡Clave para tu filtro!
    }
}
