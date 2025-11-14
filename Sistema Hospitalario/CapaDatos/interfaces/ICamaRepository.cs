using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface ICamaRepository
    {
        List<MostrarCamaDTO> GetAll();
        void Insertar(cama NuevaCama);
        void Eliminar(int NroHabitacion, int NroCama);
        void CambiarEstado(int NroHabitacion, int NroCama, int NuevoEstadoId);
    }
}
