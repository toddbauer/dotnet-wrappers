namespace DotNetWrappers.System.Data.Common;

public interface IModifyQuery<T> : IQuery<T>
{
    T Entity { get; set; }
}