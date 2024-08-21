namespace DotNetWrappers.System.Data.Common;

public interface ISelectQuery<T> : IQuery<T>
{
    T MapReader(IDbDataReaderWrapper dataReader);
    T ReadOne(IDbDataReaderWrapper dataReader);
    Task<T> ReadOneAsync(IDbDataReaderWrapper dataReader);
    IEnumerable<T> ReadAll(IDbDataReaderWrapper dataReader);
    Task<IEnumerable<T>> ReadAllAsync(IDbDataReaderWrapper dataReader);
}