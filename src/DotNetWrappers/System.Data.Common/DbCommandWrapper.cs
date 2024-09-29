using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

// ReSharper disable once InconsistentNaming
public class DbCommandWrapper(DbCommand dbCommand) : IDbCommandWrapper
{
    public virtual DbCommand DbCommand { get; } = dbCommand;

    #region DbCommand Members

    [AllowNull]
    public virtual string CommandText
    {
        get => DbCommand.CommandText;
        set => DbCommand.CommandText = value;
    }

    public virtual int CommandTimeout
    {
        get => DbCommand.CommandTimeout;
        set => DbCommand.CommandTimeout = value;
    }

    public virtual CommandType CommandType
    {
        get => DbCommand.CommandType;
        set => DbCommand.CommandType = value;
    }

    public virtual IDbConnectionWrapper? Connection
    {
        get => new DbConnectionWrapper(DbCommand.Connection!);
        set => DbCommand.Connection = value?.DbConnection;
    }

    public virtual bool DesignTimeVisible
    {
        get => DbCommand.DesignTimeVisible;
        set => DbCommand.DesignTimeVisible = value;
    }

    public virtual IDbParameterCollectionWrapper Parameters { get; } = new DbParameterCollectionWrapper(dbCommand.Parameters);

    public virtual IDbTransactionWrapper? Transaction
    {
        get => new DbTransactionWrapper(DbCommand.Transaction!);
        set => DbCommand.Transaction = value?.DbTransaction;
    }

    public virtual UpdateRowSource UpdatedRowSource
    {
        get => DbCommand.UpdatedRowSource;
        set => DbCommand.UpdatedRowSource = value;
    }

    public virtual void Cancel() => DbCommand.Cancel();
    public virtual IDbParameterWrapper CreateParameter() => new DbParameterWrapper(DbCommand.CreateParameter());
    public virtual int ExecuteNonQuery() => DbCommand.ExecuteNonQuery();
    public virtual IDbDataReaderWrapper ExecuteReader() => new DbDataReaderWrapper(DbCommand.ExecuteReader());
    public virtual IDbDataReaderWrapper ExecuteReader(CommandBehavior behavior) => new DbDataReaderWrapper(DbCommand.ExecuteReader(behavior));
    public virtual Task<int> ExecuteNonQueryAsync() => DbCommand.ExecuteNonQueryAsync();
    public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken) => DbCommand.ExecuteNonQueryAsync(cancellationToken);
    public virtual async Task<IDbDataReaderWrapper> ExecuteReaderAsync() => new DbDataReaderWrapper(await DbCommand.ExecuteReaderAsync());

    public virtual async Task<IDbDataReaderWrapper> ExecuteReaderAsync(CancellationToken cancellationToken) => 
        new DbDataReaderWrapper(await DbCommand.ExecuteReaderAsync(cancellationToken));

    public virtual async Task<IDbDataReaderWrapper> ExecuteReaderAsync(CommandBehavior behavior) => new 
        DbDataReaderWrapper(await DbCommand.ExecuteReaderAsync(behavior));

    public virtual async Task<IDbDataReaderWrapper> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken) =>
        new DbDataReaderWrapper(await DbCommand.ExecuteReaderAsync(behavior, cancellationToken));

    public virtual Task<object?> ExecuteScalarAsync() => DbCommand.ExecuteScalarAsync();
    public virtual Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken) => DbCommand.ExecuteScalarAsync(cancellationToken);
    public virtual object? ExecuteScalar() => DbCommand.ExecuteScalar();
    public virtual void Prepare() => DbCommand.Prepare();
    public virtual Task PrepareAsync(CancellationToken cancellationToken = default) => DbCommand.PrepareAsync(cancellationToken);
    public virtual ValueTask DisposeAsync() => DbCommand.DisposeAsync();

    #endregion DbCommand Members

    #region IDisposable Members

    public virtual void Dispose() => DbCommand.Dispose();

    #endregion IDisposable Members

    #region Additional Members

    public virtual bool BindByName
    {
        get
        {
            var dbCommandType = DbCommand.GetType();

            try
            {
                return (bool)dbCommandType.GetProperty("BindByName")!
                    .GetValue(DbCommand)!;
            }
            catch
            {
                // DbCommand is probably a SqlCommand, which doesn't have a BindByName property,
                // so just catch the exception and return true.
                return true;
            }
        }
        set
        {
            var dbCommandType = DbCommand.GetType();

            try
            {
                dbCommandType.GetProperty("BindByName")!
                    .SetValue(DbCommand, value, null);
            }
            catch
            {
                // DbCommand is probably a SqlCommand, which doesn't have a BindByName property,
                // so just catch the exception.
            }
        }
    }

    public virtual int ArrayBindCount
    {
        get
        {
            var dbCommandType = DbCommand.GetType();

            try
            {
                return (int)dbCommandType.GetProperty("ArrayBindCount")!
                    .GetValue(DbCommand)!;
            }
            catch
            {
                // DbCommand is probably a SqlCommand, which doesn't have a BindByName property,
                // so just catch the exception and return 0.
                return 0;
            }
        }
        set
        {
            var dbCommandType = DbCommand.GetType();

            try
            {
                dbCommandType.GetProperty("ArrayBindCount")!
                    .SetValue(DbCommand, value, null);
            }
            catch
            {
                // DbCommand is probably a SqlCommand, which doesn't have a BindByName property,
                // so just catch the exception.
            }
        }
    }

    #endregion Additional Members
}