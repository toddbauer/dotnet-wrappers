using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public class DbContext(IQueryExecutor queryExecutor) : IDbContext
{
    public DbContext(Func<IDbConnectionWrapper> dbConnectionFactory) : this(new QueryExecutor(dbConnectionFactory))
    {
    }

    public DbContext(Func<DbConnection> dbConnectionFactory) : this(new QueryExecutor(dbConnectionFactory))
    {
    }

    public virtual IEnumerable<T> FindAll<T>(ISelectQuery<T> query)
    {
        return queryExecutor.ExecuteQuery<IEnumerable<T>, T>(query, dbCommand =>
        {
            using var reader = dbCommand.ExecuteReader();
            return query.ReadAll(reader);
        });
    }

    public virtual async Task<IEnumerable<T>> FindAllAsync<T>(ISelectQuery<T> query)
    {
        return await queryExecutor.ExecuteQueryAsync<IEnumerable<T>, T>(query, dbCommand =>
        {
            using var reader = dbCommand.ExecuteReader();
            return query.ReadAllAsync(reader);
        });
    }

    public virtual T FindOne<T>(ISelectQuery<T> query)
    {
        return queryExecutor.ExecuteQuery(query, dbCommand =>
        {
            using var reader = dbCommand.ExecuteReader();
            return query.ReadOne(reader);
        });
    }

    public virtual async Task<T> FindOneAsync<T>(ISelectQuery<T> query)
    {
        return await queryExecutor.ExecuteQueryAsync(query, async dbCommand =>
        {
            using var reader = await dbCommand.ExecuteReaderAsync();
            return await query.ReadOneAsync(reader);
        });
    }

    public virtual int Modify<T>(IModifyQuery<T> query)
    {
        return queryExecutor.ExecuteQuery(query, dbCommand => dbCommand.ExecuteNonQuery());
    }

    public virtual async Task<int> ModifyAsync<T>(IModifyQuery<T> query)
    {
        return await queryExecutor.ExecuteQueryAsync(query, async dbCommand => await dbCommand.ExecuteNonQueryAsync());
    }

    public virtual TR EvaluateScalar<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate)
    {
        var obj = queryExecutor.ExecuteQuery(query, dbCommand => dbCommand.ExecuteScalar());
        return predicate(obj!);
    }

    public virtual async Task<TR> EvaluateScalarAsync<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate)
    {
        var obj = await queryExecutor.ExecuteQueryAsync(query, async dbCommand => await dbCommand.ExecuteScalarAsync());
        return predicate(obj!);
    }
}