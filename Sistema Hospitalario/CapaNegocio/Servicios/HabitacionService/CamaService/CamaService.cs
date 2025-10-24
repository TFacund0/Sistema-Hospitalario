using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService
{
    public class CamaService
    {
        // ===================== TOTAL CAMAS =====================
        public async Task<int> TotalCamas()
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                // Cuenta todas las camas en la base de datos
                int totalCamas = await Task.Run(() => db.cama.Count());
                return totalCamas;
            }
        }

        // ===================== TOTAL CAMAS X ESTADO =====================
        public async Task<int> TotalCamasXEstado(int p_id_estado, string p_nombre_estado)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                // Cuenta todas las camas que tienen el id de estado de cama igual al parámetro
                int totalCamasXEstado = await Task.Run(() => db.cama.Count(c => c.id_estado_cama == p_id_estado && c.estado_cama.disponibilidad == p_nombre_estado));
                return totalCamasXEstado;
            }
        }

        public List<CamaDto> ListarCamasXHabitacion(string p_nroHabitacion)
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                var camas = db.cama
                    .Where(c => c.nro_habitacion.ToString() == p_nroHabitacion)
                    .Select(c => new CamaDto
                {
                    NroCama = c.id_cama,
                    NroHabitacion = c.nro_habitacion,
                    IdEstadoCama = c.id_estado_cama,
                    EstadoCama = c.estado_cama.disponibilidad
                }).ToList();
                return camas;
            }
        }
    }
}
