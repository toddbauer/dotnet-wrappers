using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

public class QueryExecutor(Func<IDbConnectionWrapper> dbConnectionWrapperFactory) : IQueryExecutor
{
    protected internal Func<IDbConnectionWrapper> DbConnectionWrapperFactory { get; } = dbConnectionWrapperFactory;

    [ExcludeFromCodeCoverage]
    public QueryExecutor(Func<DbConnection> dbConnectionFactory) : this(() => new DbConnectionWrapper(dbConnectionFactory()))
    {
    }

    public virtual TR ExecuteQuery<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, TR> func)
    {
        using var dbConnectionWrapper = DbConnectionWrapperFactory();
        dbConnectionWrapper.Open();
        using var dbCommandWrapper = dbConnectionWrapper.CreateCommand();
        dbCommandWrapper.CommandTimeout = query.CommandTimeout ?? dbCommandWrapper.CommandTimeout;
        if (query.BindByName == true)
            dbCommandWrapper.BindByName = true;
        dbCommandWrapper.CommandText = query.CreateCommandText();
        dbCommandWrapper.CommandType = query.CommandType;
        if (query.ArrayBindCount != null)
            dbCommandWrapper.ArrayBindCount = query.ArrayBindCount.Value;

        // ReSharper disable once AccessToDisposedClosure
        BindParameters(query.Parameters(() => dbCommandWrapper.CreateParameter()), dbCommandWrapper);

        return func(dbCommandWrapper);
    }

    public virtual async Task<TR> ExecuteQueryAsync<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, Task<TR>> funcAsync)
    {
        await using var dbConnectionWrapper = DbConnectionWrapperFactory();
        await dbConnectionWrapper.OpenAsync();
        await using var dbCommandWrapper = dbConnectionWrapper.CreateCommand();
        dbCommandWrapper.CommandTimeout = query.CommandTimeout ?? dbCommandWrapper.CommandTimeout;
        if (query.BindByName == true)
            dbCommandWrapper.BindByName = true;
        dbCommandWrapper.CommandText = query.CreateCommandText();
        dbCommandWrapper.CommandType = query.CommandType;
        if (query.ArrayBindCount != null)
            dbCommandWrapper.ArrayBindCount = query.ArrayBindCount.Value;

        // ReSharper disable once AccessToDisposedClosure
        BindParameters(query.Parameters(() => dbCommandWrapper.CreateParameter()), dbCommandWrapper);

        return await funcAsync(dbCommandWrapper);
    }

    public virtual void BindParameters(IEnumerable<IDbParameterWrapper> dbDataParameters, IDbCommandWrapper dbCommand)
    {
        if (dbDataParameters == null!)
            return;

        foreach(var parameter in dbDataParameters)
            dbCommand.Parameters.Add(parameter);
    }
}