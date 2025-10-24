using Sistema_Hospitalario.CapaDatos.interfaces;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using System.Collections.Generic;

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
}
