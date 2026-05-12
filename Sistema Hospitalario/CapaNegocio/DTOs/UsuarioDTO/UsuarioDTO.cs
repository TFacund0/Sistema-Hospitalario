using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO
{
    /// <summary>
    /// DTO diseñado para listar los usuarios en las pantallas de administración de seguridad.
    /// </summary>
    public class MostrarUsuariosDTO 
    {
        /// <summary>ID único del usuario.</summary>
        public int IdUsuario { get; set; }

        /// <summary>Nombre real de la persona.</summary>
        public string Nombre { get; set; }

        /// <summary>Apellido real de la persona.</summary>
        public string Apellido { get; set; }

        /// <summary>Nombre de usuario (Login).</summary>
        public string NombreUsuario { get; set; }

        /// <summary>Estado de la cuenta (ej. 'Activo', 'Bloqueado').</summary>
        public string Estado { get; set; }

        /// <summary>Nombre del rol asignado.</summary>
        public string Rol { get; set; }

        /// <summary>Contraseña (generalmente omitida o devuelta como máscara en listados).</summary>
        public string Password { get; set; }

        /// <summary>Correo electrónico asociado.</summary>
        public string Correo { get; set; }
    }

    /// <summary>
    /// DTO que encapsula los datos necesarios para registrar un nuevo usuario en el sistema.
    /// </summary>
    public class UsuarioAltaDTO
    {
        /// <summary>Nombre del nuevo usuario.</summary>
        public string Nombre { get; set; }

        /// <summary>Apellido del nuevo usuario.</summary>
        public string Apellido { get; set; }

        /// <summary>Nombre de cuenta (Login).</summary>
        public string NombreUsuario { get; set; }

        /// <summary>ID del estado inicial (ej. 1 para Activo).</summary>
        public int IdEstado { get; set; }     

        /// <summary>ID del rol a asignar (ej. 1 para Administrador, 2 para Médico).</summary>
        public int IdRol { get; set; }      

        /// <summary>Contraseña en texto plano (será hasheada antes de persistir).</summary>
        public string Password { get; set; }

        /// <summary>Correo electrónico del usuario.</summary>
        public string Correo { get; set; }

        /// <summary>ID del médico asociado (si el rol es Médico).</summary>
        public int? IdMedico { get; set; }
    }

}
