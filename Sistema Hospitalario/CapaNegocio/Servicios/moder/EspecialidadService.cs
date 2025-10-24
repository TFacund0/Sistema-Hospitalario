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
    public class EspecialidadService
    {
        private readonly IEspecialidadRepository _repo;

        public EspecialidadService(IEspecialidadRepository repo)
        {
            _repo = repo;
        }

        public List<EspecialidadDTO> ObtenerEspecialidades()
        {
            return _repo.GetAll();
        }

        public void AgregarEspecialidad(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            _repo.Insertar(nombre);
        }

        public void EliminarEspecialidad(string nombre)
        {
            _repo.Eliminar(nombre);
        }
    }
}
