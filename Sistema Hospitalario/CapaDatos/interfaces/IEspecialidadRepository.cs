using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Hospitalario.CapaNegocio.DTOs.EspecialidadDTO;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la gestión del catálogo de especialidades médicas.
    /// </summary>
    public interface IEspecialidadRepository
    {
        /// <summary>
        /// Obtiene el listado completo de todas las especialidades registradas.
        /// </summary>
        /// <returns>Lista de <see cref="EspecialidadDTO"/>.</returns>
        List<EspecialidadDTO> GetAll();

        /// <summary>
        /// Registra una nueva especialidad en la base de datos.
        /// </summary>
        /// <param name="nombre">Nombre de la especialidad.</param>
        void Insertar(string nombre);

        /// <summary>
        /// Elimina una especialidad del sistema buscando por su nombre.
        /// </summary>
        /// <param name="nombre">Nombre de la especialidad a eliminar.</param>
        void Eliminar(string nombre);
    }
}
