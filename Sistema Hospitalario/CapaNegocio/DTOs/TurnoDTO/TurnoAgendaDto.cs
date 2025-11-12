using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO
{
    public class TurnoAgendaDto
    {
        public int IdTurno { get; set; }
        public string Hora { get; set; } // Texto formateado ej: "09:30"
        public string Paciente { get; set; }
        public string Estado { get; set; }
    }

    public class AgendaContadoresDto
    {
        public int Pendientes { get; set; }
        public int Completadas { get; set; }
        public int Canceladas { get; set; }
    }

}
