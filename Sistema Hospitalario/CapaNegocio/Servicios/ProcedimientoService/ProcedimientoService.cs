using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.ModerRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService
{
    public class ProcedimientoService
    {
        private readonly IProcedimientoRepository _repo;

        public ProcedimientoService(IProcedimientoRepository repo)
        {
            _repo = repo;
        }

        public List<MostrarProcedimientoDTO> ObtenerProcedimientos()
        {
            return _repo.GetAll();
        }

        public void AgregarProcedimiento(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            _repo.Insertar(nombre);
        }

        public void EliminarProcedimiento(string nombre)
        {
            _repo.Eliminar(nombre);
        }
        public List<ProcedimientoDto> ListarProcedimientos() { 
           
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
