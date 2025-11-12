using System;
using System.Collections.Generic;
using System.Linq;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;

namespace Sistema_Hospitalario.CapaDatos.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        public MedicoRepository()
        {
        }

        // Inserta un nuevo médico en la base de datos
        public (bool Ok, int IdGenerado, string Error) Insertar(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int idEspecialidad)
        {
            try
            {
                using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
                {
                    int dniint = Convert.ToInt32(dni);
                    bool existeDni = db.medico.Any(unMedico => unMedico.DNI == dniint);
                    if (existeDni)
                        return (false, 0, "Ya existe un medico registrado con ese DNI.");

                    var nuevoMedico = new medico
                    {
                        nombre = nombre,
                        apellido = apellido,
                        DNI = Convert.ToInt32(dni),
                        direccion = direccion,
                        matricula = matricula,
                        correo_electronico = correo,
                        id_especialidad = idEspecialidad
                    };

                    db.medico.Add(nuevoMedico);
                    db.SaveChanges();

                    return (true, nuevoMedico.id_medico, null);
                }
            }
            catch (Exception ex)
            {
                // Creamos un mensaje de error más detallado
                string errorMessage = ex.Message;

                // Verificamos si hay una excepción interna (el mensaje de la base de datos)
                if (ex.InnerException != null)
                {
                    errorMessage += " --> Inner Exception: " + ex.InnerException.Message;
                }

                // Devolvemos el mensaje completo
                return (false, 0, $"Error al guardar el medico: {errorMessage}");
            }
        }

        // Elimina un médico por su ID
        public void Eliminar(int idMedico)
        {
            using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
            {
                var medico = db.medico.FirstOrDefault(m => m.id_medico == idMedico);
                if (medico != null)
                {
                    try
                    {
                        db.medico.Remove(medico);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                    {
                        if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                            ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                        {
                            throw new Exception("No se puede eliminar el médico porque está asociado a otros registros.");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
        }

        // Obtener la lista de médicos con sus detalles
        public List<MostrarMedicoDTO> ObtenerMedicos()
        {

            using (var db = new Sistema_HospitalarioEntities_Conexion())
            {
                var lista = db.medico
                    .Select(m => new MostrarMedicoDTO
                    {
                        IdMedico = m.id_medico,
                        Nombre = m.nombre,
                        Apellido = m.apellido,
                        DNI = m.DNI.ToString(),
                        Direccion = m.direccion,
                        Matricula = m.matricula,
                        Correo = m.correo_electronico,
                        Especialidad = (m.especialidad != null ? m.especialidad.nombre : "ninguna")
                    })
                    .ToList();

                return lista;
            }
        }

        // Listar todos los médicos como DTOs
        public List<MedicoDto> ListarMedicos()
        {
            using (var context = new Sistema_HospitalarioEntities_Conexion())
            {
                var medicos = context.medico.ToList();
                var medicoDtos = medicos.Select(m => new MedicoDto
                {
                    Id = m.id_medico,
                    Matricula = m.matricula,
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