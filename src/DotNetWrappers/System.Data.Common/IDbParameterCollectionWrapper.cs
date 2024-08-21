using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public interface IDbParameterCollectionWrapper : IDataParameterCollection
{
    DbParameterCollection DbParameterCollection { get; }

    new IDbParameterWrapper this[int index] { get; set; }
    new IDbParameterWrapper this[string parameterName] { get; set; }
    void AddRange(Array values);
    int Add(IDbParameterWrapper dbParameterWrapper);
    void AddRange(IEnumerable<IDbParameterWrapper> values);
}