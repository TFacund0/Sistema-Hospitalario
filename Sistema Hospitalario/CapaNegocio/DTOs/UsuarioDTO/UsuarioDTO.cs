using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO
{
    public class MostrarUsuariosDTO 
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Estado { get; set; }
        public string Rol { get; set; }
        public string Password { get; set; }
        public string Correo { get; set; }
    }
    public class UsuarioAltaDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public int IdEstado { get; set; }     
        public int IdRol { get; set; }      
        public string Password { get; set; }
        public string Correo { get; set; }
        public int? IdMedico { get; set; }
    }

}
