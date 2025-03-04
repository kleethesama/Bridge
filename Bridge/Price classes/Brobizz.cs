namespace Bridge.Price_classes;

/// <summary>
/// A class for the Brobizz price discount.
/// </summary>
public class Brobizz : VehiclePriceModifier
{
    /// <summary>
    /// Initializes an instance of a <c>Brobizz</c>
    /// with a <c>Vehicle</c> property.
    /// </summary>
    public Brobizz(Vehicle vehicle) : base(vehicle) { }
    
    /// <inheritdoc/>
    public override double GetNewDiscountPrice()
    {
        return Vehicle.Price() * 0.9; // Subtracts 10% from the total price.
    }
}
