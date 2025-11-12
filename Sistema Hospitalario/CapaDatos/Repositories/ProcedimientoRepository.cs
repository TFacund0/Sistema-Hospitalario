using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class ProcedimientoRepository : IProcedimientoRepository
    {
        public ProcedimientoRepository()
        {
        }

        // Implementación de los métodos de la interfaz IProcedimientoRepository
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

        // Insertar un nuevo procedimiento
        public void Insertar(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                db.procedimiento.Add(new procedimiento { nombre = nombre });
                db.SaveChanges();
            }
        }

        // Eliminar un procedimiento por nombre
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

        // Verificar si un procedimiento con el mismo nombre ya existe
        public bool ExisteNombre(string nombre)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.procedimiento.Any(e => e.nombre == nombre);
            }
        }

        // Listar todos los procedimientos como DTOs
        public List<ProcedimientoDto> ListarProcedimientos()
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                return db.procedimiento
                         .Select(p => new ProcedimientoDto
                         {
                             Id = p.id_procedimiento,
                             Name = p.nombre
                         })
                         .ToList();
            }
        }
    }
}
