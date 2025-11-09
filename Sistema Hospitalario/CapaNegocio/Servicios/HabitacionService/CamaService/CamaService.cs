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

        public List<MostrarCamaDTO> ObtenerCamas()
        {
            return _repo.GetAll();
        }

        public void AgregarCama(int nroHabitacion)
        {
            if (nroHabitacion < 0)
                throw new ArgumentException("el numero de habitacion debe ser positivo");

            _repo.Insertar(nroHabitacion);
        }

        public void EliminarCama(int nroHabitacion, int nroCama)
        {
            _repo.Eliminar(nroHabitacion, nroCama);
        }

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
            var totalCamasXEstado = _repo.GetAll().Count(c => c.Estado == p_nombre_estado);
            return totalCamasXEstado;
        }

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
    }
}
