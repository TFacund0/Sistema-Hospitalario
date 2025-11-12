using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface ITurnoRepository
    {
        List<TurnoDto> GetAll();
        void Insertar(TurnoDto turno);
        void Actualizar(int id_turno, TurnoDto turno);
        void Eliminar(int id_turno);

        List<TurnoAgendaDto> ObtenerTurnosParaAgenda(int idMedico, DateTime fecha);
        AgendaContadoresDto ObtenerContadoresAgenda(int idMedico, DateTime fecha);
        bool ActualizarEstadoTurno(int idTurno, int idNuevoEstado);
    }
}
