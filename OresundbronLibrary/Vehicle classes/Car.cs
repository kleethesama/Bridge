namespace OresundbronLibrary.Vehicle_classes;

public class Car : Bridge.Car
{
    public Car(string licensePlate, DateTime date) : base(licensePlate, date) { }

    public override double Price()
    {
        return 460;
    }

    public override string VehicleType()
    {
        return "Oresund car";
    }
}
