namespace DotNetWrappers.System.Data.Common;

public interface IDbContext
{
    IEnumerable<T> FindAll<T>(ISelectQuery<T> query);
    Task<IEnumerable<T>> FindAllAsync<T>(ISelectQuery<T> query);

    T FindOne<T>(ISelectQuery<T> query);
    Task<T> FindOneAsync<T>(ISelectQuery<T> query);

    int Modify<T>(IModifyQuery<T> query);
    Task<int> ModifyAsync<T>(IModifyQuery<T> query);

    TR EvaluateScalar<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate);
    Task<TR> EvaluateScalarAsync<T, TR>(ISelectQuery<T> query, Func<object, TR> predicate);
}