using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO
{
    public class PacienteAltaDto
    {
        // ================== Datos personales obligatorios ==================
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Observaciones { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }     
        public string EstadoInicial { get; set; }
    }
}

