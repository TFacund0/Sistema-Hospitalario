using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.HabitacionDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService
{
    public class CamaService
    {
        private readonly ICamaRepository _repo;

        public CamaService(ICamaRepository repo)
        {
            _repo = repo;
        }

        public List<MostrarCamaDTO> ObtenerCamas()
        {
            return _repo.GetAll();
        }

        public void AgregarCama(int nroHabitacion)
        {
            if (nroHabitacion < 0)
                throw new ArgumentException("el numero de habitacion debe ser positivo");

            _repo.Insertar(nroHabitacion);
        }

        public void EliminarCama(int nroHabitacion, int nroCama)
        {
            _repo.Eliminar(nroHabitacion, nroCama);
        }

        public (bool Ok, string Error) CambiarEstado(int nroHabitacion, int idCama, int nuevoEstadoId)
        {
            try
            {
                using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
                {
                    // Buscamos la cama usando su clave primaria compuesta
                    var cama = db.cama.Find(idCama, nroHabitacion);

                    if (cama == null)
                    {
                        return (false, "La cama no fue encontrada.");
                    }

                    // ¡Actualizamos el estado!
                    cama.id_estado_cama = nuevoEstadoId;
                    db.SaveChanges(); // Guardamos

                    return (true, null); // Éxito
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        // ===================== TOTAL CAMAS =====================
        public async Task<int> TotalCamas()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                // Cuenta todas las camas en la base de datos
                int totalCamas = await Task.Run(() => db.cama.Count());
                return totalCamas;
            }
        }

        // ===================== TOTAL CAMAS X ESTADO =====================
        public async Task<int> TotalCamasXEstado(int p_id_estado, string p_nombre_estado)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                // Cuenta todas las camas que tienen el id de estado de cama igual al parámetro
                int totalCamasXEstado = await Task.Run(() => db.cama.Count(c => c.id_estado_cama == p_id_estado && c.estado_cama.disponibilidad == p_nombre_estado));
                return totalCamasXEstado;
            }
        }

        public List<CamaDto> ListarCamasXHabitacion(string p_nroHabitacion)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
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
