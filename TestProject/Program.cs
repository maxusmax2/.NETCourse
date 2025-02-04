
// Задание 1
object obj = new Car(new Engine(105));

var isCarWith105HP = obj switch
{
    105 => true,
    _ => false,
};

Console.WriteLine(isCarWith105HP);


// Задание 2
object obj2 = new Car(new Engine(105));

if (obj2 is Car res)
{
    Console.WriteLine(res.Engine.Power);
}

// Задание 3
object obj3 = new Car(new Engine(90));
object obj4 = new Motorcycle(new Engine(80));

static bool IsLowPowerVehicle(object vehicle, bool isElectro) => (vehicle,isElectro) switch
{
    (_, true) => false,
    (Motorcycle and { Engine.Power: < 100 },_)  => true,
    (Car and { Engine.Power: < 100 }, _) => true,
    (SmartCar smartCar,_) => smartCar.IsLowPowerVehicle,
    (null,_) => throw new ArgumentNullException(),
    _ => throw new ArgumentException(),
};

var isLowPower1 = IsLowPowerVehicle(obj3,true);
var isLowPower2 = IsLowPowerVehicle(obj4, false);
var isLowPower3 = IsLowPowerVehicle(42, false);


public record Engine(int Power);
public record Motorcycle(Engine Engine);
public record SmartCar(bool IsLowPowerVehicle);
public class Car
{
    public Engine Engine { get; }

    public Car(Engine engine)
    {
        Engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }
}


