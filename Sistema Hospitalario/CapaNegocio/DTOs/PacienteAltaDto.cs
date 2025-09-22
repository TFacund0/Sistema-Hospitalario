using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs
{
    public class PacienteAltaDto
    {
        // ================== Datos personales obligatorios ==================
        public string Nombre { get; set; }          // Máx 50
        public string Apellido { get; set; }        // Máx 50
        public int Dni { get; set; }             // varchar(10), lo dejo string por ceros a la izquierda
        public string Telefono { get; set; }        // varchar(15)
        public string Direccion { get; set; }       // varchar(50)
        public string ObraSocial { get; set; }      // si el paciente tiene obra social
        public int NumeroAfiliado { get; set; }  // número de afiliado vinculado a la obra social

        public DateTime? FechaNacimiento { get; set; }
        public string Observaciones { get; set; }   // varchar(200), comentarios iniciales
        public string EstadoInicial { get; set; }    // si manejás un valor o importe de referencia
    }
}

