using Bridge;
using Bridge.Price_classes;

namespace StoreBaeltTicketLibrary;

public class BrobizzWeekend : Brobizz
{
    public BrobizzWeekend(Vehicle vehicle) : base(vehicle)
    {
        if (vehicle.GetType() != typeof(Car))
        {
            throw new ArgumentException("The Brobizz weekend discount only applies to objects of the type Car.", nameof(vehicle));
        }
        if (!CheckIfDateIsWeekend())
        {
            throw new ArgumentException("The Brobizz weekend discount only applies on a weekend.", nameof(vehicle));
        }
    }

    private bool CheckIfDateIsWeekend()
    {
        return Vehicle.Date.DayOfWeek == DayOfWeek.Saturday
            || Vehicle.Date.DayOfWeek == DayOfWeek.Sunday;
    }

    public override double GetNewDiscountPrice()
    {
        return Vehicle.Price() * (1.0 - 0.15); // 15% price discount.
    }
}
