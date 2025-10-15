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

        public TurnoDTO ObtenerDetalle(int p_id_turno)
        {
            TurnoDTO turno = null;

            using (var db = new Sistema_HospitalarioEntities())
            {
                turno = (from t in db.turno
                         where t.id_turno == p_id_turno
                         select new TurnoDTO
                         {
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
