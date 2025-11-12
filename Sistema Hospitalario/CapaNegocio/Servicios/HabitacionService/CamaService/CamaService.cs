using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.HabitacionService.CamaService
{
    public class CamaService
    {
        private readonly CamaRepository _repo = new CamaRepository();

        public CamaService()
        {
        }

        // Obtener todas las camas
        public List<MostrarCamaDTO> ObtenerCamas()
        {
            return _repo.GetAll();
        }

        // Agregar una nueva cama a una habitación específica
        public void AgregarCama(int nroHabitacion)
        {
            if (nroHabitacion < 0)
                throw new ArgumentException("el numero de habitacion debe ser positivo");

            _repo.Insertar(nroHabitacion);
        }

        // Eliminar una cama por número de habitación y número de cama
        public void EliminarCama(int nroHabitacion, int nroCama)
        {
            _repo.Eliminar(nroHabitacion, nroCama);
        }

        // Cambiar el estado de una cama
        public void CambiarEstado(int nroHabitacion, int idCama, int nuevoEstadoId)
        {   
            _repo.CambiarEstado(nroHabitacion, idCama, nuevoEstadoId);
        }

        // ===================== TOTAL CAMAS =====================
        public int TotalCamas()
        {
            var totalCamas = _repo.GetAll().Count;
            return totalCamas;
        }

        // ===================== TOTAL CAMAS X ESTADO =====================
        public int TotalCamasXEstado(string p_nombre_estado)
        {
            var totalCamasXEstado = _repo.GetAll().Count(c => c.Estado.ToLower() == p_nombre_estado.ToLower());
            return totalCamasXEstado;
        }

        // ===================== LISTAR CAMAS X HABITACION =====================
        public List<CamaDto> ListarCamasXHabitacion(string p_nroHabitacion)
        {
            var camasList = _repo.GetAll();
            
            return camasList
                .Where(c => c.NroHabitacion.ToString() == p_nroHabitacion)
                .Select(c => new CamaDto
                {
                    NroCama = c.NroCama,
                    NroHabitacion = c.NroHabitacion,
                    IdEstadoCama = c.IdEstadoCama,
                    EstadoCama = c.Estado
                }).ToList();
        }

        // ===================== LISTAR ESTADOS DE CAMA =====================
        public List<CamaDto> ListarEstadosCama()
        {
            var camasList = _repo.GetEstadosCama();
            return camasList
                .Select(e => new CamaDto
                {
                    IdEstadoCama = e.id_estado_cama,
                    EstadoCama = e.disponibilidad
                }).ToList();
        }
    }
}
