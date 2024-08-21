using System.Data;

namespace DotNetWrappers.System.Data.Common;

public interface IQuery<T>
{
    CommandType CommandType { get; }
    string ConnectionString { get; }
    string Sql { get; }
    string Where { get; }
    string OrderBy { get; }
    int? CommandTimeout { get; }
    bool? BindByName { get; }
    int? ArrayBindCount { get; }

    string CreateCommandText();

    IEnumerable<IDbParameterWrapper> Parameters(Func<IDbParameterWrapper> parameterFactory);
    IDbParameterWrapper CreateParameter(Func<IDbParameterWrapper> parameterFactory, string name, object value, DbType? dbType = null);
}