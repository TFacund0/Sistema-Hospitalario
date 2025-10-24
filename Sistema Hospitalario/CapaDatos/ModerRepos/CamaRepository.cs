using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos.interfaces;
using System.Windows;

namespace Sistema_Hospitalario.CapaDatos.ModerRepos
{
    public class CamaRepository : ICamaRepository
    {
        public List<MostrarCamaDTO> GetAll()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.cama
                         .Select(e => new MostrarCamaDTO
                         {
                             NroHabitacion = e.nro_habitacion,
                             Estado = e.estado_cama.disponibilidad,
                             NroCama = e.id_cama
                         })
                         .ToList();
            }
        }

        public void Insertar(int nroHabitacion)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                db.cama.Add(new cama { nro_habitacion = nroHabitacion });
                db.SaveChanges();
            }
        }

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

        public bool ExisteNombre(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.especialidad.Any(e => e.nombre == nombre);
            }
        }
    }
}
