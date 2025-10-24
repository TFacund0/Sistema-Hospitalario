using System.Collections.Generic;
using System.Linq;
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO.EstadoPacienteDTO;

namespace Sistema_Hospitalario.CapaNegocio
{
    public class EstadoPacienteService
    {
        // Listar todos los estados de paciente disponibles
        public List<EstadoPacienteDto> ListarEstados()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.estado_paciente
                         .OrderBy(estado => estado.nombre)
                         .Select(estado => new EstadoPacienteDto
                         {
                             Id = estado.id_estado_paciente,
                             Nombre = estado.nombre
                         })
                         .ToList();
            }
        }
    }
}
