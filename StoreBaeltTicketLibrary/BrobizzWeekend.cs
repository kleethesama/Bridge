using Bridge;
using Bridge.Price_classes;
using StoreBaeltTicketLibrary.Utility_classes;
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
        if (!BrobizzUtility.CheckIfVehicleIsAllowedType(Vehicle, typeof(Car)))
        {
            throw new VehicleIsNotCarException("The Brobizz weekend discount only applies to objects of the type Car.");
        }
        if (!BrobizzUtility.CheckIfDateIsWeekend(Vehicle))
        {
            throw new DateIsNotWeekendException("The Brobizz weekend discount only applies on a weekend.");
        }
    }

    /// <inheritdoc/>
    public override double GetNewDiscountPrice()
    {
        return Vehicle.Price() * (1.0 - 0.15); // 15% price discount.
    }
}
