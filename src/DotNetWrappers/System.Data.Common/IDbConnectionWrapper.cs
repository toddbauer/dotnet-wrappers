using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace DotNetWrappers.System.Data.Common;

public interface IDbConnectionWrapper : IDisposable
{
    DbConnection DbConnection { get; }

    #region DbConnection Members

    [DefaultValue("")]
    [SettingsBindable(true)]
    [RefreshProperties(RefreshProperties.All)]
#pragma warning disable 618 // ignore obsolete warning about RecommendedAsConfigurable to use SettingsBindableAttribute
    [RecommendedAsConfigurable(true)]
#pragma warning restore 618
    [AllowNull]
    string ConnectionString { get; set; }

    int ConnectionTimeout { get; }
    string Database { get; }
    string DataSource { get; }


    [Browsable(false)]
    string ServerVersion { get; }

    [Browsable(false)]
    ConnectionState State { get; }

    IDbTransactionWrapper BeginTransaction();
    IDbTransactionWrapper BeginTransaction(IsolationLevel isolationLevel);
    ValueTask<IDbTransactionWrapper> BeginTransactionAsync(CancellationToken cancellationToken = default);
    ValueTask<IDbTransactionWrapper> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    void Close();
    Task CloseAsync();
    ValueTask DisposeAsync();
    void ChangeDatabase(string databaseName);
    Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default);
    bool CanCreateBatch { get; }
    DbBatch CreateBatch();
    IDbCommandWrapper CreateCommand();
    void EnlistTransaction(Transaction? transaction);
    DataTable GetSchema();
    DataTable GetSchema(string collectionName);
    DataTable GetSchema(string collectionName, string?[] restrictionValues);
    Task<DataTable> GetSchemaAsync(CancellationToken cancellationToken = default);
    Task<DataTable> GetSchemaAsync(string collectionName, CancellationToken cancellationToken = default);
    Task<DataTable> GetSchemaAsync(string collectionName, string?[] restrictionValues, CancellationToken cancellationToken = default);
    void Open();
    Task OpenAsync();
    Task OpenAsync(CancellationToken cancellationToken);

    #endregion DbConnection Members
}