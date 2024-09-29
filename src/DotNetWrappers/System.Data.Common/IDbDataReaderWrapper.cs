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

    #region DataReaderExtensions Wrappers

    bool GetBoolean(string name);
    byte GetByte(string name);
    long GetBytes(string name, long dataOffset, byte[] buffer, int bufferOffset, int length);
    char GetChar(string name);
    long GetChars(string name, long dataOffset, char[] buffer, int bufferOffset, int length);

    [EditorBrowsable(EditorBrowsableState.Never)]
    IDataReader GetData(string name);

    string GetDataTypeName(string name);
    DateTime GetDateTime(string name);
    decimal GetDecimal(string name);
    double GetDouble(string name);
    Type GetFieldType(string name);
    T GetFieldValue<T>(string name);
    Task<T> GetFieldValueAsync<T>(string name, CancellationToken cancellationToken = default(CancellationToken));
    float GetFloat(string name);
    Guid GetGuid(string name);
    short GetInt16(string name);
    int GetInt32(string name);
    long GetInt64(string name);

    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetProviderSpecificFieldType(string name);

    [EditorBrowsable(EditorBrowsableState.Never)]
    object GetProviderSpecificValue(string name);

    Stream GetStream(string name);
    string GetString(string name);
    TextReader GetTextReader(string name);
    object GetValue(string name);

    // ReSharper disable once InconsistentNaming
    bool IsDBNull(string name);

    // ReSharper disable once InconsistentNaming
    Task<bool> IsDBNullAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

    #endregion DataReaderExtensions Wrappers
}