namespace Bridge;

/// <summary>
/// A base class for price modifications to vehicles
/// that have a change in price, for example, 
/// a Brobizz discount.
/// </summary>
public abstract class VehiclePriceModifier
{
    /// <value>
    /// The <c>Vehicle</c> property references the 
    /// vehicle's <c>Price</c> that is required for applying
    /// price modification for this instance.
    /// </value>
    public Vehicle Vehicle { get; protected set; }

    /// <summary>
    /// Initializes an instance of a <c>VehiclePriceModifier</c>
    /// with a Vehicle property. 
    /// This base class can not be instantiated and 
    /// is only meant to be inherited.
    /// </summary>
    public VehiclePriceModifier(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    /// <summary>
    /// Gets the new discount price based on
    /// the <c>Vehicle</c> property's <c>Price</c>.
    /// </summary>
    public abstract double GetNewDiscountPrice();
}
