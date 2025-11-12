using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO
{
    // DTO auxiliar solo para la comunicación interna
    public class DatosLoginUsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string PasswordHashAlmacenado { get; set; }
        public string NombreRol { get; set; }
        public int? IdMedicoAsociado { get; set; }
    }
}
