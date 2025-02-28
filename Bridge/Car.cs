namespace Bridge;

/// <summary>
/// A Car class for storing information about a car, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public class Car
{
    /// <value>
    /// The <c>LicensePlate</c> property represents a 
    /// car's license plate for this instance.
    /// </value>
    public string LicensePlate { get; set; }

    /// <value>
    /// The <c>Date</c> property represents a 
    /// car's allowed crossing date for this instance.
    /// </value>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets the car's crossing price and returns it.
    /// </summary>
    public double Price() => 230;

    /// <summary>
    /// Gets the car's vehicle type and returns it.
    /// </summary>
    public string VehicleType() => "Car";
}
