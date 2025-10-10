using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.InternacionService
{
    public class InternacionService
    {
        public List<ListadoInternacionDto> listadoInternacionDtos()
        {
            using (var db = new Sistema_HospitalarioEntities())
            {
                return db.internacion
                    .Select(i => new ListadoInternacionDto
                    {
                        Nro_habitacion = i.nro_habitacion,
                        Nro_piso = i.habitacion.nro_piso,
                        Internado = i.paciente.nombre + " " + i.paciente.apellido,
                        Fecha_ingreso = i.fecha_inicio,
                        Cama = i.id_cama,
                        Tipo_habitacion = i.habitacion.tipo_habitacion.nombre
                    }).ToList();
            }
        }
    }
}
