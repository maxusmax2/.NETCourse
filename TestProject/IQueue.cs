namespace TestProject;

public interface IQueue<T>: ICollection<T>
{
    T Dequeue();
    void Enqueue(T item);
}
