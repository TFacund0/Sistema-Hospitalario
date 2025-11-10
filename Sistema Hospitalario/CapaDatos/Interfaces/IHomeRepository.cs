using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.HomeDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IHomeRepository
    {
        List<HomeDto> ListarActividad(int cantidad);
    }
}
