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
    }
}

