using Sistema_Hospitalario.CapaDatos.Interfaces; // Asegurate que el using sea correcto
using Sistema_Hospitalario.CapaNegocio.DTOs.UsuarioDTO; // O donde estén tus DTOs
using Sistema_Hospitalario.CapaDatos.Repositories;
using System; // Necesario para Exception
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // Necesario para hashing
using System.Text; // Necesario para hashing


namespace Sistema_Hospitalario.CapaNegocio.Servicios.UsuarioService
{
    public class UsuarioService
    {        
        private readonly IUsuarioRepository _repo;

        public UsuarioService()
        {
            _repo = new UsuarioRepository();
        }

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        

        public List<MostrarUsuariosDTO> ObtenerUsuarios(string campo = null, string valor = null)
        {
            var listaMaestra = _repo.ObtenerUsuarios();
            List<MostrarUsuariosDTO> resultado;

            // 2. Filtrado (si hay valor)
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

            // 3. Ordenamiento (si no hay valor)
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
        public void EliminarUsuario(int idUsuario)
        {
            if (idUsuario == 1)
            {
                throw new Exception("No se puede eliminar al usuario Administrador principal.");
            }

            _repo.Eliminar(idUsuario);
        }

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

        public UsuarioLoginResultadoDTO ValidarCredenciales(string username, string passwordIngresada)
        {
            // 1. Hasheamos la contraseña que el usuario escribió
            string hashedPasswordIngresada = HashPassword(passwordIngresada);

            // 2. Pedimos los datos al Repositorio
            var datosUsuario = _repo.ObtenerUsuarioParaLogin(username);

            // 3. Verificamos si existe y si el hash coincide
            if (datosUsuario != null && datosUsuario.PasswordHashAlmacenado == hashedPasswordIngresada)
            {
                // ¡Éxito!
                return new UsuarioLoginResultadoDTO
                {
                    LoginExitoso = true,
                    IdUsuario = datosUsuario.IdUsuario,
                    Username = datosUsuario.Username,
                    NombreRol = datosUsuario.NombreRol,
                    IdMedicoAsociado = datosUsuario.IdMedicoAsociado
                };
            }
            else
            {
                // Falla
                return new UsuarioLoginResultadoDTO { LoginExitoso = false };
            }
        }

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

    }
}
