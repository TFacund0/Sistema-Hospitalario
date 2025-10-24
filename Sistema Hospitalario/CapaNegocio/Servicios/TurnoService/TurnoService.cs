using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;   
using System.Data.Entity;

using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService
{
    public class TurnoService
    {
        // Listar todos los turnos con detalles
        public List<ListadoTurno> ListarTurnos ()
        {
            using (var db = new Sistema_HospitalarioEntities())
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
                                 Procedimiento = pr.nombre,
                                 FechaTurno = t.fecha_turno,
                                 Estado = e.nombre
                             };
                return turnos.ToList();
            }
        }

        // Contar turnos por estado, con manejo especial para "Pendiente" (solo los de hoy)
        public int CantidadTurnosPorEstado(string estado)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var count = 0;

                if (estado.Equals("Pendiente"))
                {
                    count = (from t in db.turno
                             join e in db.estado_turno on t.id_estado_turno equals e.id_estado_turno
                             where e.nombre == estado && DbFunctions.TruncateTime(t.fecha_turno) == DbFunctions.TruncateTime(DateTime.Now)
                             select t).Count();
                    return count;
                }

                count = (from t in db.turno
                             join e in db.estado_turno on t.id_estado_turno equals e.id_estado_turno
                             where e.nombre == estado
                             select t).Count();

                return count;
            }
        }

        // Contar todos los turnos pendientes (sin importar la fecha)
        public int CantidadTurnosPendientes()
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var count = (from t in db.turno
                             join e in db.estado_turno on t.id_estado_turno equals e.id_estado_turno
                             where e.nombre == "Pendiente"
                             select t).Count();
                return count;
            }
        }

        // Registrar un nuevo turno
        public void RegistrarTurno(TurnoDto turnoDto)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var nuevoTurno = new turno
                {
                    id_paciente = turnoDto.Id_paciente,
                    id_medico = turnoDto.Id_medico,
                    id_procedimiento = turnoDto.Id_procedimiento,
                    fecha_turno = turnoDto.FechaTurno,
                    fecha_registracion = turnoDto.FechaRegistro,
                    motivo = turnoDto.Observaciones,
                    id_estado_turno = 6
                };

                db.turno.Add(nuevoTurno);
                db.SaveChanges();
            }
        }

        // Actualizar un turno existente
        public void ActualizarTurno(int id_turno, TurnoDTO turnoDto)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var turnoExistente = db.turno.Find(id_turno);
                if (turnoExistente == null)
                {
                    throw new Exception("Turno no encontrado.");
                }
                turnoExistente.id_paciente = turnoDto.Id_paciente;
                turnoExistente.id_medico = turnoDto.Id_medico;
                turnoExistente.id_procedimiento = turnoDto.Id_procedimiento;
                turnoExistente.fecha_turno = turnoDto.FechaTurno;
                turnoExistente.motivo = turnoDto.Observaciones;

                bool correoActivo = !string.IsNullOrEmpty(turnoDto.Correo);
                bool telefonoActivo = !string.IsNullOrEmpty(turnoDto.Telefono);

                if (correoActivo == true && telefonoActivo == true)
                {
                    turnoExistente.correo_electronico = turnoDto.Correo;
                    turnoExistente.telefono = turnoDto.Telefono;
                }
                else if (correoActivo == true && telefonoActivo == false)
                {
                    turnoExistente.correo_electronico = turnoDto.Correo;
                }
                else if (correoActivo == false && telefonoActivo == true)
                {
                    turnoExistente.telefono = turnoDto.Telefono;
                }
                
                db.SaveChanges();
            }
        }

        // Eliminar un turno por ID
        public void EliminarTurno(int id_turno)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var turnoExistente = db.turno.Find(id_turno);
                if (turnoExistente == null)
                {
                    throw new Exception("Turno no encontrado.");
                }
                db.turno.Remove(turnoExistente);
                db.SaveChanges();
            }
        }

        // Obtener detalles de un turno por ID
        public TurnoDTO ObtenerDetalle(int p_id_turno)
        {
            TurnoDTO turno = null;

            using (var db = new Sistema_HospitalarioEntities())
            {
                turno = (from t in db.turno
                         where t.id_turno == p_id_turno
                         select new TurnoDTO
                         {
                             Id_turno = t.id_turno,
                             Id_paciente = t.id_paciente,
                             Paciente = t.paciente.nombre + " " + t.paciente.apellido,
                             Id_medico = t.id_medico,
                             Medico = t.medico.nombre + " " + t.medico.apellido,
                             Id_procedimiento = t.id_procedimiento,
                             Procedimiento = t.procedimiento.nombre,
                             DNI = t.paciente.dni.ToString(),
                             Telefono = t.telefono,
                             Correo = t.correo_electronico,
                             FechaTurno = t.fecha_turno,
                             FechaRegistro = t.fecha_registracion,
                             Observaciones = t.motivo
                         }).FirstOrDefault();
            }

            return turno;
        }
    }
}
