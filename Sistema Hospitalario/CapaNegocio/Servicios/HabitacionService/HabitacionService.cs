using Sistema_Hospitalario.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;
using Sistema_Hospitalario.CapaDatos.Repositories;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService
{
    public class HabitacionService
    {
        private readonly HabitacionRepository _repo = new HabitacionRepository();

        public HabitacionService()
        {
        }

        // Obtener todas las habitaciones
        public List<MostrarHabitacionDTO> ObtenerHabitaciones()
        {
            return _repo.GetAll();
        }

        // Agregar una nueva habitación
        public void AgregarHabitacion(int nroPiso, int tipoHabitacion)
        {
            if (nroPiso < 0)
                throw new ArgumentException("El piso debe ser mayor o igual a 0.");

            _repo.Insertar(nroPiso, tipoHabitacion);
        }

        // Eliminar una habitación por número de piso y número de habitación
        public void EliminarHabitacion(int nroPiso, int nroHabitacion)
        {
            _repo.Eliminar(nroPiso, nroHabitacion);
        }

        // Listar tipos de habitación
        public List<TiposHabitacionDTO> ListarTiposHabitacion()
        {
            return _repo.ListarTiposHabitacion();
        }

        // ===================== TOTAL HABITACIONES =====================
        public int TotalHabitaciones()
        {
            var totalHabitaciones = _repo.GetAll().Count;
            return totalHabitaciones;
        }

        // Listar habitaciones por piso (entrada como texto)
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