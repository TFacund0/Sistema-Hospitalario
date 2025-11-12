using Sistema_Hospitalario.CapaNegocio.DTOs.HistorialDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IInternacionRepository
    {
        List<InternacionDto> GetAll();
        void Insertar(InternacionDto internacion);
        void Eliminar(int id_internacion);
        void Actualizar(int id_internacion, InternacionDto internacion);
    }
}
