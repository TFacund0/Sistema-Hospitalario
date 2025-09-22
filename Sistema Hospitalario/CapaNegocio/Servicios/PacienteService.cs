using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sistema_Hospitalario.CapaNegocio.Servicios
{
    public class PacienteService
    {
        public (bool Ok, int IdGenerado, string Error) Alta(PacienteAltaDto dto)
        {
            // ===================== Validaciones de negocio =====================
            if (dto == null)
                return (false, 0, "No se recibieron datos del paciente.");

            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length > 50)
                return (false, 0, "El nombre es obligatorio y debe tener hasta 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Apellido) || dto.Apellido.Length > 50)
                return (false, 0, "El apellido es obligatorio y debe tener hasta 50 caracteres.");

            if (!string.IsNullOrEmpty(dto.Telefono) && dto.Telefono.Length > 15)
                return (false, 0, "El teléfono no puede superar 15 caracteres.");

            if (!string.IsNullOrEmpty(dto.Direccion) && dto.Direccion.Length > 50)
                return (false, 0, "La dirección no puede superar 50 caracteres.");

            if (!string.IsNullOrEmpty(dto.Observaciones) && dto.Observaciones.Length > 200)
                return (false, 0, "Las observaciones no pueden superar 200 caracteres.");

            if (dto.Dni <= 0)
                return (false, 0, "El DNI es obligatorio y debe ser un número positivo.");

            // ===================== Persistencia =====================
            try
            {
                using (var ctx = new Sistema_Hospitalario.CapaDatos.Sistema_Hospitalario())
                {
                    // Validación de unicidad de DNI
                    bool existeDni = ctx.paciente.Any(p => p.dni == dto.Dni);
                    if (existeDni)
                        return (false, 0, "Ya existe un paciente registrado con ese DNI.");

                    // Mapear DTO -> Entidad EF
                    var paciente = new paciente
                    {
                        nombre = dto.Nombre.Trim(),
                        apellido = dto.Apellido.Trim(),
                        dni = dto.Dni,
                        telefono = dto.Telefono?.Trim(),
                        direccion = dto.Direccion?.Trim(),
                        obra_social = dto.ObraSocial?.Trim(),
                        nro_afiliado = dto.NumeroAfiliado,
                        estado = dto.EstadoInicial?.Trim(),
                        observaciones = dto.Observaciones?.Trim(),
                        fecha_nacimiento = dto.FechaNacimiento ?? DateTime.Now,
                    };

                    // Guardar en BD
                    ctx.paciente.Add(paciente);
                    ctx.SaveChanges();

                    return (true, paciente.id_paciente, null);
                }
            }
            catch (Exception ex)
            {
                return (false, 0, $"Error al guardar el paciente: {ex.Message}");
            }
        }

        // ===================== LISTADO (para el DataGridView) =====================
        public List<PacienteListadoDto> ListarPacientes()
        {
            using (var ctx = new Sistema_Hospitalario.CapaDatos.Sistema_Hospitalario())
            {
                return ctx.paciente
                   .AsNoTracking()
                   .Select(p => new PacienteListadoDto
                   {
                       Id = p.id_paciente,
                       Paciente = p.nombre + " " + p.apellido,
                       DNI = p.dni,
                       Edad = DbFunctions.DiffYears(p.fecha_nacimiento, DateTime.Now) ?? 0,
                       Estado = p.estado
                   })
                   .ToList();
            }
        }

        // ===================== DETALLE (para ver/editar) =====================
        public PacienteDetalleDto ObtenerDetalle(int id)
        {
            using (var ctx = new Sistema_Hospitalario.CapaDatos.Sistema_Hospitalario())
            {
                return ctx.paciente
                         .AsNoTracking()
                         .Where(p => p.id_paciente == id)
                         .Select(p => new PacienteDetalleDto
                         {
                             Id = p.id_paciente,
                             Nombre = p.nombre,
                             Apellido = p.apellido,
                             DNI = p.dni,
                             FechaNacimiento = p.fecha_nacimiento,
                             Telefono = p.telefono,       // string en entidad
                             Direccion = p.direccion,
                             Estado = p.estado,
                             ObraSocial = p.obra_social,    // texto
                             NumeroAfiliado = p.nro_afiliado,   // texto
                             Observaciones = p.observaciones
                         })
                         .FirstOrDefault();
            }
        }

        // ===================== EDITAR (actualizar en BD) =====================
        public (bool Ok, string Error) Editar(PacienteDetalleDto dto)
        {
            // -------- Validaciones básicas (alineadas a tus reglas) --------
            if (dto == null) return (false, "No se recibieron datos del paciente.");
            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length > 50)
                return (false, "El nombre es obligatorio y debe tener hasta 50 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.Apellido) || dto.Apellido.Length > 50)
                return (false, "El apellido es obligatorio y debe tener hasta 50 caracteres.");
            //if (string.IsNullOrWhiteSpace(dto.DNI) || dto.DNI.Length > 15 || !dto.DNI.All(char.IsDigit))
              //  return (false, "El DNI es obligatorio, numérico y de hasta 15 dígitos.");
            if (!string.IsNullOrEmpty(dto.Telefono) && dto.Telefono.Length > 15)
                return (false, "El teléfono no puede superar 15 caracteres.");
            if (!string.IsNullOrEmpty(dto.Direccion) && dto.Direccion.Length > 50)
                return (false, "La dirección no puede superar 50 caracteres.");
            if (!string.IsNullOrEmpty(dto.Observaciones) && dto.Observaciones.Length > 200)
                return (false, "Las observaciones no pueden superar 200 caracteres.");
            //if (!string.IsNullOrEmpty(dto.NumeroAfiliado) && (dto.NumeroAfiliado.Length > 20 || !dto.NumeroAfiliado.All(char.IsDigit)))
              //  return (false, "El N° de afiliado debe ser numérico y de hasta 20 dígitos.");
            if (dto.FechaNacimiento > DateTime.Today)
                return (false, "La fecha de nacimiento no puede ser futura.");

            if (dto.DNI <= 0)
                return (false, "El DNI no es válido.");

            try
            {
                using (var ctx = new Sistema_Hospitalario.CapaDatos.Sistema_Hospitalario())
                {
                    var ent = ctx.paciente.SingleOrDefault(p => p.id_paciente == dto.Id);
                    if (ent == null) return (false, "Paciente no encontrado.");

                    // Unicidad de DNI (excluye al propio paciente)
                    bool dniOcupado = ctx.paciente.Any(p => p.dni == dto.DNI && p.id_paciente != dto.Id);
                    if (dniOcupado) return (false, "Ya existe otro paciente con ese DNI.");

                    // -------- Mapear DTO -> Entidad (solo campos editables) --------
                    ent.nombre = dto.Nombre?.Trim();
                    ent.apellido = dto.Apellido?.Trim();
                    ent.dni = dto.DNI;                         // int en entidad
                    ent.telefono = dto.Telefono?.Trim();
                    ent.direccion = dto.Direccion?.Trim();
                    ent.obra_social = dto.ObraSocial?.Trim();
                    ent.nro_afiliado = dto.NumeroAfiliado;
                    ent.estado = dto.Estado?.Trim();
                    ent.fecha_nacimiento = dto.FechaNacimiento;

                    ctx.SaveChanges();
                    return (true, null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el paciente: {ex.Message}");
            }
        }

        // (Opcional) Helper si alguna vez lo necesitás
        private static int CalcularEdad(DateTime fechaNac)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNac.Year;
            if (fechaNac.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }
    }
}
