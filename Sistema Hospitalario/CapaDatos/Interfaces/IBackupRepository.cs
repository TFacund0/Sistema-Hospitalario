using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema_Hospitalario.CapaDatos.Interfaces
{
    public interface IBackupRepository
    {
        Task BackupAsync(string backupFullPath, IProgress<int> progreso = null, CancellationToken ct = default);
        Task RestoreAsync(string backupFullPath, IProgress<int> progreso = null, CancellationToken ct = default);
    }
}
