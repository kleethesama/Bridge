using Bridge;
using Bridge.Price_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace StoreBaeltTicketLibrary;

/// <summary>
/// A class for the Brobizz weekend price discount. 
/// Applies only to cars
/// </summary>
public class BrobizzWeekend : Brobizz
{
    /// <summary>
    /// Initializes an instance of a <c>BrobizzWeekend</c>
    /// with a <c>Vehicle</c> property.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <exception cref="VehicleIsNotCarException"></exception>
    /// <exception cref="DateIsNotWeekendException"></exception>
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

    /// <summary>
    /// A method used to check if this instance's date is a weekend day or not.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the <c>Vehicle</c> property's 
    /// <c>Date</c> property is a Saturday or Sunday, 
    /// otherwise <see langword="false"/>.
    /// </returns>
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
