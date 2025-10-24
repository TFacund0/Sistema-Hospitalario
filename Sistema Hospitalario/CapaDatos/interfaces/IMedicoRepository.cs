using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;

namespace Sistema_Hospitalario.CapaDatos.interfaces
{
    using System.Collections.Generic;

    public interface IMedicoRepository
    {
        List<MostrarMedicoDTO> GetAll();
        void Insertar(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int? idEspecialidad);
        void Eliminar(int idMedico);
    }
}
