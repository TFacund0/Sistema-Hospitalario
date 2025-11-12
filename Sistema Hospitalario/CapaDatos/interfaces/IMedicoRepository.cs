using Sistema_Hospitalario.CapaNegocio.DTOs.HistorialDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IMedicoRepository
    {
        (bool Ok, int IdGenerado, string Error) Insertar(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int idEspecialidad);
        void Eliminar(int idMedico);
        List<MostrarMedicoDTO> ObtenerMedicos();
        List<MedicoDto> ListarMedicos();
        void InsertarConsulta(Consulta consulta);
        List<PacienteListadoMedicoDto> ObtenerTodosParaMedico(DateTime? fechaTurno);
        int ContarTotalPacientes();
        List<HistorialItemDto> ObtenerHistorialConsultas(int idPaciente);

        List<HistorialItemDto> ObtenerHistorialInternaciones(int idPaciente);
        List<HistorialItemDto> ObtenerHistorialTurnos(int idPaciente);
    }
}
