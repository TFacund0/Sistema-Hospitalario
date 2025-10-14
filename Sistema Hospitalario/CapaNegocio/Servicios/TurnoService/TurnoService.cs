using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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
                                 Paciente = p.nombre + " " + p.apellido,
                                 Medico = m.nombre + " " + m.apellido,
                                 Procedimiento = pr.nombre,
                                 FechaTurno = t.fecha_turno,
                                 Estado = e.nombre
                             };
                return turnos.ToList();
            }
        }
    }
}
