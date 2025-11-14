using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO
{
    public class InternacionDto
    {
        public int Id_internacion { get; set; }
        public int Id_paciente { get; set; }
        public string Internado { get; set; } 
        public int Id_medico{ get; set; }
        public string NombreCompletoMedico { get; set; }
        public int  Id_procedimiento { get; set; }
        public string procedimiento { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public DateTime? Fecha_egreso { get; set; }
        public string Diagnostico { get; set; }
        public int Nro_habitacion { get; set; }
        public int Id_cama { get; set; }
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

    public class FinalizarInternacionDto
    {
        public int IdInternacion { get; set; }
        public int NroHabitacion { get; set; }
        public int IdCama { get; set; }

        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }

        public string DiagnosticoIngreso { get; set; }
        public string DiagnosticoEgreso { get; set; }

        public int IdMedicoEgreso { get; set; }  // si necesitás guardarlo
    }

}
