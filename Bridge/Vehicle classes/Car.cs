namespace Bridge;

/// <summary>
/// A <c>Car</c> class for storing information about a car, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public class Car : Vehicle
{
    /// <summary>
    /// Initializes an instance of a <c>Car</c> object with properties 
    /// like LicensePlate and Date.
    /// </summary>
    public Car(string licensePlate, DateTime date) : base(licensePlate, date) { }

    /// <inheritdoc/>
    public override double Price() => 230;
    /// <inheritdoc/>
    public override string VehicleType() => "Car";
}