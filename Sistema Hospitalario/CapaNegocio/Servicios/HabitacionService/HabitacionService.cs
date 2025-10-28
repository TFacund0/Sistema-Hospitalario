using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.ModerRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService
{
    public class HabitacionService
    {
        private readonly IHabitacionRepository _repo;

        public HabitacionService(IHabitacionRepository repo)
        {
            _repo = repo;
        }

        public List<MostrarHabitacionDTO> ObtenerHabitaciones()
        {
            return _repo.GetAll();
        }

        public void AgregarHabitacion(int nroPiso, int tipoHabitacion)
        {
            if (nroPiso < 0)
                throw new ArgumentException("El piso debe ser mayor o igual a 0.");

            _repo.Insertar(nroPiso, tipoHabitacion);
        }

        public void EliminarHabitacion(int nroPiso, int nroHabitacion)
        {
            _repo.Eliminar(nroPiso, nroHabitacion);
        }

        public List<TiposHabitacionDTO> ListarTiposHabitacion()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.tipo_habitacion
                            .Select(e => new TiposHabitacionDTO
                            {
                                IdTipoHabitacion = e.id_tipo_habitacion,
                                Nombre = e.nombre

                            })
                            .ToList();
            }
        }

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