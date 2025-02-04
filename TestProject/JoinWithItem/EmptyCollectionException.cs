namespace TestProject.JoinWithItem;

public class EmptyCollectionException : Exception
{
    public EmptyCollectionException (string message) : base (message)
        { }
    public EmptyCollectionException(string message, Exception innerException) : base (message, innerException) { }
    public EmptyCollectionException(): base () { }
}
