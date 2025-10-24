using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System.Collections.Generic;
using System.Linq;

public class MedicoService
{
    private readonly IMedicoRepository _repo;

    public MedicoService(IMedicoRepository repo)
    {
        _repo = repo;
    }

    public List<MostrarMedicoDTO> ObtenerMedicos()
    {
        return _repo.GetAll();
    }

    public void AgregarMedico(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int? idEspecialidad)
    {
        _repo.Insertar(nombre, apellido, dni, direccion, matricula, correo, idEspecialidad);
    }

    public void EliminarMedico(int idMedico)
    {
        _repo.Eliminar(idMedico);
    }

    public List<MedicoDto> ListarMedicos()
    {
        using (var context = new Sistema_HospitalarioEntities_Conexion())
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
