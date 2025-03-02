namespace Bridge;

public abstract class VehiclePriceModifier
{
    public Vehicle Vehicle { get; protected set; }

    public VehiclePriceModifier(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public abstract double GetNewDiscountPrice();
}
