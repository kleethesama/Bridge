namespace Bridge;

/// <summary>
/// An MC class for storing information about a motorcycle, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public class MC : Vehicle
{
    /// <summary>
    /// Initializes an instance of an <c>MC</c> object with properties 
    /// like LicensePlate and Date.
    /// </summary>
    public MC(string licensePlate, DateTime date) : base(licensePlate, date) { }

    /// <inheritdoc/>
    public override double Price() => 120;
    /// <inheritdoc/>
    public override string VehicleType() => "MC";
}
