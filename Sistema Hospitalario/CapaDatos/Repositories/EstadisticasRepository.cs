using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class EstadisticasRepository
    {
        // Contar turnos por estado en una fecha específica
        public int ContarPacientesPorEstadoYFecha(string estado, DateTime fecha)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.paciente
                    .Count(p =>
                        DbFunctions.TruncateTime(p.fecha_registracion) == fecha &&
                        p.estado_paciente.nombre.ToLower() == estado.ToLower());
            }
        }

        // Contar camas por disponibilidad
        public int ContarCamasPorDisponibilidad(string disponibilidad)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.cama
                    .Count(c => c.estado_cama.disponibilidad.ToLower() == disponibilidad.ToLower());
            }
        }

        // Obtener conteo de turnos por día en un rango de fechas
        public Dictionary<DateTime, int> ObtenerConteoTurnosPorDia(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                // truncamos fecha para comparar solo día
                var query = db.turno
                    .Where(t =>
                        DbFunctions.TruncateTime(t.fecha_registracion) >= fechaInicio.Date &&
                        DbFunctions.TruncateTime(t.fecha_registracion) <= fechaFin.Date)
                    .GroupBy(t => DbFunctions.TruncateTime(t.fecha_registracion))
                    .Select(g => new
                    {
                        Fecha = g.Key.Value,
                        Cantidad = g.Count()
                    })
                    .ToList();

                // pasamos a diccionario Fecha -> Cantidad
                var dic = new Dictionary<DateTime, int>();

                foreach (var item in query)
                {
                    dic[item.Fecha.Date] = item.Cantidad;
                }

                return dic;
            }
        }

        // Obtener distribución de turnos por estado en un rango de fechas
        public Dictionary<string, int> ObtenerDistribucionEstadosTurnos(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var query = db.turno
                    .Where(t =>
                        DbFunctions.TruncateTime(t.fecha_registracion) >= fechaInicio.Date &&
                        DbFunctions.TruncateTime(t.fecha_registracion) <= fechaFin.Date)
                    .GroupBy(t => t.estado_turno.nombre)
                    .Select(g => new
                    {
                        Estado = g.Key,
                        Cantidad = g.Count()
                    })
                    .ToList();

                var dic = new Dictionary<string, int>();

                foreach (var item in query)
                {
                    if (item.Estado == null) continue;

                    string clave = item.Estado.ToLower();
                    dic[clave] = item.Cantidad;
                }

                return dic;
            }
        }

        // Obtener conteo de pacientes registrados por día en un rango de fechas
        public Dictionary<DateTime, int> ObtenerConteoPacientesRegistradosPorDia(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var query = db.paciente
                    .Where(p =>
                        DbFunctions.TruncateTime(p.fecha_registracion) >= fechaInicio.Date &&
                        DbFunctions.TruncateTime(p.fecha_registracion) <= fechaFin.Date)
                    .GroupBy(p => DbFunctions.TruncateTime(p.fecha_registracion))
                    .Select(g => new
                    {
                        Fecha = g.Key.Value,
                        Cantidad = g.Count()
                    })
                    .ToList();

                var dic = new Dictionary<DateTime, int>();

                foreach (var item in query)
                {
                    dic[item.Fecha.Date] = item.Cantidad;
                }

                return dic;
            }
        }

        // Obtener distribución de pacientes por estado
        public Dictionary<string, int> ObtenerDistribucionPacientesPorEstado()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var query = db.paciente
                    .GroupBy(p => p.estado_paciente.nombre)
                    .Select(g => new
                    {
                        Estado = g.Key,
                        Cantidad = g.Count()
                    })
                    .ToList();

                var dic = new Dictionary<string, int>();

                foreach (var item in query)
                {
                    if (item.Estado == null)
                        continue;

                    string clave = item.Estado.ToLower(); // "activo", "internado", "alta"
                    dic[clave] = item.Cantidad;
                }

                return dic;
            }
        }
    }
}

