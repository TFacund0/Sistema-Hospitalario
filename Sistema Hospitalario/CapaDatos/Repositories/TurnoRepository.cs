using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        public TurnoRepository()
        {
        }

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
                                 FechaRegistro = t.fecha_registracion,
                                 Observaciones = t.motivo
                             };
                return turnos.ToList();
            }
        }

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
                                 Procedimiento = pr.nombre,
                                 FechaTurno = t.fecha_turno,
                                 Estado = e.nombre
                             };
                return turnos.ToList();
            }
        }

        public void Insertar(TurnoDto p_turno)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var nuevoTurno = new turno
                {
                    id_paciente = p_turno.Id_paciente,
                    id_medico = p_turno.Id_medico,
                    id_procedimiento = p_turno.Id_procedimiento,
                    fecha_turno = p_turno.FechaTurno,
                    fecha_registracion = p_turno.FechaRegistro,
                    motivo = p_turno.Observaciones,
                    id_estado_turno = 6
                };

                db.turno.Add(nuevoTurno);
                db.SaveChanges();
            }
        }

        public void Actualizar(int id_turno, TurnoDto turnoDto)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnoExistente = db.turno.Find(id_turno) ?? throw new Exception("Turno no encontrado.");
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
        public void Eliminar(int id_turno)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var turnoExistente = db.turno.Find(id_turno) ?? throw new Exception("Turno no encontrado.");
                db.turno.Remove(turnoExistente);
                db.SaveChanges();
            }
        }
    }
}
