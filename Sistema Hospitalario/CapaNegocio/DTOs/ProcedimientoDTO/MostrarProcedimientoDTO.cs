using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO
{
    /// <summary>
    /// DTO simple utilizado para listar los nombres de los procedimientos médicos en la interfaz.
    /// </summary>
    public class MostrarProcedimientoDTO
    {
        /// <summary>Nombre del procedimiento.</summary>
        public string Nombre { get; set; }
    }
}
