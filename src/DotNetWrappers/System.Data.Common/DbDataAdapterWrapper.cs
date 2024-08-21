using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public class DbDataAdapterWrapper(DbDataAdapter dbDataAdapter) : IDbDataAdapterWrapper
{
    public DbDataAdapter DbDataAdapter { get; } = dbDataAdapter ?? throw new ArgumentNullException(nameof(dbDataAdapter));

    public IDbCommandWrapper DeleteCommand
    {
        get => new DbCommandWrapper(DbDataAdapter.DeleteCommand!);
        set => DbDataAdapter.DeleteCommand = value.DbCommand;
    }

    public IDbCommandWrapper InsertCommand
    {
        get => new DbCommandWrapper(DbDataAdapter.InsertCommand!);
        set => DbDataAdapter.InsertCommand = value.DbCommand;
    }

    public IDbCommandWrapper SelectCommand
    {
        
        get => new DbCommandWrapper(DbDataAdapter.SelectCommand!);
        set => DbDataAdapter.SelectCommand = value.DbCommand;
    }

    public IDbCommandWrapper UpdateCommand
    {
        get => new DbCommandWrapper(DbDataAdapter.UpdateCommand!);
        set => DbDataAdapter.UpdateCommand = value.DbCommand;
    }

    public int Fill(DataSet dataSet) => DbDataAdapter.Fill(dataSet);
    public virtual int Fill(DataSet dataSet, string srcTable) => DbDataAdapter.Fill(dataSet, srcTable);

    public virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable) => 
        DbDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);

    public virtual int Fill(DataTable dataTable) => DbDataAdapter.Fill(dataTable);

    public virtual int Fill(int startRecord, int maxRecords, params DataTable[] dataTables) => 
        DbDataAdapter.Fill(startRecord, maxRecords, dataTables);

    public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => DbDataAdapter.FillSchema(dataSet, schemaType);
    public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable) => 
        DbDataAdapter.FillSchema(dataSet, schemaType, srcTable);

    public IDataParameter[] GetFillParameters() => DbDataAdapter.GetFillParameters();

    public int Update(DataSet dataSet) => DbDataAdapter.Update(dataSet);
    public virtual int Update(DataRow[] dataRows) => DbDataAdapter.Update(dataRows);
    public virtual int Update(DataTable dataTable) => DbDataAdapter.Update(dataTable);
    public virtual int Update(DataSet dataSet, string srcTable) => DbDataAdapter.Update(dataSet, srcTable);
}