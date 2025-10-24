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
    public class CamaService
    {
        private readonly ICamaRepository _repo;

        public CamaService(ICamaRepository repo)
        {
            _repo = repo;
        }

        public List<MostrarCamaDTO> ObtenerCamas()
        {
            return _repo.GetAll();
        }

        public void AgregarCama(int nroHabitacion)
        {
            if (nroHabitacion < 0)
                throw new ArgumentException("el numero de habitacion debe ser positivo");

            _repo.Insertar(nroHabitacion);
        }

        public void EliminarCama(int nroHabitacion, int nroCama)
        {
            _repo.Eliminar(nroHabitacion, nroCama);
        }
    }
}
