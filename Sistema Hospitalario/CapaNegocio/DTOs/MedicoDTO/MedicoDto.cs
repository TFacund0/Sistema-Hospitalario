using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public string matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }
}
