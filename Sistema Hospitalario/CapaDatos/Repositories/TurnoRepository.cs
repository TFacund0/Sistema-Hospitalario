using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        public TurnoRepository()
        {
        }

        public List<TurnoAgendaDto> ObtenerTurnosParaAgenda(int idMedico, DateTime fecha)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                // 1. Traemos datos "crudos" de la BD
                var turnosCrudos = db.turno
                    .Where(t => t.id_medico == idMedico &&
                                DbFunctions.TruncateTime(t.fecha_turno) == fecha.Date)
                    .OrderBy(t => t.fecha_turno) // Ordenamos por la fecha completa
                    .Select(t => new
                    {
                        IdTurno = t.id_turno,
                        FechaHoraTurno = t.fecha_turno,
                        PacienteNombre = t.paciente.nombre,
                        PacienteApellido = t.paciente.apellido,
                        Estado = t.estado_turno.nombre
                    })
                    .ToList(); // El SQL se ejecuta aquí

                // 2. Convertimos a DTO en C# para formatear la hora
                var resultadoFinal = turnosCrudos
                    .Select(t => new TurnoAgendaDto
                    {
                        IdTurno = t.IdTurno,
                        Hora = t.FechaHoraTurno.ToString("HH:mm"), // Formato 24hs (ej: 09:05)
                        Paciente = t.PacienteNombre + " " + t.PacienteApellido,
                        Estado = t.Estado
                    })
                    .ToList();

                return resultadoFinal;
            }
        }

        public AgendaContadoresDto ObtenerContadoresAgenda(int idMedico, DateTime fecha)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var contadores = db.turno
                    .Where(t => t.id_medico == idMedico &&
                                DbFunctions.TruncateTime(t.fecha_turno) == fecha.Date)
                    .GroupBy(t => t.estado_turno.nombre)
                    .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                    .ToDictionary(k => k.Estado, v => v.Cantidad);

                // ¡IMPORTANTE! Reemplazá "Pendiente", "Completado", etc.,
                // con los nombres EXACTOS de tu tabla 'estado_turno'.
                return new AgendaContadoresDto
                {
                    Pendientes = contadores.ContainsKey("Pendiente") ? contadores["Pendiente"] : 0,
                    Completadas = contadores.ContainsKey("Completado") ? contadores["Completado"] : 0,
                    Canceladas = (contadores.ContainsKey("Cancelado") ? contadores["Cancelado"] : 0) +
                                 (contadores.ContainsKey("Reprogramado") ? contadores["Reprogramado"] : 0)
                };
            }
        }

        public bool ActualizarEstadoTurno(int idTurno, int idNuevoEstado)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var turno = db.turno.Find(idTurno);
                if (turno == null) return false;

                turno.id_estado_turno = idNuevoEstado;
                db.SaveChanges();
                return true;
            }
        }
        // Obtener todos los turnos con detalles
        public List<TurnoDto> GetAll()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnos = from t in db.turno
                             join p in db.paciente on t.id_paciente equals p.id_paciente
                             join m in db.medico on t.id_medico equals m.id_medico
                             join pr in db.procedimiento on t.id_procedimiento equals pr.id_procedimiento
                             join e in db.estado_turno on t.id_estado_turno equals e.id_estado_turno
                             select new TurnoDto
                             {
                                 Id_turno = t.id_turno,
                                 Id_paciente = p.id_paciente,
                                 Id_medico = m.id_medico,
                                 Id_procedimiento = pr.id_procedimiento,
                                 Paciente = p.nombre + " " + p.apellido,
                                 Medico = m.nombre + " " + m.apellido,
                                 Procedimiento = pr.nombre,
                                 FechaTurno = t.fecha_turno,
                                 Correo = p.correo_electronico,
                                 DNI = p.dni.ToString(),
                                 Telefono = t.telefono,
                                 Estado = e.nombre,
                                 Observaciones = t.motivo
                             };
                return turnos.ToList();
            }
        }

        // Obtener detalle de un turno por ID
        public TurnoDto ObtenerDetalle(int id_turno)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var t = db.turno
                    .AsNoTracking()
                    .Where(x => x.id_turno == id_turno)
                    .Select(x => new TurnoDto
                    {
                        Id_turno = x.id_turno,
                        Id_paciente = x.id_paciente,
                        Paciente = x.paciente.nombre + " " + x.paciente.apellido,
                        Id_medico = x.id_medico,
                        Medico = x.medico.apellido + " " + x.medico.nombre,
                        Id_procedimiento = x.id_procedimiento,
                        Procedimiento = x.procedimiento.nombre,
                        FechaRegistro = x.fecha_registracion,
                        FechaTurno = x.fecha_turno,
                        Observaciones = x.motivo,
                        Correo = x.correo_electronico,  
                        Telefono = x.telefono,          
                        Estado = x.estado_turno.nombre
                    })
                    .FirstOrDefault();

                return t;
            }
        }

        // Listar todos los turnos en formato listado
        public List<ListadoTurno> ListadoTurnos()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnos = from t in db.turno
                             join p in db.paciente on t.id_paciente equals p.id_paciente
                             join m in db.medico on t.id_medico equals m.id_medico
                             join pr in db.procedimiento on t.id_procedimiento equals pr.id_procedimiento
                             join e in db.estado_turno on t.id_estado_turno equals e.id_estado_turno
                             select new ListadoTurno
                             {
                                 Id_turno = t.id_turno,
                                 Paciente = p.nombre + " " + p.apellido,
                                 Medico = m.nombre + " " + m.apellido,
                                 Id_medico = m.id_medico,
                                 Procedimiento = pr.nombre,
                                 FechaTurno = t.fecha_turno,
                                 Fecha_Del_Turno = t.fecha_turno,
                                 Estado = e.nombre
                             };
                return turnos.ToList();
            }
        }

        // Insertar un nuevo turno
        public void Insertar(TurnoDto p_turno)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var estado = db.estado_turno
                            .AsNoTracking()
                            .FirstOrDefault(e => e.nombre.ToLower() == "pendiente");

                if (estado == null)
                    throw new Exception("No se encontró el estado 'Programado' en la base de datos.");

                var nuevoTurno = new turno
                {
                    id_paciente = p_turno.Id_paciente,
                    id_medico = p_turno.Id_medico,
                    id_procedimiento = p_turno.Id_procedimiento,
                    fecha_turno = p_turno.FechaTurno,
                    correo_electronico = p_turno.Correo,
                    fecha_registracion = DateTime.Now,
                    telefono = p_turno.Telefono,
                    motivo = p_turno.Observaciones,
                    id_estado_turno = db.estado_turno.AsNoTracking().FirstOrDefault(e => e.nombre.ToLower() == "pendiente").id_estado_turno,
                };

                db.turno.Add(nuevoTurno);
                db.SaveChanges();
            }
        }

        // Actualizar un turno existente
        public void Actualizar(int id_turno, TurnoDto turnoDto)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnoExistente = db.turno.Find(id_turno)
                                     ?? throw new Exception("Turno no encontrado.");

                turnoExistente.id_paciente = turnoDto.Id_paciente;
                turnoExistente.id_medico = turnoDto.Id_medico;
                turnoExistente.id_procedimiento = turnoDto.Id_procedimiento;
                turnoExistente.fecha_turno = turnoDto.FechaTurno;
                turnoExistente.motivo = turnoDto.Observaciones;

                // Actualizar estado SOLO si viene texto en el DTO
                if (!string.IsNullOrWhiteSpace(turnoDto.Estado))
                {
                    // Buscamos el ID del estado por nombre (case-insensitive)
                    var nuevoEstadoId = db.estado_turno
                        .Where(e => e.nombre.Equals(
                            turnoDto.Estado,
                            StringComparison.OrdinalIgnoreCase))
                        .Select(e => e.id_estado_turno)   // ajustá el nombre si la PK se llama distinto
                        .FirstOrDefault();                // si no encuentra, devuelve 0

                    // Si encontramos un estado válido, lo asignamos
                    if (nuevoEstadoId != 0)
                    {
                        turnoExistente.id_estado_turno = nuevoEstadoId; // FK en la tabla turno
                    }
                    // Si no lo encuentra, conserva el estado actual
                }

                // Correo: si viene vacío, lo guardamos como null
                turnoExistente.correo_electronico =
                    string.IsNullOrWhiteSpace(turnoDto.Correo)
                        ? null
                        : turnoDto.Correo.Trim();

                // Teléfono: igual idea
                turnoExistente.telefono =
                    string.IsNullOrWhiteSpace(turnoDto.Telefono)
                        ? null
                        : turnoDto.Telefono.Trim();

                db.SaveChanges();
            }
        }

        // Eliminar un turno por ID
        public void Eliminar(int id_turno)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnoExistente = db.turno.Find(id_turno) ?? throw new Exception("Turno no encontrado.");
                db.turno.Remove(turnoExistente);
                db.SaveChanges();
            }
        }

        // Listar todos los estados de turno
        public List<ListadoEstadoTurno> ListarEstadosTurno()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var estados = from e in db.estado_turno
                              select new ListadoEstadoTurno
                              {
                                  Id_estado = e.id_estado_turno.ToString(),
                                  Estado = e.nombre
                              };
                return estados.ToList();
            }
        }
    }
}
