using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO
{
    public class MostrarMedicoDTO
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string Matricula { get; set; }
        public string Correo { get; set; }
        public string Especialidad { get; set; }
    }
}
