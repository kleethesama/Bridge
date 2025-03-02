namespace Bridge.Price_classes;

public class Brobizz : VehiclePriceModifier
{
    public Brobizz(Vehicle vehicle) : base(vehicle) { }

    public override double GetNewDiscountPrice()
    {
        return Vehicle.Price() * 0.9; // Subtracts 10% from the total price.
    }
}
