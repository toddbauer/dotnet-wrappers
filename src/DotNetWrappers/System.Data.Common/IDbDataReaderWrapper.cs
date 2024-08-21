using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

public interface IDbDataReaderWrapper : IDataReader, IEnumerable, IAsyncDisposable
{
    DbDataReader DbDataReader { get; }

    bool HasRows { get; }
    int VisibleFieldCount => FieldCount;
    Task CloseAsync();
    Task<DataTable?> GetSchemaTableAsync(CancellationToken cancellationToken = default);
    Task<ReadOnlyCollection<DbColumn>> GetColumnSchemaAsync(CancellationToken cancellationToken = default);
    new IDbDataReaderWrapper GetData(int ordinal);

    [EditorBrowsable(EditorBrowsableState.Never)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)]
    Type GetProviderSpecificFieldType(int ordinal);

    [EditorBrowsable(EditorBrowsableState.Never)]
    object GetProviderSpecificValue(int ordinal);

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetProviderSpecificValues(object[] values);

    Stream GetStream(int ordinal);
    TextReader GetTextReader(int ordinal);
    T GetFieldValue<T>(int ordinal);
    Task<T> GetFieldValueAsync<T>(int ordinal);
    Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken);
    Task<bool> ReadAsync();
    Task<bool> ReadAsync(CancellationToken cancellationToken);
    Task<bool> NextResultAsync();
    Task<bool> NextResultAsync(CancellationToken cancellationToken);
}