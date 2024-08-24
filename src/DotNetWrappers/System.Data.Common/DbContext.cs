using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public class DbContext(IQueryExecutor queryExecutor) : IDbContext
{
    protected internal IQueryExecutor QueryExecutor { get; } = queryExecutor;

    public DbContext(Func<IDbConnectionWrapper> dbConnectionFactory) : this(new QueryExecutor(dbConnectionFactory))
    {
    }

    public DbContext(Func<DbConnection> dbConnectionFactory) : this(new QueryExecutor(dbConnectionFactory))
    {
    }

    public virtual IEnumerable<T> FindAll<T>(ISelectQuery<T> query)
    {
        return QueryExecutor.ExecuteQuery<IEnumerable<T>, T>(query, dbCommand =>
        {
            using var reader = dbCommand.ExecuteReader();
            return query.ReadAll(reader);
        });
    }

    public virtual async Task<IEnumerable<T>> FindAllAsync<T>(ISelectQuery<T> query)
    {
        return await QueryExecutor.ExecuteQueryAsync<IEnumerable<T>, T>(query, async dbCommand =>
        {
            await using var reader = await dbCommand.ExecuteReaderAsync();
            return await query.ReadAllAsync(reader);
        });
    }

    public virtual T FindOne<T>(ISelectQuery<T> query)
    {
        return QueryExecutor.ExecuteQuery<T, T>(query, dbCommand =>
        {
            using var reader = dbCommand.ExecuteReader();
            return query.ReadOne(reader);
        });
    }

    public virtual async Task<T> FindOneAsync<T>(ISelectQuery<T> query)
    {
        return await QueryExecutor.ExecuteQueryAsync(query, async dbCommand =>
        {
            await using var reader = await dbCommand.ExecuteReaderAsync();
            return await query.ReadOneAsync(reader);
        });
    }

    public virtual int Modify<T>(IModifyQuery<T> query)
    {
        return QueryExecutor.ExecuteQuery(query, dbCommand => dbCommand.ExecuteNonQuery());
    }

    public virtual async Task<int> ModifyAsync<T>(IModifyQuery<T> query)
    {
        return await QueryExecutor.ExecuteQueryAsync(query, async dbCommand => await dbCommand.ExecuteNonQueryAsync());
    }

    public virtual TR EvaluateScalar<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate)
    {
        var obj = QueryExecutor.ExecuteQuery(query, dbCommand => dbCommand.ExecuteScalar());
        return predicate(obj!);
    }

    public virtual async Task<TR> EvaluateScalarAsync<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate)
    {
        var obj = await QueryExecutor.ExecuteQueryAsync(query, async dbCommand => await dbCommand.ExecuteScalarAsync());
        return predicate(obj!);
    }
}