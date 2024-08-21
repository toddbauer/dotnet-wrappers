using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public interface IDbTransactionWrapper : IDbTransaction, IAsyncDisposable
{
    DbTransaction DbTransaction { get; }

    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);

    #region Savepoints

    bool SupportsSavepoints { get; }
    Task SaveAsync(string savepointName, CancellationToken cancellationToken = default);
    Task RollbackAsync(string savepointName, CancellationToken cancellationToken = default);
    Task ReleaseAsync(string savepointName, CancellationToken cancellationToken = default);
    void Save(string savepointName);
    void Rollback(string savepointName);
    void Release(string savepointName);

    #endregion Savepoints
}