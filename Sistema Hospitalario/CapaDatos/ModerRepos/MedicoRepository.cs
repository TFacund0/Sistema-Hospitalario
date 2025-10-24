using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System;
using System.Collections.Generic;
using System.Linq;

public class MedicoRepository : IMedicoRepository
{
    public List<MostrarMedicoDTO> GetAll()
    {
        using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
        {
            var lista = (from m in db.medico
                         join e in db.especialidad on m.id_especialidad equals e.id_especialidad into espJoin
                         from e in espJoin.DefaultIfEmpty() // Left join
                         select new MostrarMedicoDTO
                         {
                             IdMedico = m.id_medico,
                             Nombre = m.nombre,
                             Apellido = m.apellido,
                             DNI = m.DNI.ToString(),
                             Direccion = m.direccion,
                             Matricula = m.matricula,
                             Correo = m.correo_electronico,
                             Especialidad = e != null ? e.nombre : "Ninguna"
                         }).ToList();

            return lista;
        }
    }

    public void Insertar(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int? idEspecialidad)
    {
        using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
        {
            var nuevo = new medico
            {
                nombre = nombre,
                apellido = apellido,
                DNI = Convert.ToInt32(dni),
                direccion = direccion,
                matricula = matricula,
                correo_electronico = correo,
                id_especialidad = (int)idEspecialidad // puede ser null
            };

            db.medico.Add(nuevo);
            db.SaveChanges();
        }
    }

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
                        throw new Exception("No se puede eliminar el médico porque está asociado a otros registros (por ejemplo, turnos).");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}
