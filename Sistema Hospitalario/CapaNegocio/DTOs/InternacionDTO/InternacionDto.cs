using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO
{
    public class InternacionDto
    {
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Procedimiento { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public DateTime? Fecha_egreso { get; set; }
        public string Diagnostico { get; set; }
        public int Nro_habitacion { get; set; }
        public int Cama { get; set; }
        public int Nro_piso { get; set; }

    }

    public class ListadoInternacionDto
    {
        public int Nro_habitacion { get; set; } 
        public int Nro_piso { get; set; }
        public string Internado { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public int Cama { get; set; }
        public string Tipo_habitacion { get; set; }
    }
}
