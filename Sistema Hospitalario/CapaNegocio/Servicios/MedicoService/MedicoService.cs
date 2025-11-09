using System;
using System.Collections.Generic;
using System.Linq;

using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService
{
    public class MedicoService
    {
        private readonly IMedicoRepository _repo;

        public MedicoService()
        {
            _repo = new MedicoRepository();
        }
        public List<MostrarMedicoDTO> ObtenerMedicos(string campo = null, string valor = null)
        {
            var listaCompleta = _repo.ObtenerMedicos();

            List<MostrarMedicoDTO> resultado;

            // FILTRAMOS SI HAY VALOR
            if (!string.IsNullOrEmpty(valor))
            {
                string valorLower = valor.ToLower();
                switch (campo)
                {
                    case "Nombre":
                        resultado = listaCompleta.Where(m => m.Nombre.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Apellido":
                        resultado = listaCompleta.Where(m => m.Apellido.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "DNI":
                        resultado = listaCompleta.Where(m => m.DNI.StartsWith(valor)).ToList();
                        break;
                    case "Direccion":
                        resultado = listaCompleta.Where(m => m.Direccion.StartsWith(valor)).ToList();
                        break;
                    case "Matricula":
                        resultado = listaCompleta.Where(m => m.Matricula.StartsWith(valor)).ToList();
                        break;
                    case "Correo":
                        resultado = listaCompleta.Where(m => m.Correo.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    case "Especialidad":
                        resultado = listaCompleta.Where(m => m.Especialidad.ToLower().StartsWith(valorLower)).ToList();
                        break;
                    default: //IdMedico
                        resultado = listaCompleta.Where(m => m.IdMedico.ToString() == valor).ToList();
                        break;
                }
            }
            else
            {

                resultado = listaCompleta;
            }

            // ordenamiento
            if (string.IsNullOrEmpty(valor))
            {
                switch (campo)
                {
                    case "Nombre":
                        resultado = resultado.OrderBy(m => m.Nombre).ToList();
                        break;
                    case "Apellido":
                        resultado = resultado.OrderBy(m => m.Apellido).ToList();
                        break;
                    case "DNI":
                        resultado = resultado.OrderBy(m => m.DNI).ToList();
                        break;
                    case "Direccion":
                        resultado = resultado.OrderBy(m => m.Direccion).ToList();
                        break;
                    case "Matricula":
                        resultado = resultado.OrderBy(m => m.Matricula).ToList();
                        break;
                    case "Correo":
                        resultado = resultado.OrderBy(m => m.Correo).ToList();
                        break;
                    case "Especialidad":
                        resultado = resultado.OrderBy(m => m.Especialidad).ToList();
                        break;
                    default: // Orden por defecto (IdMedico)
                        resultado = resultado.OrderBy(m => m.IdMedico).ToList();
                        break;
                }
            }

            return resultado;
        }

        public void AgregarMedico(string nombre, string apellido, string dni, string direccion, string matricula, string correo, int idEspecialidad)
        {
            var (Ok, _, Error) = _repo.Insertar(nombre, apellido, dni, direccion, matricula, correo, idEspecialidad);
            if (!Ok)
            {
                throw new System.Exception(Error);
            }
        }

        public void EliminarMedico(int idMedico)
        {
            _repo.Eliminar(idMedico);
        }
        public List<MedicoDto> ListarMedicos()
        {
            return _repo.ListarMedicos();
        }

        public List<MedicoSimpleDTO> ObtenerMedicosParaComboBox()
        {
            var todosLosMedicos = _repo.ObtenerMedicos();

            var resultado = todosLosMedicos
                            .OrderBy(m => m.Apellido).ThenBy(m => m.Nombre).ThenBy(m => m.DNI)
                            .Select(m => new MedicoSimpleDTO
                            {
                                Id = m.IdMedico,
                                NombreCompletoYDNI = $"{m.Apellido}, {m.Nombre} ({m.DNI}) esp:{m.Especialidad}"
                            })
                            .ToList();
            return resultado;
        }
    }
}