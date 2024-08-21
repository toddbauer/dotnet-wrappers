using System.Collections;
using System.Data;
using System.Data.Common;

namespace DotNetWrappers.System.Data.Common;

public class DbParameterCollectionWrapper(DbParameterCollection dbParameterCollection) : IDbParameterCollectionWrapper
{
    public virtual DbParameterCollection DbParameterCollection { get; } = dbParameterCollection ?? throw new ArgumentNullException(nameof(dbParameterCollection));

    public virtual void AddRange(IEnumerable<IDbParameterWrapper> values) => DbParameterCollection.AddRange(values.Select(x => x.DbParameter).ToArray());
    public virtual int Add(IDbParameterWrapper dbParameterWrapper) => DbParameterCollection.Add(dbParameterWrapper.DbParameter);
    public virtual IEnumerator GetEnumerator() => DbParameterCollection.GetEnumerator();
    public virtual void CopyTo(Array array, int index) => DbParameterCollection.CopyTo(array, index);
    public virtual int Count => DbParameterCollection.Count;
    public virtual bool IsSynchronized => DbParameterCollection.IsSynchronized;
    public virtual object SyncRoot => DbParameterCollection.SyncRoot;
    public virtual int Add(object? value) => DbParameterCollection.Add(value!);
    public virtual void Clear() => DbParameterCollection.Clear();
    public virtual bool Contains(object? value) => DbParameterCollection.Contains(value!);
    public virtual int IndexOf(object? value) => DbParameterCollection.IndexOf(value!);
    public virtual void Insert(int index, object? value) => DbParameterCollection.Insert(index, value!);
    public virtual void Remove(object? value) => DbParameterCollection.Remove(value!);
    public virtual void RemoveAt(int index) => DbParameterCollection.RemoveAt(index);
    public virtual bool IsFixedSize => DbParameterCollection.IsFixedSize;
    public virtual bool IsReadOnly => DbParameterCollection.IsReadOnly;

    public virtual IDbParameterWrapper this[int index]
    {
        get => new DbParameterWrapper(DbParameterCollection[index]);
        set => DbParameterCollection[index] = value.DbParameter;
    }

    object? IList.this[int index]
    {
        get => DbParameterCollection[index].Value;
        set => DbParameterCollection[index].Value = value;
    }

    public virtual bool Contains(string parameterName) => DbParameterCollection.Contains(parameterName);
    public virtual int IndexOf(string parameterName) => DbParameterCollection.IndexOf(parameterName);
    public virtual void RemoveAt(string parameterName) => DbParameterCollection.RemoveAt(parameterName);
    public virtual IDbParameterWrapper this[string parameterName]
    {
        get => new DbParameterWrapper(DbParameterCollection[parameterName]);
        set => DbParameterCollection[parameterName] = value.DbParameter;
    }

    public virtual void AddRange(Array values) => DbParameterCollection.AddRange(values);

    object IDataParameterCollection.this[string parameterName]
    {
        get => DbParameterCollection[parameterName].Value!;
        set => DbParameterCollection[parameterName].Value = value;
    }
}