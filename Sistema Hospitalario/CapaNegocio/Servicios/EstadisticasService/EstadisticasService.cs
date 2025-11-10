using Sistema_Hospitalario.CapaDatos.Repositories;
using Sistema_Hospitalario.CapaNegocio.DTOs.EstadisticasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaNegocio.Servicios.EstadisticasService
{
    public class EstadisticasService
    {
        private readonly EstadisticasRepository _repo;

        public EstadisticasService()
        {
            _repo = new EstadisticasRepository();
        }

        public List<PacientesPorDiaDto> ObtenerPacientesSemana()
        {
            var lista = new List<PacientesPorDiaDto>();

            DateTime hoy = DateTime.Today;
            DateTime inicio = hoy.AddDays(-6);

            for (DateTime dia = inicio; dia <= hoy; dia = dia.AddDays(1))
            {
                int cantActivos = _repo.ContarPacientesPorEstadoYFecha("activo", dia.Date);
                int cantAltas = _repo.ContarPacientesPorEstadoYFecha("alta", dia.Date);

                lista.Add(new PacientesPorDiaDto
                {
                    Fecha = dia.Date,
                    CantActivos = cantActivos,
                    CantAltas = cantAltas
                });
            }

            return lista;
        }

        public CamasDistribucionDto ObtenerDistribucionCamas()
        {
            int ocupadas = _repo.ContarCamasPorDisponibilidad("ocupada");
            int disponibles = _repo.ContarCamasPorDisponibilidad("disponible");

            return new CamasDistribucionDto
            {
                Ocupadas = ocupadas,
                Disponibles = disponibles
            };
        }
        public List<TurnosPorDiaDto> ObtenerTurnosPorDiaUltimaSemana()
        {
            DateTime hoy = DateTime.Today;
            DateTime inicio = hoy.AddDays(-6);

            // El repo devuelve un diccionarioFecha->Cantidad
            var conteo = _repo.ObtenerConteoTurnosPorDia(inicio, hoy);

            var lista = new List<TurnosPorDiaDto>();

            // Armamos la semana completa, incluyendo días sin turnos (cantidad 0)
            for (DateTime d = inicio; d <= hoy; d = d.AddDays(1))
            {
                int cantidad = conteo.ContainsKey(d.Date) ? conteo[d.Date] : 0;

                lista.Add(new TurnosPorDiaDto
                {
                    Fecha = d.Date,
                    Cantidad = cantidad
                });
            }

            return lista;
        }

        public TurnosEstadosDistribucionDto ObtenerDistribucionEstadosTurnosUltimaSemana()
        {
            DateTime hoy = DateTime.Today;
            DateTime inicio = hoy.AddDays(-6);

            var dic = _repo.ObtenerDistribucionEstadosTurnos(inicio, hoy);

            // normalizamos claves a lower
            int pendientes = dic.ContainsKey("pendiente") ? dic["pendiente"] : 0;
            int atendidos = dic.ContainsKey("atendido") ? dic["atendido"] : 0;
            int cancelados = dic.ContainsKey("cancelado") ? dic["cancelado"] : 0;

            return new TurnosEstadosDistribucionDto
            {
                Pendientes = pendientes,
                Atendidos = atendidos,
                Cancelados = cancelados
            };
        }
    }
}

