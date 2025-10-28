using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.CamaDTO;

namespace Sistema_Hospitalario.CapaDatos.interfaces
{
    public interface ICamaRepository
    {
        List<MostrarCamaDTO> GetAll();
        void Insertar(int NroHabitacion);
        void Eliminar(int NroHabitacion, int NroCama);
    }
}
