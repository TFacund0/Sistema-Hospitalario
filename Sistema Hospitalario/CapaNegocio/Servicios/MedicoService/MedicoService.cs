using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaDatos;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService
{
    public class MedicoService
    {
        public List<MedicoDto> ListarMedicos()
        {
            using (var context = new Sistema_HospitalarioEntities())
            {
                var medicos = context.medico.ToList();
                var medicoDtos = medicos.Select(m => new MedicoDto
                {
                    Id = m.id_medico,
                    matricula = m.matricula,
                    Nombre = m.nombre,
                    Apellido = m.apellido,
                    Especialidad = m.especialidad.nombre,
                    Direccion = m.direccion,
                    Email = m.correo_electronico
                }).ToList();
                return medicoDtos;
            }
        }

    }
}
