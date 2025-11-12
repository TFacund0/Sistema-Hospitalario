using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.ConsultaDTO
{
    public class ConsultaAltaDTO
    {
        public string DniPaciente { get; set; }
        public string NroAfiliado { get; set; } // Lo usaremos para verificar
        public string Motivo { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime Fecha { get; set; }
    }
}
