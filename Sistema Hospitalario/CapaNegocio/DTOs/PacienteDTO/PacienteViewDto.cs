using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO
{
    public class PacienteDto
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_nacimiento {get; set; }
        public string Observaciones { get; set; }
        public string Direccion {  get; set; }
        public string Email { get; set; }
        public string Estado_paciente { get; set; }
        public int Id_estado_paciente { get; set; }
        public string Telefono { get; set; }
        public int Edad 
        { 
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Fecha_nacimiento.Year;
                if (Fecha_nacimiento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }

    // ===== Clases “view” para mostrar en los combos (DisplayMember/ValueMember) =====
    public class PacienteItem
    {
        public int Id { get; set; }
        public string Texto { get; set; } // "Apellido, Nombre (DNI)"
    }

    public class PacienteListadoDto
    {
        public int Id { get; set; }
        public string Paciente { get; set; }     
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
        public string Estado { get; set; }       

        public string Email { get; set; }
        public string Observaciones { get; set; }
    }
}

