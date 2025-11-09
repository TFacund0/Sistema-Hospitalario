using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        public PacienteRepository()
        {
        }
        public List<PacienteDto> GetAll()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.paciente
                    .Where(p => p.estado_paciente.nombre == "Activo")
                    .Select(p => new PacienteDto
                    {
                        Id = p.id_paciente,
                        Nombre = p.nombre,
                        Apellido = p.apellido,
                        Fecha_nacimiento = p.fecha_nacimiento,
                        Observaciones = p.observaciones,
                        Dni = p.dni,
                        Direccion = p.direccion,
                        Email = p.correo_electronico,
                        Estado_paciente = p.estado_paciente.nombre
                    })
                    .ToList();
            }
        }

        public void Insertar(PacienteDto paciente)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var estado = db.estado_paciente.FirstOrDefault(e => e.nombre == paciente.Estado_paciente);
                
                if (estado == null)
                {
                    throw new Exception($"No se encontró el estado de paciente '{paciente.Estado_paciente}'");
                }

                db.paciente.Add(new paciente
                {
                    nombre = paciente.Nombre,
                    apellido = paciente.Apellido,
                    fecha_nacimiento = paciente.Fecha_nacimiento,
                    observaciones = paciente.Observaciones,
                    dni = paciente.Dni,
                    direccion = paciente.Direccion,
                    correo_electronico = paciente.Email,
                    id_estado_paciente = estado.id_estado_paciente,
                });
                db.SaveChanges();
            }
        }

        public void Eliminar(int id_paciente)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var paciente = db.paciente.FirstOrDefault(p => p.id_paciente == id_paciente);
                if (paciente != null)
                {
                    db.paciente.Remove(paciente);
                    db.SaveChanges();
                }
            }
        }

        public void Actualizar(int id_paciente, PacienteDto pacienteActualizado)
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var paciente = db.paciente.FirstOrDefault(p => p.id_paciente == id_paciente);
                var estado = db.estado_paciente.FirstOrDefault(e => e.nombre == paciente.estado_paciente.nombre);

                if (estado == null)
                {
                    throw new Exception($"No se encontró el estado de paciente '{paciente.estado_paciente.nombre}'");
                }
                
                if (paciente != null)
                {
                    paciente.nombre = pacienteActualizado.Nombre;
                    paciente.apellido = pacienteActualizado.Apellido;
                    paciente.fecha_nacimiento = pacienteActualizado.Fecha_nacimiento;
                    paciente.observaciones = pacienteActualizado.Observaciones;
                    paciente.dni = pacienteActualizado.Dni;
                    paciente.direccion = pacienteActualizado.Direccion;
                    paciente.correo_electronico = pacienteActualizado.Email;
                    paciente.id_estado_paciente = estado.id_estado_paciente;
                    db.SaveChanges();
                }
            }
        }

        public List<EstadoPacienteDto> GetEstados()
        {
            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                return db.estado_paciente
                    .Select(e => new EstadoPacienteDto
                    {
                        Id = e.id_estado_paciente,
                        Nombre = e.nombre
                    })
                    .ToList();
            }
        }
    }
}