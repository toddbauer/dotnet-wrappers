using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public interface IDbDataParameterWrapper : IDbDataParameter
{
    DbParameter DbParameter { get; }
}