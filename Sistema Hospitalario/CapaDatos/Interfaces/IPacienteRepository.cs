using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IPacienteRepository
    {
        List<PacienteDto> GetAll();
        List<EstadoPacienteDto> GetEstados();
        void Insertar(PacienteDto paciente);
        void Eliminar(int id_paciente);
        void Actualizar(int id_paciente, PacienteDto paciente);
    }
}
