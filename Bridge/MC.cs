namespace Bridge;

/// <summary>
/// An MC class for storing information about a motorcycle, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public class MC : Vehicle
{
    /// <summary>
    /// Initializes an instance of an MC with properties 
    /// like LicensePlate and Date.
    /// </summary>
    public MC(string licensePlate, DateTime date) : base(licensePlate, date)
    {
        LicensePlate = licensePlate;
        Date = date;
    }

    /// <summary>
    /// Gets the motorcycle's crossing price and returns it.
    /// </summary>
    public override double Price() => 120;

    /// <summary>
    /// Gets the motorcycle's vehicle type and returns it.
    /// </summary>
    public override string VehicleType() => "MC";
}
