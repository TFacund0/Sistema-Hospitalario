using Sistema_Hospitalario.CapaDatos;
using Sistema_Hospitalario.CapaDatos.Interfaces;
using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.ConsultaDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.MedicoDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.moderDTO;
using Sistema_Hospitalario.CapaNegocio.DTOs.PacienteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.MedicoService
{
    public class MedicoService
    {
        private readonly IMedicoRepository _repo;

        public MedicoService()
        {
            _repo = new MedicoRepository();
        }
        public int ObtenerConteoTotalPacientes()
        {
            return _repo.ContarTotalPacientes();
        }
        public List<PacienteListadoMedicoDto> ObtenerPacientes(string nombre, string apellido, string dni, DateTime? fechaTurno)
        {
            // 1. Obtenemos la lista "maestra" completa
            var listaMaestra = _repo.ObtenerTodosParaMedico(fechaTurno);

            // 2. Aplicamos filtros en memoria (LINQ)

            if (!string.IsNullOrEmpty(nombre))
            {
                listaMaestra = listaMaestra.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(apellido))
            {
                listaMaestra = listaMaestra.Where(p => p.Apellido.ToLower().Contains(apellido.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(dni))
            {
                listaMaestra = listaMaestra.Where(p => p.Dni.StartsWith(dni)).ToList();
            }

            // (Aquí iría el filtro por Fecha Ultimo Turno si lo implementamos)

            return listaMaestra.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();
        }

        public (bool Ok, string Error) RegistrarConsulta(ConsultaAltaDTO dto, int idMedicoLogueado)
        {
            try
            {
                // --- VERIFICACIONES ---
                if (string.IsNullOrWhiteSpace(dto.DniPaciente))
                    return (false, "El DNI del paciente es obligatorio.");

                if (string.IsNullOrWhiteSpace(dto.Motivo))
                    return (false, "El Motivo de la consulta es obligatorio.");

                // Convertimos el DNI de string a int para buscar
                if (!int.TryParse(dto.DniPaciente, out int dniPacienteNum))
                    return (false, "El formato del DNI es incorrecto (debe ser numérico).");

                // Buscamos al paciente en la BD
                paciente pacienteEncontrado;
                using (var db = new Sistema_Hospitalario.CapaDatos.Sistema_HospitalarioEntities_Conexion())
                {
                    // Usamos 'FirstOrDefault' que es seguro
                    pacienteEncontrado = db.paciente.FirstOrDefault(p => p.dni == dniPacienteNum);
                }

                // Verificación 1: ¿Existe el paciente?
                if (pacienteEncontrado == null)
                {
                    return (false, $"No se encontró ningún paciente con el DNI {dto.DniPaciente}.");
                }

                // --- CREACIÓN DEL OBJETO ---
                var nuevaConsulta = new Sistema_Hospitalario.CapaDatos.Consulta
                {
                    motivo = dto.Motivo,
                    diagnostico = dto.Diagnostico,
                    tratamiento = dto.Tratamiento,
                    fecha_consulta = dto.Fecha,

                    // Asignamos las llaves
                    id_medico = idMedicoLogueado,
                    id_paciente = pacienteEncontrado.id_paciente
                };

                // --- GUARDADO ---
                _repo.InsertarConsulta(nuevaConsulta);

                return (true, null); // ¡Éxito!
            }
            catch (Exception ex)
            {
                return (false, "Error inesperado: " + ex.Message);
            }
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

        public string ObtenerHistorialFormateado(string idPaciente, int filtroDniMedico, DateTime? filtroFecha)
        {
            
            // 1. Traemos las dos listas
            var listaConsultas = _repo.ObtenerHistorialConsultas(Convert.ToInt32(idPaciente));
            var listaInternaciones = _repo.ObtenerHistorialInternaciones(Convert.ToInt32(idPaciente));

            var listaCompleta = listaConsultas.Concat(listaInternaciones).ToList();

            string prueba = filtroDniMedico.ToString();
            if (!string.IsNullOrWhiteSpace(prueba))
            {
                // Filtra por DNI de médico (si el DNI en la BD es int, hay que convertir)
                listaCompleta = listaCompleta.Where(h => h.DniMedico == prueba).ToList();
            }

            if (filtroFecha.HasValue)
            {
                // Filtra por día (ignora la hora)
                listaCompleta = listaCompleta.Where(h => h.Fecha.Date == filtroFecha.Value.Date).ToList();
            }

            // 4. Ordenamos la lista final por fecha, de más nueva a más vieja
            var listaOrdenada = listaCompleta.OrderByDescending(h => h.Fecha);

            // 5. Convertimos la lista en un solo string (¡La Magia!)
            if (!listaOrdenada.Any())
            {
                return "--- No se encontraron registros para este paciente o filtros seleccionados ---";
            }

            var sb = new StringBuilder();
            foreach (var item in listaOrdenada)
            {
                sb.AppendLine($"==================================================");
                sb.AppendLine($"  {item.Tipo.ToUpper()} - {item.Fecha.ToString("dd/MM/yyyy HH:mm")} hs.");
                sb.AppendLine($"  Médico: {item.NombreMedico} (DNI: {item.DniMedico})");
                sb.AppendLine($"--------------------------------------------------");
                sb.AppendLine($"Motivo/Obs: {item.Motivo}");
                sb.AppendLine($"Diagnóstico/Proced: {item.Diagnostico}");
                sb.AppendLine($"Tratamiento: {item.Tratamiento}");
                sb.AppendLine($"==================================================\r\n"); // \r\n = nueva línea
            }

            return sb.ToString();
        }
    }
}