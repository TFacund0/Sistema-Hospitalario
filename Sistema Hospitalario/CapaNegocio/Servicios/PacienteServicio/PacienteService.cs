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
        public (bool Ok, int IdGenerado, string Error) Alta(PacienteAltaDto dtoPaciente)
        { 
            try
            {
                using (var bdd = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities())
                {
                    // Obtenemos la lista completa de pacientes
                    var listaPacientes = bdd.paciente.ToList();

                    // Validación de unicidad de DNI
                    bool existeDni = listaPacientes.Any(unPaciente => unPaciente.dni == dtoPaciente.Dni);
                    if (existeDni)
                        return (false, 0, "Ya existe un paciente registrado con ese DNI.");

                    var nombreEstado = (dtoPaciente.EstadoInicial ?? "Activo").Trim();
                    

                    var estado = bdd.estado_paciente.SingleOrDefault(unEstado => unEstado.nombre == nombreEstado);

                    if (estado == null)
                        return (false, 0, $"El estado '{nombreEstado}' no existe en estado_paciente.");

                    var paciente = new paciente
                    {
                        nombre = dtoPaciente.Nombre.Trim(),
                        apellido = dtoPaciente.Apellido.Trim(),
                        dni = dtoPaciente.Dni,
                        fecha_nacimiento = dtoPaciente.FechaNacimiento ?? DateTime.Now,
                        observaciones = dtoPaciente.Observaciones?.Trim(),
                        direccion = dtoPaciente.Direccion?.Trim(),
                        correo_electronico = dtoPaciente.Email?.Trim(),
                        id_estado_paciente = estado.id_estado_paciente   
                    };

                    if (!string.IsNullOrWhiteSpace(dtoPaciente.Telefono))
                    {
                        paciente.telefono.Add(new telefono
                        {
                            numero_telefono = dtoPaciente.Telefono.Trim()
                        });
                    }

                    bdd.paciente.Add(paciente);
                    bdd.SaveChanges();

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
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities())
            {
                return db.paciente
                   .Select(aux_paciente => new PacienteListadoDto
                   {
                       Id = aux_paciente.id_paciente,
                       Paciente = aux_paciente.nombre + " " + aux_paciente.apellido,
                       DNI = aux_paciente.dni,
                       Edad = DbFunctions.DiffYears(aux_paciente.fecha_nacimiento, DateTime.Now) ?? 0,
                       Estado = aux_paciente.estado_paciente.nombre
                   })
                   .ToList();
            }
        }

        // ===================== DETALLE (para ver/editar) =====================
        public PacienteDetalleDto ObtenerDetalle(int id)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities())
            {
                return db.paciente
                         .Where(aux_paciente => aux_paciente.id_paciente == id)
                         .Select(aux_paciente => new PacienteDetalleDto
                         {
                             Id = aux_paciente.id_paciente,
                             Nombre = aux_paciente.nombre,
                             Apellido = aux_paciente.apellido,
                             DNI = aux_paciente.dni,
                             FechaNacimiento = aux_paciente.fecha_nacimiento,
                             Direccion = aux_paciente.direccion,
                             Email = aux_paciente.correo_electronico,
                             Estado = aux_paciente.estado_paciente.nombre,
                             Observaciones = aux_paciente.observaciones,
                             Telefono = aux_paciente.telefono.Select(t => t.numero_telefono).FirstOrDefault()
                         })
                         .FirstOrDefault();
            }
        }

        // ===================== EDITAR (actualizar en BD) =====================
        public (bool Ok, string Error) Editar(PacienteDetalleDto dto)
        {
            try
            {
                using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities())
                {
                    // 1) Traer paciente + teléfonos
                    var pacienteEdit = db.paciente
                                         .Include("telefono")
                                         .SingleOrDefault(p => p.id_paciente == dto.Id);

                    if (pacienteEdit == null)
                        return (false, "Paciente no encontrado.");

                    // 2) Unicidad de DNI
                    bool dniOcupado = db.paciente.Any(p => p.dni == dto.DNI && p.id_paciente != dto.Id);
                    if (dniOcupado)
                        return (false, "Ya existe otro paciente con ese DNI.");

                    // 3) Mapear DTO -> Entidad (solo campos editables)
                    pacienteEdit.nombre = dto.Nombre?.Trim();
                    pacienteEdit.apellido = dto.Apellido?.Trim();
                    pacienteEdit.dni = dto.DNI;
                    pacienteEdit.direccion = dto.Direccion?.Trim();
                    pacienteEdit.fecha_nacimiento = dto.FechaNacimiento;
                    pacienteEdit.correo_electronico = dto.Email?.Trim();
                    pacienteEdit.observaciones = dto.Observaciones?.Trim();

                    // 4) Estado (busca por nombre y asigna el id)
                    int estadoId = db.estado_paciente
                                     .Where(e => e.nombre == dto.Estado)
                                     .Select(e => e.id_estado_paciente)
                                     .FirstOrDefault();

                    if (estadoId == 0)
                        return (false, $"Estado '{dto.Estado}' no encontrado.");
                    pacienteEdit.id_estado_paciente = estadoId;

                    // 5) Teléfono principal (editar el "primero" que mostrás en el detalle)
                    string nuevoTelefono = dto.Telefono?.Trim();

                    // Tomamos el "primero" de forma estable (por id)
                    var telPrincipal = pacienteEdit.telefono.FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(nuevoTelefono))
                    {
                        // Si querés permitir vaciar/eliminar el teléfono principal:
                        // if (telPrincipal != null) db.telefono.Remove(telPrincipal);
                        // En este ejemplo: si está vacío, no tocamos teléfonos.
                    }
                    else
                    {
                        if (telPrincipal == null)
                        {
                            // No tenía teléfonos -> crear uno
                            var nuevo = new telefono
                            {
                                id_paciente = pacienteEdit.id_paciente,
                                numero_telefono = nuevoTelefono
                                // es_principal = true; // si tenés este campo, marcálo
                            };
                            db.telefono.Add(nuevo);
                        }
                        else
                        {
                            // Actualizar el existente
                            telPrincipal.numero_telefono = nuevoTelefono;

                            // Si existe columna es_principal, garantizá que sea el principal:
                            // telPrincipal.es_principal = true;
                        }
                    }

                    db.SaveChanges();
                    return (true, null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el paciente: {ex.Message}");
            }
        }
    }
}
