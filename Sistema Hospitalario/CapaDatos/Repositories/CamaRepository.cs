using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class CamaRepository : ICamaRepository
    {
        public CamaRepository()
        {
        }

        // Obtener todas las camas con su estado y número de habitación
        public List<MostrarCamaDTO> GetAll()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.cama
                            .Select(e => new MostrarCamaDTO
                            {
                                NroHabitacion = e.nro_habitacion,
                                Estado = e.estado_cama.disponibilidad,
                                NroCama = e.id_cama,
                                IdEstadoCama = e.id_estado_cama
                            })
                            .ToList();
            }
        }

        // Insertar una nueva cama con el estado "Disponible" por defecto
        public void Insertar(int nroHabitacion)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                int idEstadoPorDefecto = db.estado_cama
                                           .Where(e => e.disponibilidad == "Disponible")
                                           .Select(e => e.id_estado_cama)
                                           .FirstOrDefault();

                if (idEstadoPorDefecto == 0)
                {
                    // Lanzamos un error claro si no existe el estado "Disponible"
                    throw new Exception("Error de configuración: No se encontró el estado 'Disponible' en la base de datos.");
                }

                // 3. Creamos la cama con el ID de estado correcto
                var nuevaCama = new cama
                {
                    nro_habitacion = nroHabitacion,
                    id_estado_cama = idEstadoPorDefecto
                };

                db.cama.Add(nuevaCama);
                db.SaveChanges();
            }
        }

        // Eliminar una cama específica
        public void Eliminar(int nroHabitacion, int nroCama)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var esp = db.cama.FirstOrDefault(e => e.nro_habitacion == nroHabitacion && e.id_cama == nroCama) ;
                if (esp == null) 
                {
                    MessageBox.Show($"No se encontró la cama {nroCama} en la habitación {nroHabitacion}");
                    return;
                }
                if (esp != null)
                {
                    db.cama.Remove(esp);
                    db.SaveChanges();
                }
            }
        }

        // Cambiar el estado de una cama específica
        public void CambiarEstado(int nroHabitacion, int nroCama, int nuevoEstadoId)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var cama = db.cama.FirstOrDefault(e => e.nro_habitacion == nroHabitacion && e.id_cama == nroCama);
                if (cama != null)
                {
                    cama.id_estado_cama = nuevoEstadoId;
                    db.SaveChanges();
                }
            }
        }

        // Obtener todos los estados de cama disponibles
        public List<estado_cama> GetEstadosCama()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.estado_cama.ToList();
            }
        }
    }
}
