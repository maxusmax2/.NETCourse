namespace TestProject.JoinWithItem;

public static class Extensions
{
    public static IEnumerable<Item1> LeftJoin(IEnumerable<Item1> items1, IEnumerable<Item2> items2)
    {
        if (!items1.Any() || !items2.Any())
            throw new EmptyCollectionException();
        return items1.MyLeftJoin(items2, x => x.Id, y => y.Id, (x, y,id) => new Item1() { Id=x.Id,Description= x?.Description ?? y?.Description});
    }
}
