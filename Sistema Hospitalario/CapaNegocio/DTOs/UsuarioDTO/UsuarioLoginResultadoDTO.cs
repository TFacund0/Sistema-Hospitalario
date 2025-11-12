using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO
{
    public class UsuarioLoginResultadoDTO
    {
        public bool LoginExitoso { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string NombreRol { get; set; }
        public int? IdMedicoAsociado { get; set; } // Nullable int
    }
}
