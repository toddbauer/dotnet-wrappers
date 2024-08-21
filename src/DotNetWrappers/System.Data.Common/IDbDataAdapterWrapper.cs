using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DotNetWrappers.System.Data.Common;

public interface IDbDataAdapterWrapper
{
    DbDataAdapter DbDataAdapter { get; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbCommandWrapper DeleteCommand { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbCommandWrapper InsertCommand { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbCommandWrapper SelectCommand { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    IDbCommandWrapper UpdateCommand { get; set; }


    int Fill(DataSet dataSet);
    int Fill(DataSet dataSet, string srcTable);
    int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);
    int Fill(DataTable dataTable);
    int Fill(int startRecord, int maxRecords, params DataTable[] dataTables);

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable);


    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IDataParameter[] GetFillParameters();

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    int Update(DataSet dataSet);

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    int Update(DataRow[] dataRows);

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    int Update(DataTable dataTable);

    [RequiresUnreferencedCode("IDataReader's (built from adapter commands) schema table types cannot be statically analyzed.")]
    int Update(DataSet dataSet, string srcTable);
}