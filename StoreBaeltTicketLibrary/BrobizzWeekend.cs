using Bridge;
using Bridge.Price_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace StoreBaeltTicketLibrary;

public class BrobizzWeekend : Brobizz
{
    public BrobizzWeekend(Vehicle vehicle) : base(vehicle)
    {
        if (Vehicle.GetType() != typeof(Car))
        {
            throw new VehicleIsNotCarException("The Brobizz weekend discount only applies to objects of the type Car.");
        }
        if (!CheckIfDateIsWeekend())
        {
            throw new DateIsNotWeekendException("The Brobizz weekend discount only applies on a weekend.");
        }
    }

    private bool CheckIfDateIsWeekend()
    {
        return Vehicle.Date.DayOfWeek == DayOfWeek.Saturday
            || Vehicle.Date.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <inheritdoc/>
    public override double GetNewDiscountPrice()
    {
        return Vehicle.Price() * (1.0 - 0.15); // 15% price discount.
    }
}
