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

        public int ContarCamasPorDisponibilidad(string disponibilidad)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.cama
                    .Count(c => c.estado_cama.disponibilidad.ToLower() == disponibilidad.ToLower());
            }
        }
    }
}

