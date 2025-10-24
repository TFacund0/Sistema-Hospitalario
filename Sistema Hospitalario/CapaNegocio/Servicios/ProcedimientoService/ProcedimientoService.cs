using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.ProcedimientoDTO;
using Sistema_Hospitalario.CapaDatos;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.ProcedimientoService
{
    public class ProcedimientoService
    {
        public List<ProcedimientoDto> ListarProcedimientos()
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                return db.procedimiento
                    .Select(p => new ProcedimientoDto
                    {
                        Id = p.id_procedimiento,
                        Name = p.nombre
                    })
                    .ToList();
            }
        }
    }
}
