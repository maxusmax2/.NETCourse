List<Person>persons = new List<Person>
{
        new Person { Name = "Maksim", City = "Vladimir" },
        new Person { Name = "Nikita", City = "Moscow" },
        new Person { Name = "Nasty", City = "Vladimir" },
        new Person { Name = "Won", City = "Seul" }
    };
List<Weather>weathers = new List<Weather>
    {
        new Weather { Now = "Solar", City = "Moscow" },
        new Weather { Now = "Rainy", City = "Tallin" },
        new Weather { Now = "Cold", City = "Vladimir" },
    };

var personWithWeatherInner = from person in persons
                             join weather in weathers on person.City equals weather.City
                             select new { Name = person.Name, City = person.City, Now = weather.Now };


foreach (var info in personWithWeatherInner) 
{
    Console.WriteLine(info);
}

var personWithWeatherLeft = from person in persons
                             join weather in weathers on person.City equals weather.City into weatherList
                             from weather in weatherList.DefaultIfEmpty()
                             select new { Name = person.Name, City = person.City, Now = weather?.Now };


foreach (var info in personWithWeatherLeft)
{
    Console.WriteLine(info);
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