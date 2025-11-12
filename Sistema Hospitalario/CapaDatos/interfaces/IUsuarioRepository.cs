using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IUsuarioRepository
    {
        (bool Ok, int IdGenerado, string Error) Insertar(string Nombre, string Apellido, string NombreUsuario, int Estado, int Rol, string Password, string correo, int? IdMedico);
        void Eliminar(int IdUsuario);
        List<MostrarUsuariosDTO> ObtenerUsuarios();
        bool ExisteUsername(string username);
        DatosLoginUsuarioDTO ObtenerUsuarioParaLogin(string username);
    }
}
