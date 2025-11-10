using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO
{
    public class PacienteEditarDto
    {
        public int Id { get; set; }             
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }      
        public string Email { get; set; }
        public string Observaciones { get; set; }
    }
}

