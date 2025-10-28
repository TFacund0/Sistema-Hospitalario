using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.EspecialidadDTO;

namespace Sistema_Hospitalario.CapaDatos.interfaces
{
    public interface IEspecialidadRepository
    {
        List<EspecialidadDTO> GetAll();
        void Insertar(string nombre);
        void Eliminar(string nombre);
    }
}
