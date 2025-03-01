namespace Bridge;

/// <summary>
/// A Car class for storing information about a car, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public class Car : Vehicle
{
    public Car(string licensePlate, DateTime date) : base(licensePlate, date)
    {
        LicensePlate = licensePlate;
        Date = date;
    }

    /// <summary>
    /// Gets the car's crossing price and returns it.
    /// </summary>
    public override double Price() => 230;

    /// <summary>
    /// Gets the car's vehicle type and returns it.
    /// </summary>
    public override string VehicleType() => "Car";
}