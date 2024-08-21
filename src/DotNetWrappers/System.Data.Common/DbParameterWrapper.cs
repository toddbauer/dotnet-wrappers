using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

public class DbParameterWrapper(DbParameter dbParameter) : IDbParameterWrapper
{
    public DbParameter DbParameter { get; } = dbParameter;

    #region IDbDataParameter

    public DbType DbType
    {
        get => DbParameter.DbType;
        set => DbParameter.DbType = value;
    }

    public ParameterDirection Direction
    {
        get => DbParameter.Direction;
        set => DbParameter.Direction = value;
    }

    public bool IsNullable
    {
        get => DbParameter.IsNullable;
        set => DbParameter.IsNullable = value;
    }

    [AllowNull]
    [DefaultValue("")]
    public string ParameterName
    {
        get => DbParameter.ParameterName;
        set => DbParameter.ParameterName = value;
    }

    [AllowNull]
    [DefaultValue("")]
    public string SourceColumn
    {
        get => DbParameter.SourceColumn;
        set => DbParameter.SourceColumn = value;
    }

    public DataRowVersion SourceVersion
    {
        get => DbParameter.SourceVersion;
        set => DbParameter.SourceVersion = value;
    }

    [DefaultValue(null)]
    public object? Value
    {
        get => DbParameter.Value;
        set => DbParameter.Value = value;
    }

    public byte Precision
    {
        get => DbParameter.Precision;
        set => DbParameter.Precision = value;
    }

    public byte Scale
    {
        get => DbParameter.Scale;
        set => DbParameter.Scale = value;
    }

    public int Size
    {
        get => DbParameter.Size; 
        set => DbParameter.Size = value;
    }

    #endregion IDbDataParameter
    
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void ResetDbType()
    {
        DbParameter.ResetDbType();
    }

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public bool SourceColumnNullMapping
    {
        get => DbParameter.SourceColumnNullMapping;
        set => DbParameter.SourceColumnNullMapping = value;
    }
}