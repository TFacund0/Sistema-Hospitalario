using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.interfaces
{
    using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
    using System.Collections.Generic;

    public interface IMedicoRepository
    {
        (bool Ok, int IdGenerado, string Error) Insertar(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int idEspecialidad);
        void Eliminar(int idMedico);
        List<MostrarMedicoDTO> ObtenerMedicos();
        List<MedicoDto> ListarMedicos();
    }
}
