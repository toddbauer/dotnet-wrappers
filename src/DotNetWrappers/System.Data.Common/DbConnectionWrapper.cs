using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace DotNetWrappers.System.Data.Common;

public class DbConnectionWrapper(DbConnection dbConnection) : IDbConnectionWrapper
{
    public virtual DbConnection DbConnection { get; } = dbConnection;

    #region DbConnection Members

    [AllowNull]
    public virtual string ConnectionString
    {
        get => DbConnection.ConnectionString;
        set => DbConnection.ConnectionString = value;
    }

    public virtual int ConnectionTimeout => DbConnection.ConnectionTimeout;
    public virtual string Database => DbConnection.Database;
    public virtual string DataSource => DbConnection.DataSource;
    public virtual string ServerVersion => DbConnection.ServerVersion;
    public virtual ConnectionState State => DbConnection.State;
    public virtual IDbTransactionWrapper BeginTransaction() => new DbTransactionWrapper(DbConnection.BeginTransaction());

    public virtual IDbTransactionWrapper BeginTransaction(IsolationLevel isolationLevel) =>
        new DbTransactionWrapper(DbConnection.BeginTransaction(isolationLevel));

    public virtual async ValueTask<IDbTransactionWrapper> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        new DbTransactionWrapper(await DbConnection.BeginTransactionAsync(cancellationToken));

    public virtual async ValueTask<IDbTransactionWrapper> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) =>
        new DbTransactionWrapper(await DbConnection.BeginTransactionAsync(isolationLevel, cancellationToken));

    public virtual void Close()
    {
        DbConnection.Close();
    }

    public virtual Task CloseAsync()
    {
        return DbConnection.CloseAsync();
    }

    public virtual ValueTask DisposeAsync() => DbConnection.DisposeAsync();
    public virtual void ChangeDatabase(string databaseName) => DbConnection.ChangeDatabase(databaseName);

    public virtual Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default) =>
        DbConnection.ChangeDatabaseAsync(databaseName, cancellationToken);

    public virtual bool CanCreateBatch => DbConnection.CanCreateBatch;
    public virtual DbBatch CreateBatch() => DbConnection.CreateBatch();
    public virtual IDbCommandWrapper CreateCommand() => new DbCommandWrapper(DbConnection.CreateCommand());
    public virtual void EnlistTransaction(Transaction? transaction) => DbConnection.EnlistTransaction(transaction);
    public virtual DataTable GetSchema() => DbConnection.GetSchema();
    public virtual DataTable GetSchema(string collectionName) => DbConnection.GetSchema(collectionName);

    public virtual DataTable GetSchema(string collectionName, string?[] restrictionValues) => 
        DbConnection.GetSchema(collectionName, restrictionValues);

    public virtual Task<DataTable> GetSchemaAsync(CancellationToken cancellationToken = default) => DbConnection.GetSchemaAsync(cancellationToken);

    public virtual Task<DataTable> GetSchemaAsync(string collectionName, CancellationToken cancellationToken = default) =>
        DbConnection.GetSchemaAsync(collectionName, cancellationToken);
    
    public virtual Task<DataTable> GetSchemaAsync(string collectionName, string?[] restrictionValues, CancellationToken cancellationToken = default) =>
        DbConnection.GetSchemaAsync(collectionName, restrictionValues, cancellationToken);

    public virtual void Open() => DbConnection.Open();
    public virtual Task OpenAsync() => DbConnection.OpenAsync();
    public virtual Task OpenAsync(CancellationToken cancellationToken) => DbConnection.OpenAsync(cancellationToken);

    #endregion DbConnection Members

    #region IDisposable Members

    public virtual void Dispose()
    {
        DbConnection.Dispose();
    }

    #endregion IDisposble Members
}