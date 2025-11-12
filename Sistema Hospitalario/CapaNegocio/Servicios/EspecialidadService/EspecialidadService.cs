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

        // Obtener todas las especialidades
        public List<EspecialidadDTO> ObtenerEspecialidades()
        {
            return _repo.GetAll();
        }

        // Agregar una nueva especialidad
        public void AgregarEspecialidad(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            _repo.Insertar(nombre);
        }

        // Eliminar una especialidad por nombre
        public void EliminarEspecialidad(string nombre)
        {
            _repo.Eliminar(nombre);
        }
    }
}
