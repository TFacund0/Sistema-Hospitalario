using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using Sistema_Hospitalario.CapaDatos.Repositories;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService
{
    public class InternacionService
    {
        private readonly InternacionRepository _repo = new InternacionRepository();

        public InternacionService()
        {
        }

        // Obtener todas las internaciones
        public List<InternacionDto> ListadoInternacionDtos()
        {
            var listadoInternaciones = _repo.GetAll();
            return listadoInternaciones;
        }

        // Alta de una nueva internación
        public void AltaInternacion(InternacionDto dto)
        {
            _repo.Insertar(dto);
        }

        // ===================== TOTAL INTERNACIONES X PROCEDIMIENTO =====================
        public int TotalInternacionesXProcedimiento(int id_procedimiento)
        {
            var totalInternaciones = _repo.GetAll()
                .Count(i => i.Id_procedimiento == id_procedimiento);
            return totalInternaciones;
        }
    }
}
