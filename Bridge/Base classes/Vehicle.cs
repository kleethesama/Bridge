namespace Bridge;

/// <summary>
/// A class <c>Vehicle</c> for storing information about vehicles, 
/// getting the price for crossing the bridge, and 
/// what kind of vehicle it is.
/// </summary>
public abstract class Vehicle
{
    /// <value>
    /// The <c>LicensePlate</c> property represents a 
    /// vehicles's license plate for this instance.
    /// </value>
    public string LicensePlate
    {
        get => LicensePlate;
        protected set
        {
            if (value.Length > 7)
            {
                throw new ArgumentException("License plate may not be more than 7 characters long.", nameof(LicensePlate));
            }
        }
    }

    /// <value>
    /// The <c>Date</c> property represents a 
    /// vehicles's allowed crossing date for this instance.
    /// </value>
    public DateTime Date { get; protected set; }

    /// <summary>
    /// Initializes an instance of a <c>Vehicle</c> with properties 
    /// like <c>LicensePlate</c> and <c>Date</c>.
    /// This base class can not be instantiated and 
    /// is only meant to be inherited.
    /// </summary>
    public Vehicle(string licensePlate, DateTime date)
    {
        LicensePlate = licensePlate;
        Date = date;
    }

    /// <summary>
    /// Gets the vehicles's crossing price and returns it.
    /// </summary>
    /// <returns>
    /// The price for the vehicle.
    /// </returns>
    public abstract double Price();

    /// <summary>
    /// Gets the vehicles's crossing price if there 
    /// is a price modifier like a Brobizz and returns it.
    /// </summary>
    /// <remarks>
    /// An overloaded helper function that calls the 
    /// <c>GetNewDiscountPrice</c> function directly by passing 
    /// a <c>VehiclePriceModifier</c> reference.
    /// </remarks>
    /// <returns>
    /// The discount price for the vehicle.
    /// </returns>
    /// <param name="vehiclePriceModifier">
    /// The <c>VehiclePriceModifier</c> object that references 
    /// the vehicle object and has the discount factor.
    /// </param>
    public static double Price(VehiclePriceModifier vehiclePriceModifier)
    {
        return vehiclePriceModifier.GetNewDiscountPrice();
    }

    /// <summary>
    /// Gets the vehicles's vehicle type and returns it.
    /// </summary>
    /// <returns>
    /// The string that represents the vehicle's type.
    /// </returns>
    public abstract string VehicleType();
}
