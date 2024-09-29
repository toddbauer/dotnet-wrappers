using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

// ReSharper disable once InconsistentNaming
public class DbDataReaderWrapper(DbDataReader dbDataReader) : IDbDataReaderWrapper
{
    public DbDataReader DbDataReader { get; } = dbDataReader ?? throw new ArgumentNullException(nameof(dbDataReader));
    public virtual bool HasRows => DbDataReader.HasRows;

    public virtual bool GetBoolean(int i) => DbDataReader.GetBoolean(i);
    public virtual byte GetByte(int i) => DbDataReader.GetByte(i);

    public virtual long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferOffset, int length) =>
        DbDataReader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);

    public virtual char GetChar(int i) => DbDataReader.GetChar(i);

    public virtual long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length) =>
        DbDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

    IDataReader IDataRecord.GetData(int i) => new DbDataReaderWrapper(DbDataReader.GetData(i));
    public virtual IDbDataReaderWrapper GetData(int ordinal) => new DbDataReaderWrapper(DbDataReader.GetData(ordinal));
    public virtual Type GetProviderSpecificFieldType(int ordinal) => DbDataReader.GetProviderSpecificFieldType(ordinal);
    public virtual object GetProviderSpecificValue(int ordinal) => DbDataReader.GetProviderSpecificValue(ordinal);
    public virtual int GetProviderSpecificValues(object[] values) => DbDataReader.GetProviderSpecificValues(values);
    public virtual Stream GetStream(int ordinal) => DbDataReader.GetStream(ordinal);
    public virtual TextReader GetTextReader(int ordinal) => DbDataReader.GetTextReader(ordinal);
    public virtual T GetFieldValue<T>(int ordinal) => DbDataReader.GetFieldValue<T>(ordinal);
    public virtual Task<T> GetFieldValueAsync<T>(int ordinal) => DbDataReader.GetFieldValueAsync<T>(ordinal);

    public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken) => 
        DbDataReader.GetFieldValueAsync<T>(ordinal, cancellationToken);

    public virtual Task<bool> ReadAsync() => DbDataReader.ReadAsync();
    public virtual Task<bool> ReadAsync(CancellationToken cancellationToken) => DbDataReader.ReadAsync(cancellationToken);
    public virtual Task<bool> NextResultAsync() => DbDataReader.NextResultAsync();
    public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken) => DbDataReader.NextResultAsync(cancellationToken);

    public virtual Task CloseAsync() => DbDataReader.CloseAsync();

    public virtual Task<DataTable?> GetSchemaTableAsync(CancellationToken cancellationToken = default) =>
        DbDataReader.GetSchemaTableAsync(cancellationToken);

    public virtual Task<ReadOnlyCollection<DbColumn>> GetColumnSchemaAsync(CancellationToken cancellationToken = default) =>
        DbDataReader.GetColumnSchemaAsync(cancellationToken);

    public virtual string GetDataTypeName(int i) => DbDataReader.GetDataTypeName(i);
    public virtual DateTime GetDateTime(int i) => DbDataReader.GetDateTime(i);
    public virtual decimal GetDecimal(int i) => DbDataReader.GetDecimal(i);
    public virtual double GetDouble(int i) => DbDataReader.GetDouble(i);
    public virtual Type GetFieldType(int i) => DbDataReader.GetFieldType(i);
    public virtual float GetFloat(int i) => DbDataReader.GetFloat(i);
    public virtual Guid GetGuid(int i) => DbDataReader.GetGuid(i);
    public virtual short GetInt16(int i) => DbDataReader.GetInt16(i);
    public virtual int GetInt32(int i) => DbDataReader.GetInt32(i);
    public virtual long GetInt64(int i) => DbDataReader.GetInt64(i);
    public virtual string GetName(int i) => DbDataReader.GetName(i);
    public virtual int GetOrdinal(string name) => DbDataReader.GetOrdinal(name);
    public virtual string GetString(int i) => DbDataReader.GetString(i);
    public virtual object GetValue(int i) => DbDataReader.GetValue(i);
    public virtual int GetValues(object[] values) => DbDataReader.GetValues(values);
    public virtual bool IsDBNull(int i) => DbDataReader.IsDBNull(i);
    public virtual int FieldCount => DbDataReader.FieldCount;
    public virtual object this[int i] => DbDataReader[i];
    public virtual object this[string name] => DbDataReader[name];
    public virtual void Dispose() => DbDataReader.Dispose();
    public virtual void Close() => DbDataReader.Close();
    public virtual DataTable? GetSchemaTable() => DbDataReader.GetSchemaTable();
    public virtual bool NextResult() => DbDataReader.NextResult();
    public virtual bool Read() => DbDataReader.Read();
    public virtual int Depth => DbDataReader.Depth;
    public virtual bool IsClosed => DbDataReader.IsClosed;
    public virtual int RecordsAffected => DbDataReader.RecordsAffected;
    public virtual IEnumerator GetEnumerator() => DbDataReader.GetEnumerator();
    public virtual ValueTask DisposeAsync() => DbDataReader.DisposeAsync();

    #region DataReaderExtensions Wrappers

    public virtual bool GetBoolean(string name) => DbDataReader.GetBoolean(name);
    public virtual byte GetByte(string name) => DbDataReader.GetByte(name);

    public virtual long GetBytes(string name, long dataOffset, byte[] buffer, int bufferOffset, int length) => 
        DbDataReader.GetBytes(name, dataOffset, buffer, bufferOffset, length);

    public virtual char GetChar(string name) => DbDataReader.GetChar(name);
    public virtual long GetChars(string name, long dataOffset, char[] buffer, int bufferOffset, int length) => 
        DbDataReader.GetChars(name, dataOffset, buffer, bufferOffset, length);

    public IDataReader GetData(string name) => new DbDataReaderWrapper(DbDataReader.GetData(name));
    public virtual string GetDataTypeName(string name) => DbDataReader.GetDataTypeName(name);
    public virtual DateTime GetDateTime(string name) => DbDataReader.GetDateTime(name);
    public virtual decimal GetDecimal(string name) => DbDataReader.GetDecimal(name);
    public virtual double GetDouble(string name) => DbDataReader.GetDouble(name);
    public virtual Type GetFieldType(string name) => DbDataReader.GetFieldType(name);
    public virtual T GetFieldValue<T>(string name) => DbDataReader.GetFieldValue<T>(name);

    public virtual Task<T> GetFieldValueAsync<T>(string name, CancellationToken cancellationToken = default(CancellationToken)) => 
        DbDataReader.GetFieldValueAsync<T>(name, cancellationToken);

    public virtual float GetFloat(string name) => DbDataReader.GetFloat(name);
    public virtual Guid GetGuid(string name) => DbDataReader.GetGuid(name);
    public virtual short GetInt16(string name) => DbDataReader.GetInt16(name);
    public virtual int GetInt32(string name) => DbDataReader.GetInt32(name);
    public virtual long GetInt64(string name) => DbDataReader.GetInt64(name);
    public virtual Type GetProviderSpecificFieldType(string name) => DbDataReader.GetProviderSpecificFieldType(name);
    public virtual object GetProviderSpecificValue(string name) => DbDataReader.GetProviderSpecificValue(name);
    public virtual Stream GetStream(string name) => DbDataReader.GetStream(name);
    public virtual string GetString(string name) => DbDataReader.GetString(name);
    public virtual TextReader GetTextReader(string name) => DbDataReader.GetTextReader(name);
    public virtual object GetValue(string name) => DbDataReader.GetValue(name);
    public virtual bool IsDBNull(string name) => DbDataReader.IsDBNull(name);

    public virtual Task<bool> IsDBNullAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) => 
        DbDataReader.IsDBNullAsync(name, cancellationToken);

    #endregion DataReaderExtensions Wrappers
}