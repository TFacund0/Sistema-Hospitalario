using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;   

namespace Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService
{
    public class TurnoService
    {
        private readonly TurnoRepository _repo = new TurnoRepository();
        public TurnoService(TurnoRepository repo)
        {
            _repo = repo;
        }
        public TurnoService()
        {
        }

        public List<TurnoAgendaDto> ObtenerTurnosParaAgenda(int idMedico, DateTime fecha)
        {
            return _repo.ObtenerTurnosParaAgenda(idMedico, fecha);
        }

        public AgendaContadoresDto ObtenerContadoresAgenda(int idMedico, DateTime fecha)
        {
            return _repo.ObtenerContadoresAgenda(idMedico, fecha);
        }

        public bool ActualizarEstadoTurno(int idTurno, int idNuevoEstado)
        {
            return _repo.ActualizarEstadoTurno(idTurno, idNuevoEstado);
        }
        // Listar todos los turnos
        public List<ListadoTurno> ListarTurnos ()
        {
            return _repo.ListadoTurnos();
        }

        // Contar turnos por estado, con manejo especial para "Pendiente" (solo los de hoy)
        public int CantidadTurnosPorEstado(string estado)
        {
            var turnos = _repo.GetAll().Where(t => t.Estado.ToLower() == estado.ToLower());

            if (estado.ToLower() == "pendiente")
            {
                var hoy = DateTime.Today;
                turnos = turnos.Where(t => t.FechaTurno.Date == hoy);
            }

            return turnos.Count();
        }

        // Contar todos los turnos pendientes
        public int CantidadTurnosPendientes()
        {
            var turnosPendientes = _repo.GetAll()
                                        .Where(t => t.Estado.ToLower() == "pendiente");
            return turnosPendientes.Count();
        }

        // Registrar un nuevo turno
        public void RegistrarTurno(TurnoDto turnoDto)
        {
            _repo.Insertar(turnoDto);
        }

        // Actualizar un turno existente
        public void ActualizarTurno(int id_turno, TurnoDto turnoDto)
        {
            _repo.Actualizar(id_turno, turnoDto);
        }

        // Eliminar un turno por ID
        public void EliminarTurno(int id_turno)
        {
            _repo.Eliminar(id_turno);
        }

        // Obtener detalles de un turno por ID
        public TurnoDto ObtenerDetalle(int p_id_turno)
        {
            return _repo.ObtenerDetalle(p_id_turno);
        }

        // Listar estados de turnos
        public List<ListadoEstadoTurno> ListadoEstadosTurnos()
        {
            return _repo.ListarEstadosTurno();
        }
        
        // Verificar si existe un turno para el mismo día, mismo médico y mismo paciente
        public bool ExisteTurnoMismoDiaMismoMedicoPaciente(int idPaciente, int idMedico, DateTime fecha)
        {
            return _repo.ExisteTurnoMismoDiaMismoMedicoPaciente(idPaciente, idMedico, fecha);
        }

        public bool ExisteTurnoMismoDiaMismoMedicoPacienteExcluyendo(int idTurnoExcluir, int idPaciente, int idMedico, DateTime fecha)
        {
            return _repo.ExisteTurnoMismoDiaMismoMedicoPacienteExcluyendo(
                idTurnoExcluir, idPaciente, idMedico, fecha);
        }
    }
}
