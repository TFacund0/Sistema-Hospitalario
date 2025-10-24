using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.ModerRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.moder
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
    }
}
