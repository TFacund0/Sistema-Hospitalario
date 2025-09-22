using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs
{
    public class PacienteListadoDto
    {
        public int Id { get; set; }
        public string Paciente { get; set; }     // "Nombre Apellido"
        public int DNI { get; set; }
        public int Edad { get; set; }
        public string Estado { get; set; }
    }
    public class PacienteDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }        // nombre legible
        public string ObraSocial { get; set; }    // nombre legible
        public int? NumeroAfiliado { get; set; }
        public string Observaciones { get; set; }
    }
}

