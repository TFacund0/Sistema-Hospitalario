using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;   
using System.Data.Entity;

using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.TurnoDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.TurnoService
{
    public class TurnoService
    {
        private readonly TurnoRepository _repo = new TurnoRepository();
        public TurnoService()
        {
        }

        public List<ListadoTurno> ListarTurnos ()
        {
            return _repo.ListadoTurnos().Where(t => t.FechaTurno >= DateTime.Now.AddDays(-7)).ToList();
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
    }
}
