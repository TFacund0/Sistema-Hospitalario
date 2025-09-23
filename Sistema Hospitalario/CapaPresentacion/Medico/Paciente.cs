using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaPresentacion.Medico
{
    
        public class PacienteDTO
        {
            public DateTime FechaNacimiento { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string direccion { get; set; }
            public string obraSocial { get; set; }
            public int nroAfiliado { get; set; }
            public int dni { get; set; }
            public int habitacion { get; set; }
            public int telefono { get; set; }
            public string observaciones { get; set; }
            public string Estado { get; set; }
    }

    
}
