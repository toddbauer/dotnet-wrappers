
namespace DotNetWrappers.System.Data.Common;

public abstract class SelectQueryBase<T> : QueryBase<T>, ISelectQuery<T>
{
    public abstract T MapReader(IDbDataReaderWrapper dbDataReader);

    public virtual T ReadOne(IDbDataReaderWrapper dbDataReader)
    {
        return dbDataReader.Read() ? MapReader(dbDataReader) : default!;
    }

    public virtual async Task<T> ReadOneAsync(IDbDataReaderWrapper dbDataReader)
    {
        return await dbDataReader.ReadAsync() ? MapReader(dbDataReader) : default!;
    }

    public virtual IEnumerable<T> ReadAll(IDbDataReaderWrapper dbDataReader)
    {
        var list = new List<T>();

        while(dbDataReader.Read())
            list.Add(MapReader(dbDataReader));

        return list;
    }

    public virtual async Task<IEnumerable<T>> ReadAllAsync(IDbDataReaderWrapper dbDataReader)
    {
        var list = new List<T>();

        while (await dbDataReader.ReadAsync())
            list.Add(MapReader(dbDataReader));

        return list;
    }
}