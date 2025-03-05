namespace OresundbronLibrary.Vehicle_classes;

/// <inheritdoc/>
public class Car : Bridge.Car
{
    /// <inheritdoc/>
    public Car(string licensePlate, DateTime date) : base(licensePlate, date) { }

    /// <inheritdoc/>
    public override double Price()
    {
        return 460;
    }

    /// <inheritdoc/>
    public override string VehicleType()
    {
        return "Oresund car";
    }
}
