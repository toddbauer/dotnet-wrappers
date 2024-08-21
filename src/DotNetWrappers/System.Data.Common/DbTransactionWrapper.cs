using System.Data.Common;
using System.Data;

namespace DotNetWrappers.System.Data.Common;

public class DbTransactionWrapper(DbTransaction dbTransaction) : IDbTransactionWrapper
{
    public DbTransaction DbTransaction { get; } = dbTransaction ?? throw new ArgumentNullException(nameof(dbTransaction));

    public virtual Task CommitAsync(CancellationToken cancellationToken = default) => DbTransaction.CommitAsync(cancellationToken);
    public virtual Task RollbackAsync(CancellationToken cancellationToken = default) => DbTransaction.RollbackAsync(cancellationToken);
    public virtual void Dispose() => DbTransaction.Dispose();

    #region DbTransaction.Savepoints

    public virtual bool SupportsSavepoints => DbTransaction.SupportsSavepoints;
    public virtual Task SaveAsync(string savepointName, CancellationToken cancellationToken = default) => 
        DbTransaction.SaveAsync(savepointName, cancellationToken);

    public virtual Task RollbackAsync(string savepointName, CancellationToken cancellationToken = default) => 
        DbTransaction.RollbackAsync(savepointName, cancellationToken);

    public virtual Task ReleaseAsync(string savepointName, CancellationToken cancellationToken = default) => 
        DbTransaction.ReleaseAsync(savepointName, cancellationToken);

    public virtual void Save(string savepointName) => DbTransaction.Save(savepointName);
    public virtual void Rollback(string savepointName) => DbTransaction.Rollback(savepointName);
    public virtual void Release(string savepointName) => DbTransaction.Release(savepointName);

    #endregion DbTransaction.Savepoints

    #region IDbTransaction

    public virtual IDbConnection? Connection => DbTransaction.Connection;
    public virtual IsolationLevel IsolationLevel => DbTransaction.IsolationLevel;
    public virtual void Commit() => DbTransaction.Commit();
    public virtual void Rollback() => DbTransaction.Rollback();

    #endregion IDbTransaction

    #region IAsyncDisposable

    public virtual ValueTask DisposeAsync() => DbTransaction.DisposeAsync();

    #endregion IAsyncDisposable
}