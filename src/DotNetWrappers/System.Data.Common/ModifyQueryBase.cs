
namespace DotNetWrappers.System.Data.Common;

public abstract class ModifyQueryBase<T> : QueryBase<T>, IModifyQuery<T>
{
    public T Entity { get; set; } = default!;
}