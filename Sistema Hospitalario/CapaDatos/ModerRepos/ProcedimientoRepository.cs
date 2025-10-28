using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos.interfaces;

namespace Sistema_Hospitalario.CapaDatos.ModerRepos
{
    public class ProcedimientoRepository : IProcedimientoRepository
    {
        public List<MostrarProcedimientoDTO> GetAll()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.procedimiento
                         .Select(e => new MostrarProcedimientoDTO
                         {
                             Nombre = e.nombre
                         })
                         .ToList();
            }
        }

        public void Insertar(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                db.procedimiento.Add(new procedimiento { nombre = nombre });
                db.SaveChanges();
            }
        }

        public void Eliminar(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var esp = db.procedimiento.FirstOrDefault(e => e.nombre.ToLower() == nombre.ToLower());
                if (esp != null)
                {
                    db.procedimiento.Remove(esp);
                    db.SaveChanges();
                }
            }
        }

        public bool ExisteNombre(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.procedimiento.Any(e => e.nombre == nombre);
            }
        }
    }
}
