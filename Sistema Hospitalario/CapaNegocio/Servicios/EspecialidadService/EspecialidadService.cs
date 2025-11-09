using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.EspecialidadDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.EspecialidadService
{
    public class EspecialidadService
    {
        private readonly EspecialidadRepository _repo = new EspecialidadRepository();

        public EspecialidadService()
        {
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
