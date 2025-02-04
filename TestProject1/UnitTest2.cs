using TestProject.JoinWithItem;

namespace TestProject1;

public class UnitTest2
{
    [Fact]
    public void LeftJoin_EmptyCollection1_ThrowsException()
    {
        var collection1 = new Item1[]
        {
        };

        var collection2 = new Item2[]
        {
            new Item2{ }
        };
        Assert.Throws<EmptyCollectionException>(() => Extensions.LeftJoin(collection1, collection2));
    }

    [Fact]
    public void LeftJoin_EmptyCollection2_ThrowsException()
    {
        var collection1 = new Item1[]
        {
            new Item1{ }
        };

        var collection2 = new Item2[]
        {

        };
        Assert.Throws<EmptyCollectionException>(() => Extensions.LeftJoin(collection1, collection2));
    }

    [Fact]
    public void LeftJoin_CorrectJoin()
    {
        var collection1 = new Item1[]
        {
            new Item1
            {
                Id = 4,
                Value = "4"
            },

            new Item1
            {
                Id = 1,
                Value = "1"
            },
            new Item1
            {
                Id = 2,
                Value = "2"
            },
            new Item1
            {
                Id = 3,
                Value = "3"
            },
            new Item1
            {
                Id = 8,
                Value = "8"
            }
        };

        var collection2 = new Item2[]
        {
            new Item2
            {
                Id = 2,
                Description = "2"
            },
            new Item2
            {
                Id = 8,
                Description = "8"
            },
             new Item2
            {
                Id = 1,
                Description = "1"
            },
            new Item2
            {
                Id = 4,
                Description = "4"
            }
        };
        var joined = Extensions.LeftJoin(collection1, collection2);
        Assert.Equal("1", collection1.First(x => x.Id == 1).Description);
        Assert.Equal("2", collection1.First(x => x.Id == 2).Description);
        Assert.Null(collection1.First(x => x.Id == 3).Description);
        Assert.Equal("4", collection1.First(x => x.Id == 4).Description);
        Assert.Equal("8", collection1.First(x => x.Id == 8).Description);
    }
    
}
