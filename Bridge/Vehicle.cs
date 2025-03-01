namespace Bridge;

/// <summary>
/// A class Vehicle for storing information about vehicles, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public abstract class Vehicle
{
    /// <value>
    /// The <c>LicensePlate</c> property represents a 
    /// vehicles's license plate for this instance.
    /// </value>
    protected string LicensePlate { get; set; }

    /// <value>
    /// The <c>Date</c> property represents a 
    /// vehicles's allowed crossing date for this instance.
    /// </value>
    protected DateTime Date { get; set; }

    public Vehicle(string licensePlate, DateTime date)
    {
        LicensePlate = licensePlate;
        Date = date;
    }

    /// <summary>
    /// Gets the vehicles's crossing price and returns it.
    /// </summary>
    public abstract double Price();

    /// <summary>
    /// Gets the vehicles's vehicle type and returns it.
    /// </summary>
    public abstract string VehicleType();
}
