using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;
using Sistema_Hospitalario.CapaDatos.Repositories;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService
{
    public class PacienteService
    {
        private readonly PacienteRepository _repo = new PacienteRepository();

        public PacienteService()
        {
        }

        // Obtener todas los pacientes
        public List<PacienteDto> ObtenerPacientes()
        {
            return _repo.GetAll();
        }

        // ===================== ALTA (guardar en BD) =====================
        public (bool Ok, int IdGenerado, string Error) Alta(PacienteAltaDto dtoPaciente)
        {
            try
            {
                var listaPacientes = this.ObtenerPacientes();
                    
                // Validación de unicidad de DNI
                bool existeDni = listaPacientes.Any(unPaciente => unPaciente.Dni == dtoPaciente.Dni);
                
                if (existeDni)
                    return (false, 0, "Ya existe un paciente registrado con ese DNI.");
                    
                // Obtener el estado inicial del paciente o usar "Activo" por defecto
                var nombreEstado = (dtoPaciente.EstadoInicial ?? "activo").Trim().ToLower();

                // Buscar el estado en la base de datos
                var listaEstados = this._repo.GetEstados();
                // Buscar el estado por nombre y obtener us id
                var estado = listaEstados.FirstOrDefault(e => e.Nombre.ToLower() == nombreEstado.ToLower());

                if (estado == null)
                        return (false, 0, $"El estado '{nombreEstado}' no existe en estado_paciente.");

                // Crear el nuevo registro del paciente
                PacienteDto paciente = new PacienteDto()
                    {
                        Nombre = dtoPaciente.Nombre.Trim(),
                        Apellido = dtoPaciente.Apellido.Trim(),
                        Dni = dtoPaciente.Dni,
                        Fecha_nacimiento = dtoPaciente.FechaNacimiento ?? DateTime.Now,
                        Observaciones = dtoPaciente.Observaciones?.Trim(),
                        Direccion = dtoPaciente.Direccion?.Trim(),
                        Telefono = dtoPaciente.Telefono,
                        Email = dtoPaciente.Email?.Trim(),
                        Id_estado_paciente = estado.Id,
                        Estado_paciente = estado.Nombre.ToLower()
                };

                if (!string.IsNullOrWhiteSpace(dtoPaciente.Telefono))
                {
                    paciente.Telefono = dtoPaciente.Telefono.Trim();
                }

                _repo.Insertar(paciente);
                return (true, paciente.Id, null);
            }
            catch (Exception ex)
            {
                // Creamos un mensaje de error más detallado
                string errorMessage = ex.Message;

                // Verificamos si hay una excepción interna (el mensaje de la base de datos)
                if (ex.InnerException != null)
                {
                    errorMessage += " --> Inner Exception: " + ex.InnerException.Message;
                }

                // Devolvemos el mensaje completo
                return (false, 0, $"Error al guardar el paciente: {errorMessage}");
            }
        }

        // ===================== EDITAR (actualizar en BD) =====================
        public (bool Ok, string Error) Editar(PacienteDetalleDto dto)
        {
            try
            {
                // 1) Paciente existente
                    var pacienteEdit = _repo.GetAll().Any(p => p.Id == dto.Id) 
                        ? _repo.GetAll().FirstOrDefault(p => p.Id == dto.Id) 
                        : null;

                if (pacienteEdit == null)
                        return (false, "Paciente no encontrado.");

                // 2) Unicidad de DNI
                bool dniOcupado = _repo.GetAll().Any(p => p.Dni == dto.DNI && p.Id != dto.Id);
                if (dniOcupado)
                    return (false, "Ya existe otro paciente con ese DNI.");

                // 3) Mapear DTO -> Entidad (solo campos editables)
                pacienteEdit.Nombre = dto.Nombre?.Trim();
                pacienteEdit.Apellido = dto.Apellido?.Trim();
                pacienteEdit.Dni = dto.DNI;
                pacienteEdit.Direccion = dto.Direccion?.Trim();
                pacienteEdit.Fecha_nacimiento = dto.FechaNacimiento;
                pacienteEdit.Email = dto.Email?.Trim();
                pacienteEdit.Observaciones = dto.Observaciones?.Trim();
                pacienteEdit.Telefono = dto.Telefono.Trim();

                // 4) Estado (busca por nombre y asigna el id)
                var listaEstados = this._repo.GetEstados();
                int estadoId = listaEstados
                    .Where(e => e.Nombre == dto.Estado)   // <- usar dto.Estado
                    .Select(e => e.Id)
                    .FirstOrDefault();

                if (estadoId == 0)
                    return (false, $"Estado '{dto.Estado}' no encontrado.");

                pacienteEdit.Id_estado_paciente = estadoId;

                _repo.Actualizar(pacienteEdit.Id, pacienteEdit);
                
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el paciente: {ex.Message}");
            }
        }

        // ===================== CONTAR CANTIDAD PACIENTES (por estado) =====================
        public int ContarPorEstadoId(string estado)
        {
            var listaPacientes = this.ObtenerPacientes();
            return listaPacientes.Where(p => p.Estado_paciente.ToLower() == estado.ToLower()).Count();
        }

        // ===================== DETALLE (para ver/editar) =====================
        public PacienteDetalleDto ObtenerDetalle(int p_id_paciente)
        {
            var listaPacientes = this.ObtenerPacientes();
            return listaPacientes
                .Where(p => p.Id == p_id_paciente)
                .Select(p => new PacienteDetalleDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    DNI = p.Dni,
                    FechaNacimiento = p.Fecha_nacimiento,
                    Direccion = p.Direccion,
                    Email = p.Email,
                    Estado = p.Estado_paciente,
                    Observaciones = p.Observaciones,
                    Telefono = p.Telefono
                })
                .FirstOrDefault();
        }

        // ===================== LISTADO (para el DataGridView) =====================
        public List<PacienteDto> ListarPacientes()
        {
            var listaPacientes = this.ObtenerPacientes();
            return listaPacientes
                .Where(p => p.Estado_paciente.ToLower() == "activo" || p.Estado_paciente.ToLower() == "internado")
                .Select(p => new PacienteDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Dni = p.Dni,
                    Estado_paciente = p.Estado_paciente,
                    Fecha_nacimiento = p.Fecha_nacimiento
                })
                .ToList();
        }

        // ===================== LISTADO PACIENTES EGRESADOS =====================
        public List<PacienteDto> ListarPacienteEgresados()
        {
            var listaPacientes = this.ObtenerPacientes();
            return listaPacientes
                .Where(p => p.Estado_paciente.ToLower() == "alta")
                .Select(p => new PacienteDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Dni = p.Dni,
                    Estado_paciente = p.Estado_paciente,
                    Fecha_nacimiento = p.Fecha_nacimiento
                })
                .ToList();
        }

        // ===================== LISTADO COMPLETO PACIENTES =====================
        public List<PacienteDto> ListadoPacientesDGV()
        {
            var listaPacientes = this.ObtenerPacientes();
            return listaPacientes
                .Select(p => new PacienteDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Dni = p.Dni,
                    Estado_paciente = p.Estado_paciente,
                    Fecha_nacimiento = p.Fecha_nacimiento
                })
                .ToList();
        }
    }
}