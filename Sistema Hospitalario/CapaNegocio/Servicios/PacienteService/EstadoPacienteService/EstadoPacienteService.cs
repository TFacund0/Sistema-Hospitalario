using System.Collections.Generic;
using System.Linq;

using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService
{
    /// <summary>
    /// Servicio que gestiona el catálogo de estados clínicos en los que puede encontrarse un paciente.
    /// Proporciona acceso a los estados definidos (ej. Activo, Internado, Alta).
    /// </summary>
    public class EstadoPacienteService
    {
        private readonly PacienteRepository _repo = new PacienteRepository();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EstadoPacienteService"/>.
        /// </summary>
        public EstadoPacienteService()
        {
        }

        /// <summary>
        /// Obtiene el listado completo de estados de paciente, ordenados alfabéticamente por nombre.
        /// </summary>
        /// <returns>Lista de <see cref="EstadoPacienteDto"/>.</returns>
        public List<EstadoPacienteDto> ListarEstados()
        {
            var listaEstados = _repo.GetEstados();
            
            return listaEstados.OrderBy(e => e.Nombre).ToList();
        }
    }
}
