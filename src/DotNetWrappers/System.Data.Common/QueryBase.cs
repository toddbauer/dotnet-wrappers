using System.Data;

namespace DotNetWrappers.System.Data.Common;

public abstract class QueryBase<T> : IQuery<T>
{
    public abstract CommandType CommandType { get; }
    public abstract string ConnectionString { get; }
    public abstract string Sql { get; }
    public abstract string Where { get; }
    public abstract string OrderBy { get; }
    public virtual int? CommandTimeout => null;
    public virtual bool? BindByName => true;
    public virtual int? ArrayBindCount => 0;

    public virtual string CreateCommandText()
    {
        return $"{Sql} {Where} {OrderBy}".Trim();
    }

    public abstract IEnumerable<IDbParameterWrapper> Parameters(Func<IDbParameterWrapper> parameterFactory);

    public virtual IDbParameterWrapper CreateParameter(Func<IDbParameterWrapper> parameterFactory, string name, object value, DbType? dbType)
    {
        var dbDataParameter = parameterFactory();

        dbDataParameter.ParameterName = name;
        dbDataParameter.Value = value;

        if (dbType != null)
            dbDataParameter.DbType = dbType.Value;

        return dbDataParameter;
    }
}