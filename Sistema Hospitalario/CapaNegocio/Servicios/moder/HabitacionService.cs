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
    public class HabitacionService
    {
        private readonly IHabitacionRepository _repo;

        public HabitacionService(IHabitacionRepository repo)
        {
            _repo = repo;
        }

        public List<MostrarHabitacionDTO> ObtenerHabitaciones()
        {
            return _repo.GetAll();
        }

        public void AgregarHabitacion(int nroPiso, int tipoHabitacion)
        {
            if (nroPiso < 0)
                throw new ArgumentException("El piso debe ser mayor o igual a 0.");

            _repo.Insertar(nroPiso, tipoHabitacion);
        }

        public void EliminarHabitacion(int nroPiso, int nroHabitacion)
        {
            _repo.Eliminar(nroPiso, nroHabitacion);
        }
    }
}
