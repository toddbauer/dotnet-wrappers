namespace DotNetWrappers.System.Data.Common;

public interface IQueryExecutor
{
    TR ExecuteQuery<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, TR> func);
    Task<TR> ExecuteQueryAsync<TR, T>(IQuery<T> query, Func<IDbCommandWrapper, Task<TR>> func);
    void BindParameters(IEnumerable<IDbParameterWrapper> dbParameters, IDbCommandWrapper dbCommand);
}