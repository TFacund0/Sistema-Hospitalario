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
        public List<ListadoInternacionDto> ListadoInternacionDtos()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.internacion
                    .Select(i => new ListadoInternacionDto
                    {
                        Nro_habitacion = i.nro_habitacion,
                        Nro_piso = i.cama.habitacion.nro_piso,
                        Internado = i.paciente.nombre + " " + i.paciente.apellido,
                        Fecha_ingreso = i.fecha_inicio,
                        Cama = i.id_cama,
                        Tipo_habitacion = i.cama.habitacion.tipo_habitacion.nombre
                    }).ToList();
            }
        }

        public void AltaInternacion(InternacionDto dto)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var paciente = db.paciente.Find(dto.Id_paciente);
                // Actualizar estado del paciente a "Internado"
                paciente.id_estado_paciente = 2;
                db.Entry(paciente).State = System.Data.Entity.EntityState.Modified;

                // Cama es clave compuesta
                var cama = db.cama.Find(dto.Id_cama, dto.Nro_habitacion);
                // Actualizar estado de la cama a "Ocupada"
                cama.id_estado_cama = 9;
                db.Entry(cama).State = System.Data.Entity.EntityState.Modified;

                var internacion = new internacion
                {
                    id_paciente = dto.Id_paciente,
                    id_medico = dto.Id_medico,
                    id_procedimiento = dto.Id_procedimiento,
                    id_cama = dto.Id_cama,
                    nro_habitacion = dto.Nro_habitacion,
                    fecha_inicio = dto.Fecha_ingreso,
                    fecha_fin = dto.Fecha_egreso,
                    motivo = dto.Diagnostico,
                };

                db.internacion.Add(internacion);
                db.SaveChanges();
            }
        }

        public int TotalInternacionesXProcedimiento(int id_procedimiento)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                int totalInternaciones = db.internacion.Count(i => i.id_procedimiento == id_procedimiento);
                return totalInternaciones;
            }
        }
    }
}
