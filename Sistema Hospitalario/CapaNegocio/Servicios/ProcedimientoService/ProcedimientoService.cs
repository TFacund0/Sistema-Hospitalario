using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService
{
    public class ProcedimientoService
    {
        private readonly ProcedimientoRepository _repo = new ProcedimientoRepository();

        public ProcedimientoService()
        {
        }

        // Obtener todas los procedimientos
        public List<MostrarProcedimientoDTO> ObtenerProcedimientos()
        {
            return _repo.GetAll();
        }

        // Agregar un nuevo procedimiento
        public void AgregarProcedimiento(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            _repo.Insertar(nombre);
        }

        // Eliminar un procedimiento por nombre
        public void EliminarProcedimiento(string nombre)
        {
            _repo.Eliminar(nombre);
        }

        // Listar procedimientos
        public List<ProcedimientoDto> ListarProcedimientos() { 
           return _repo.ListarProcedimientos();
        }
    }
}
