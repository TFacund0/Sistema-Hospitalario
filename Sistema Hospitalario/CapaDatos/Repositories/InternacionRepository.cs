using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.InternacionDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class InternacionRepository : IInternacionRepository
    {
        public InternacionRepository()
        {
        }
        public List<InternacionDto> GetAll()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.internacion
                    .Select(i => new InternacionDto
                    {
                        Id_paciente = i.id_paciente,
                        Id_medico = i.id_medico,
                        Id_procedimiento = i.id_procedimiento,
                        Internado = i.paciente.nombre + " " + i.paciente.apellido,
                        Fecha_ingreso = i.fecha_inicio,
                        Fecha_egreso = i.fecha_fin,
                        Diagnostico = i.motivo,
                        Nro_habitacion = i.nro_habitacion,
                        Id_cama = i.id_cama,
                        Nro_piso = i.cama.habitacion.nro_piso
                    }).ToList();
            }
        }

        public void Insertar(InternacionDto internacion)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                // 1) Buscar y validar paciente
                var paciente = db.paciente.Find(internacion.Id_paciente);
                if (paciente == null)
                {
                    throw new Exception($"No se encontró el paciente con ID {internacion.Id_paciente}");
                }

                // 2) Buscar el estado "Internado" en la tabla estado_paciente
                var estadoInternado = db.estado_paciente
                    .FirstOrDefault(e => e.nombre == "Internado");

                if (estadoInternado == null)
                {
                    throw new Exception("No existe el estado de paciente 'Internado' en la tabla estado_paciente.");
                }

                // Asignar el id encontrado
                paciente.id_estado_paciente = estadoInternado.id_estado_paciente;
                db.Entry(paciente).State = EntityState.Modified;

                // 3) Buscar y validar cama (clave compuesta: id_cama + nro_habitacion)
                var cama = db.cama.Find(internacion.Id_cama, internacion.Nro_habitacion);
                if (cama == null)
                {
                    throw new Exception(
                        $"No se encontró la cama con Id_cama = {internacion.Id_cama} y Nro_habitacion = {internacion.Nro_habitacion}");
                }

                // 4) Buscar el estado "Ocupada" en la tabla estado_cama
                var estadoOcupada = db.estado_cama
                    .FirstOrDefault(e => e.disponibilidad == "Ocupada");

                if (estadoOcupada == null)
                {
                    throw new Exception("No existe el estado de cama 'Ocupada' en la tabla estado_cama.");
                }

                // Asignar el id encontrado
                cama.id_estado_cama = estadoOcupada.id_estado_cama;
                db.Entry(cama).State = EntityState.Modified;

                // 5) Crear la internación como ya lo hacías
                var nuevaInternacion = new internacion
                {
                    id_paciente = internacion.Id_paciente,
                    id_medico = internacion.Id_medico,
                    id_procedimiento = internacion.Id_procedimiento,
                    fecha_inicio = internacion.Fecha_ingreso,
                    fecha_fin = internacion.Fecha_egreso,
                    motivo = internacion.Diagnostico,
                    nro_habitacion = internacion.Nro_habitacion,
                    id_cama = internacion.Id_cama
                };

                db.internacion.Add(nuevaInternacion);

                // 6) Guardar todos los cambios juntos
                db.SaveChanges();
            }
        }

        public void Eliminar(int id_internacion)
            {
                using (var db = new Sistema_HospitalarioEntities_Conexion())
                {
                    var internacion = db.internacion.Find(id_internacion);
                    if (internacion != null)
                    {
                        db.internacion.Remove(internacion);
                        db.SaveChanges();
                    }
                }
            }

        public void Actualizar(int id_internacion, InternacionDto internacion)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var internacionExistente = db.internacion.Find(id_internacion);
                if (internacionExistente != null)
                {
                    internacionExistente.id_paciente = internacion.Id_paciente;
                    internacionExistente.id_medico = internacion.Id_medico;
                    internacionExistente.id_procedimiento = internacion.Id_procedimiento;
                    internacionExistente.fecha_inicio = internacion.Fecha_ingreso;
                    internacionExistente.fecha_fin = internacion.Fecha_egreso;
                    internacionExistente.motivo = internacion.Diagnostico;
                    internacionExistente.nro_habitacion = internacion.Nro_habitacion;
                    internacionExistente.id_cama = internacion.Id_cama;
                    db.Entry(internacionExistente).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
