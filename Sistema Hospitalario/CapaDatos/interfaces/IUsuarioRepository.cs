using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;


public interface IUsuarioRepository
{
    (bool Ok, int IdGenerado, string Error) Insertar(string Nombre, string Apellido, string NombreUsuario, int Estado, int Rol, string Password, string correo);
   void Eliminar(int IdUsuario);
   List<MostrarUsuariosDTO> ObtenerUsuarios();
}

