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
    /// <summary>
    /// Servicio que gestiona la información general mostrada en la pantalla de inicio (Home).
    /// Proporciona acceso a las actividades recientes registradas en el sistema.
    /// </summary>
    public class HomeService
    {
        private readonly HomeRepository _repo = new HomeRepository();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HomeService"/>.
        /// </summary>
        public HomeService()
        {
        }

        /// <summary>
        /// Obtiene un listado de las actividades recientes del sistema (ej. altas de pacientes, turnos).
        /// </summary>
        /// <param name="cantidad">Número máximo de actividades a recuperar.</param>
        /// <returns>Lista de <see cref="HomeDto"/> con la descripción y fecha de cada actividad.</returns>
        public List<HomeDto> ListarActividadReciente(int cantidad)
        {
            // Trae la lista y filtra solo las actividades del último mes
            return _repo.ListarActividad(cantidad);
        }

    }
}
