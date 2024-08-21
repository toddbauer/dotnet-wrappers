using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public interface IDbParameterWrapper : IDbDataParameter
{
    DbParameter DbParameter { get; }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    void ResetDbType();

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    bool SourceColumnNullMapping { get; set; }
}