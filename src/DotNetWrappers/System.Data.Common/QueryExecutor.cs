using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public class QueryExecutor(Func<IDbConnectionWrapper> dbConnectionFactory) : IQueryExecutor
{
    public QueryExecutor(Func<DbConnection> dbConnectionFactory) : this(() => new DbConnectionWrapper(dbConnectionFactory()))
    {
    }

    public virtual TR ExecuteQuery<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, TR> func)
    {
        using var dbConnection = dbConnectionFactory();
        dbConnection.Open();
        using var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandTimeout = query.CommandTimeout ?? dbCommand.CommandTimeout;
        if (query.BindByName == true)
            dbCommand.BindByName = true;
        dbCommand.CommandText = query.CreateCommandText();
        dbCommand.CommandType = query.CommandType;
        if (query.ArrayBindCount != null)
            dbCommand.ArrayBindCount = query.ArrayBindCount.Value;

        BindParameters(query.Parameters(() => dbCommand.CreateParameter()), dbCommand);

        return func(dbCommand);
    }

    public virtual async Task<TR> ExecuteQueryAsync<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, Task<TR>> func)
    {
        using var dbConnection = dbConnectionFactory();
        await dbConnection.OpenAsync();
        using var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandTimeout = query.CommandTimeout ?? dbCommand.CommandTimeout;
        if (query.BindByName == true)
            dbCommand.BindByName = true;
        dbCommand.CommandText = query.CreateCommandText();
        dbCommand.CommandType = query.CommandType;
        if (query.ArrayBindCount != null)
            dbCommand.ArrayBindCount = query.ArrayBindCount.Value;

        BindParameters(query.Parameters(() => dbCommand.CreateParameter()), dbCommand);

        return func(dbCommand).Result;
    }

    public void BindParameters(IEnumerable<IDbParameterWrapper> dbDataParameters, IDbCommandWrapper dbCommand)
    {
        if (dbDataParameters == null!)
            return;

        foreach(var parameter in dbDataParameters)
            dbCommand.Parameters.Add(parameter);
    }
}