using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO;

namespace Sistema_Hospitalario.CapaNegocio // O el namespace que uses
{
    public static class SesionUsuario
    {
        public static int IdUsuario { get; private set; }
        public static string Username { get; private set; }
        public static string NombreRol { get; private set; }
        public static int? IdMedicoAsociado { get; private set; }
        public static bool IsLoggedIn { get; private set; } = false;

        public static void Login(UsuarioLoginResultadoDTO datosUsuario)
        {
            if (datosUsuario != null && datosUsuario.LoginExitoso)
            {
                IdUsuario = datosUsuario.IdUsuario;
                Username = datosUsuario.Username;
                NombreRol = datosUsuario.NombreRol;
                IdMedicoAsociado = datosUsuario.IdMedicoAsociado;
                IsLoggedIn = true;
            }
        }

        public static void Logout()
        {
            IdUsuario = 0;
            Username = null;
            NombreRol = null;
            IdMedicoAsociado = null;
            IsLoggedIn = false;
        }
    }
}
