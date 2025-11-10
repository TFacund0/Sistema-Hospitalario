using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Sistema_Hospitalario.CapaNegocio.DTOs.HomeDTO;
using Sistema_Hospitalario.CapaDatos.Repositories;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HomeService
{
    public class HomeService
    {
        private readonly HomeRepository _repo = new HomeRepository();

        public HomeService()
        {
        }

        public List<HomeDto> ListarActividadReciente(int cantidad)
        {
            // Trae la lista y filtra solo las actividades del último mes
            return _repo.ListarActividad(cantidad);
        }

    }
}
