using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO
{
    public class TurnoDto
        {
            public int Id_turno { get; set; }
            public string Paciente { get; set; }
            public int Id_paciente { get; set; }
            public string Medico { get; set; }
            public int Id_medico { get; set; }
            public string Procedimiento { get; set; }
            public int Id_procedimiento { get; set; }
            public string Correo { get; set; }
            public string DNI { get; set; }
            public string Telefono { get; set; }

            public DateTime FechaTurno { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Observaciones { get; set; }
            public string Estado { get; set; }
        }

    public class ListadoTurno
    {
        public int Id_turno { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public int Id_medico { get; set; }
        public string Procedimiento { get; set; }
        public DateTime FechaTurno { get; set; }
        public DateTime Fecha_Del_Turno { get; set; }
        public string Estado { get; set; }
    }

    public class ListadoEstadoTurno
    {
        public string Id_estado { get; set; }
        public string Estado { get; set; }
    }
}
