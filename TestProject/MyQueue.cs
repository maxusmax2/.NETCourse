using System.Collections;

namespace TestProject;

public class MyQueue<T> : IQueue<T>
{
    private LinkedList<T> values;
    public MyQueue() 
    {
        values = new ();
    }
    public int Count => values.Count;

    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(T item)
    {
        values.AddLast(item);
    }

    public void Clear()
    {
        values.Clear();
    }

    public bool Contains(T item)
    {
        return values.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        values.CopyTo(array, arrayIndex);
    }

    public T Dequeue()
    {
        if (values.First is null)
            throw new InvalidOperationException();
        var first = values.First.Value;
        values.RemoveFirst();
        return first;
    }

    public void Enqueue(T item)
    {
        values.AddLast(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return values.GetEnumerator();
    }

    public bool Remove(T item)
    {
        return values.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
