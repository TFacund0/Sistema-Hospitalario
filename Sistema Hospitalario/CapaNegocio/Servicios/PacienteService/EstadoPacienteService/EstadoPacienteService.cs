using System.Collections.Generic;
using System.Linq;

using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.PacienteService
{
    public class EstadoPacienteService
    {
        private readonly PacienteRepository _repo = new PacienteRepository();

        public EstadoPacienteService()
        {
        }

        public List<EstadoPacienteDto> ListarEstados()
        {
            var listaEstados = _repo.GetEstados();
            
            return listaEstados.OrderBy(e => e.Nombre).ToList();
        }
    }
}
