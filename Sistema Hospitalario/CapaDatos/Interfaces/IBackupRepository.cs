using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de persistencia de respaldo y restauración 
    /// de la base de datos.
    /// </summary>
    public interface IBackupRepository
    {
        /// <summary>
        /// Ejecuta el comando SQL nativo para realizar un backup completo de la base de datos.
        /// </summary>
        /// <param name="backupFullPath">Ruta completa y nombre del archivo .bak resultante.</param>
        /// <param name="progreso">Interfáz para reportar el avance de la operación al llamador.</param>
        /// <param name="ct">Token para permitir la cancelación de la operación asíncrona.</param>
        /// <returns>Una tarea que representa la ejecución del comando en el servidor SQL.</returns>
        Task BackupAsync(string backupFullPath, IProgress<int> progreso = null, CancellationToken ct = default);

        /// <summary>
        /// Ejecuta el comando SQL nativo para restaurar la base de datos a partir de un archivo .bak.
        /// </summary>
        /// <param name="backupFullPath">Ruta completa del archivo .bak de origen.</param>
        /// <param name="progreso">Interfáz para reportar el avance de la operación al llamador.</param>
        /// <param name="ct">Token para permitir la cancelación de la operación asíncrona.</param>
        /// <returns>Una tarea que representa la ejecución del comando en el servidor SQL.</returns>
        Task RestoreAsync(string backupFullPath, IProgress<int> progreso = null, CancellationToken ct = default);
    }
}
