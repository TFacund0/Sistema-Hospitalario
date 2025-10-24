using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService
{
    public class HabitacionService
    {
        // Método para obtener el total de habitaciones registradas en el sistema
        public async Task<int> TotalHabitaciones()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                // Cuenta todas las habitaciones registradas
                int totalHabitaciones = await Task.Run(() => db.habitacion.Count());
                return totalHabitaciones;
            }
        }

        public List<HabitacionDto> ListarHabitacionesXPiso(string pisoTexto)
        {
            var habitaciones = new List<HabitacionDto>();

            // Validar que el texto sea numérico y mayor a 0
            if (string.IsNullOrWhiteSpace(pisoTexto) || !int.TryParse(pisoTexto, out var piso) || piso <= 0)
            {
                return habitaciones; // devuelve lista vacía
            }

            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                habitaciones = db.habitacion
                    .Where(h => h.nro_piso == piso)
                    .OrderBy(h => h.nro_habitacion)
                    .Select(h => new HabitacionDto
                    {
                        Nro_habitacion = h.nro_habitacion,
                        Nro_piso = h.nro_piso,
                        Tipo_habitacion = h.tipo_habitacion.nombre
                    })
                    .ToList();
            }

            return habitaciones;
        }

    }
}
