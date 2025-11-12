using Sistema_Hospitalario.CapaDatos.Interfaces; // Asegurate que el using sea correcto
using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO; // O donde estén tus DTOs
using System; // Necesario para Exception
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // Necesario para hashing
using System.Text; // Necesario para hashing

namespace Sistema_Hospitalario.CapaNegocio.Servicios.UsuarioService
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;

        public UsuarioService()
        {
            _repo = new UsuarioRepository();
        }

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = new UsuarioRepository();
        }

        // Obtener usuarios con filtrado y ordenamiento
        public List<MostrarUsuariosDTO> ObtenerUsuarios(string campo = null, string valor = null)
        {
            var listaMaestra = _repo.ObtenerUsuarios();
            List<MostrarUsuariosDTO> resultado;

            // Filtrado (si hay valor)
            if (!string.IsNullOrEmpty(valor))
            {
                string valorLower = valor.ToLower();
                switch (campo)
                {
                    case "NombreUsuario":
                        resultado = listaMaestra.Where(u => u.NombreUsuario.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Nombre":
                        resultado = listaMaestra.Where(u => u.Nombre.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Apellido":
                        resultado = listaMaestra.Where(u => u.Apellido.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "correo":
                        resultado = listaMaestra.Where(u => u.Correo.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Rol":
                        resultado = listaMaestra.Where(u => u.Rol.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Estado":
                        resultado = listaMaestra.Where(u => u.Estado.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    default:
                        resultado = listaMaestra.Where(u => u.IdUsuario.ToString() == valor).ToList();
                        break;
                }
            }
            else
            {
                resultado = listaMaestra;
            }

            // Ordenamiento (si no hay valor)
            if (string.IsNullOrEmpty(valor))
            {
                switch (campo)
                {
                    case "NombreUsuario":
                        resultado = resultado.OrderBy(u => u.NombreUsuario).ToList();
                        break;
                    case "Nombre":
                        resultado = resultado.OrderBy(u => u.Nombre).ToList();
                        break;
                    case "Apellido":
                        resultado = resultado.OrderBy(u => u.Apellido).ToList();
                        break;
                    case "correo":
                        resultado = resultado.OrderBy(u => u.Correo).ToList();
                        break;
                    case "Rol":
                        resultado = resultado.OrderBy(u => u.Rol).ToList();
                        break;
                    case "Estado":
                        resultado = resultado.OrderBy(u => u.Estado).ToList();
                        break;
                    default:
                        resultado = resultado.OrderBy(u => u.IdUsuario).ToList();
                        break;
                }
            }
            return resultado;
        }

        // Agregar un nuevo usuario
        public (bool Ok, int IdGenerado, string Error) AgregarUsuario(UsuarioAltaDTO dto)
        {
            if (_repo.ExisteUsername(dto.NombreUsuario))
            {
                return (false, 0, $"El nombre de usuario '{dto.NombreUsuario}' ya está en uso.");
            }

            string hashedPassword = HashPassword(dto.Password);

            return _repo.Insertar(
                dto.Nombre,
                dto.Apellido,
                dto.NombreUsuario,
                dto.IdEstado,
                dto.IdRol,
                hashedPassword,
                dto.Correo,
                dto.IdMedico

            );
        }

        // Eliminar un usuario
        public void EliminarUsuario(int idUsuario)
        {
            if (idUsuario == 1)
            {
                throw new Exception("No se puede eliminar al usuario Administrador principal.");
            }

            _repo.Eliminar(idUsuario);
        }

        // Hashing de contraseñas usando SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Obtener conteo de usuarios por rol
        public Dictionary<string, int> ObtenerConteoUsuariosPorRol()
        {
            var todosLosUsuarios = _repo.ObtenerUsuarios();

            var conteoPorRol = todosLosUsuarios
                .GroupBy(u => u.Rol)
                .Select(grupo => new
                {
                    NombreRol = grupo.Key,
                    Cantidad = grupo.Count()
                })
                .ToDictionary(item => item.NombreRol, item => item.Cantidad);
            return conteoPorRol;
        }

        internal UsuarioLoginResultadoDTO ValidarCredenciales(string usuario, string contraseña)
        {
            string hashedPasswordIngresada = HashPassword(contraseña);

            var datosUsuario = _repo.ObtenerUsuarioParaLogin(usuario);

            // 3. Verificamos si el usuario existe y la contraseña coincide
            if (datosUsuario != null && datosUsuario.PasswordHashAlmacenado == hashedPasswordIngresada)
            {
                // ¡Éxito! Devolvemos los datos necesarios
                return new UsuarioLoginResultadoDTO
                {
                    LoginExitoso = true,
                    IdUsuario = datosUsuario.IdUsuario,
                    Username = datosUsuario.Username,
                    NombreRol = datosUsuario.NombreRol,
                    IdMedicoAsociado = datosUsuario.IdMedicoAsociado // Puede ser null
                };
            }
            else
            {
                // Falla (usuario no encontrado o contraseña incorrecta)
                return new UsuarioLoginResultadoDTO { LoginExitoso = false };

            }
        }
    }
}
