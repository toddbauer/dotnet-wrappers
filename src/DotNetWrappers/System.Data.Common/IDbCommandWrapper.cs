using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

public interface IDbCommandWrapper : IDisposable
{
    DbCommand DbCommand { get; }

    #region DbCommand Members

    [DefaultValue("")]
    [RefreshProperties(RefreshProperties.All)]
    [AllowNull]
    string CommandText { get; set; }

    int CommandTimeout { get; set; }

    [DefaultValue(global::System.Data.CommandType.Text)]
    [RefreshProperties(RefreshProperties.All)]
    CommandType CommandType { get; set; }

    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbConnectionWrapper? Connection { get; set; }

    [DefaultValue(true)]
    [DesignOnly(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    bool DesignTimeVisible { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbParameterCollectionWrapper Parameters { get; }

    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbTransactionWrapper? Transaction { get; set; }

    [DefaultValue(global::System.Data.UpdateRowSource.Both)]
    UpdateRowSource UpdatedRowSource { get; set; }

    void Cancel();
    IDbParameterWrapper CreateParameter();
    int ExecuteNonQuery();
    IDbDataReaderWrapper ExecuteReader();
    IDbDataReaderWrapper ExecuteReader(CommandBehavior behavior);
    Task<int> ExecuteNonQueryAsync();
    Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken);
    Task<IDbDataReaderWrapper> ExecuteReaderAsync();
    Task<IDbDataReaderWrapper> ExecuteReaderAsync(CancellationToken cancellationToken);
    Task<IDbDataReaderWrapper> ExecuteReaderAsync(CommandBehavior behavior);
    Task<IDbDataReaderWrapper> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken);
    Task<object?> ExecuteScalarAsync();
    Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken);
    object? ExecuteScalar();
    void Prepare();
    Task PrepareAsync(CancellationToken cancellationToken = default);
    ValueTask DisposeAsync();

    #endregion DbCommand Members

    #region Additional Members

    bool BindByName { get; set; }
    int ArrayBindCount { get; set; }

    #endregion Additional Members
}