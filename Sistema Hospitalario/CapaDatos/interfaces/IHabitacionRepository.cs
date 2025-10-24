using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;

namespace Sistema_Hospitalario.CapaDatos.interfaces
{
    public interface IHabitacionRepository
    {
        List<MostrarHabitacionDTO> GetAll();
        void Insertar(int NroPiso, int tipoHabitacion);
        void Eliminar(int nroPiso, int NroHabitacion);
    }
}
