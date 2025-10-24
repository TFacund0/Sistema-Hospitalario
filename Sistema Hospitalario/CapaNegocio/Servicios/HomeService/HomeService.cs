using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Sistema_Hospitalario.CapaNegocio.DTOs.HomeDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HomeService
{
    public class HomeService
    {
        public async Task<List<HomeDto>> ListarActividadRecienteAsync(int cantidad = 50)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities())
            {
                // 1) Turnos
                var turnos = db.turno
                    .AsNoTracking()
                    .Select(t => new HomeDto
                    {
                        Nombre = t.paciente.nombre,
                        Apellido = t.paciente.apellido,
                        Accion = "Turno registrado",
                        Horario = t.fecha_registracion,
                        Tipo = t.procedimiento.nombre // si FK es opcional: Tipo = t.procedimiento != null ? t.procedimiento.nombre : null
                    });

                // 2) Internaciones
                var internaciones = db.internacion
                    .AsNoTracking()
                    .Select(i => new HomeDto
                    {
                        Nombre = i.paciente.nombre,
                        Apellido = i.paciente.apellido,
                        Accion = "Internación registrada",
                        Horario = i.fecha_inicio,      // o i.fecha_registracion si la tenés
                        Tipo = i.procedimiento != null ? i.procedimiento.nombre : null
                    });

                // 3) Pacientes registrados
                var pacientes = db.paciente
                    .AsNoTracking()
                    .Select(p => new HomeDto
                    {
                        Nombre = p.nombre,
                        Apellido = p.apellido,
                        Accion = "Paciente registrado",
                        Horario = p.fecha_registracion, // agrega esta columna si no existe
                        Tipo = null
                    });

                // Unificamos, ordenamos por más reciente y limitamos
                var query = turnos
                    .Concat(internaciones)
                    .Concat(pacientes)
                    .OrderByDescending(x => x.Horario)
                    .Take(cantidad);

                return await query.ToListAsync();
            }
        }
    }
}
