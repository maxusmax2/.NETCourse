using TestProject;

List<Person>persons = new List<Person>
{
        new Person { Name = "A", City = "C" },
        new Person { Name = "B", City = "B" },
        new Person { Name = "C", City = "A" },
         new Person { Name = "A", City = "A" },
        new Person { Name = "B", City = "B" },
        new Person { Name = "C", City = "C" },
    };
List<Weather>weathers = new List<Weather>
    {
        new Weather { Now = "Solar", City = "Moscow" },
        new Weather { Now = "Rainy", City = "Tallin" },
        new Weather { Now = "Cold", City = "Vladimir" },
    };

List<int> list = [1,2,3,4,2,3,4,5,6];
var dictionary = list.ToDictionary(x=> x,x=>x.ToString());
IEnumerable<KeyValuePair<int, string>> items = dictionary;
var seed = new Dictionary<int, string>();
var dictionary2 = items.Aggregate(seed,(x,y) => { x.TryAdd(y.Key, y.Value);return x; });
foreach (var j in persons.OrderByDescending(x=>x.Name).OrderBy(x=>x.City))
{
    Console.WriteLine($"{j.Name ?? "NULL"} | {j.City?? "NULL"}");
}
Console.ReadKey();

var leftJoin = persons.MyLeftJoin(weathers, x => x.City, y => y.City, (first, second, id) => new { id, first, second });
foreach (var j in leftJoin)
{
    Console.WriteLine($"{j.first?.Name ?? "NULL"} | {j.id} | {j.second?.Now ?? "NULL"}");
}
Console.ReadKey();


public static class Extension
{
 
    public static IEnumerable<TReturn> MyLeftJoin<T1, T2, TJoin, TReturn>(this IEnumerable<T1> t1, IEnumerable<T2> t2, Func<T1, TJoin> f1Join, Func<T2, TJoin> f2Join, Func<T1, T2, TJoin, TReturn> returnVal) =>
        new MyLeftJoinIterator<T1, T2, TJoin, TReturn>(t1, t2, f1Join, f2Join, returnVal);
    public static IEnumerable<TReturn> FullJoin<T1, T2, TJoin, TReturn>(this IEnumerable<T1>? t1, IEnumerable<T2>? t2, Func<T1, TJoin> f1Join, Func<T2, TJoin> f2Join, Func<T1?, T2?, TJoin, TReturn> returnVal)
    {
        var a = t1.ToLookup(x => x, y => t2.Where(x => f2Join(x).Equals(f1Join(y))));
        var b = t2.ToLookup(x => x, y => t1.Where(x => f1Join(x).Equals(f2Join(y))));

        List<TReturn> c = new();
        foreach (var list in a)
        {
            foreach (var items in list)
            {
                if (!items.Any())
                {
                    c.Add(returnVal(list.Key, default, f1Join(list.Key)));
                }
                else
                {
                    foreach (var item in items)
                    {
                        c.Add(returnVal(list.Key, item, f1Join(list.Key)));
                    }
                }
            }
        }

        foreach (var list in b)
        {
            foreach (var items in list)
            {
                if (!items.Any())
                {
                    c.Add(returnVal(default, list.Key, f2Join(list.Key)));
                }
                else
                {
                    foreach (var item in items)
                    {
                        c.Add(returnVal(item, list.Key, f2Join(list.Key)));
                    }
                }
            }
        }
        return c.ToHashSet();
    }
}

public class Person
{
    public string Name { get; set; }
    public string City { get; set; }
}

public class Weather
{
    public string Now { get; set; }
    public string City { get; set; }
}