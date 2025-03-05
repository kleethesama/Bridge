using Bridge;
using StoreBaeltTicketLibrary.Utility_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace OresundbronLibrary.Price_classes;

/// <summary>
/// A class for the Brobizz price discount for 
/// vehicles of the <c>Car</c> type.
/// </summary>
public class BrobizzCar : Bridge.Price_classes.Brobizz
{
    /// <summary>
    /// Initializes an instance of a <c>BrobizzCar</c> 
    /// with a <c>Vehicle</c> property.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <exception cref="VehicleIsNotCarException"></exception>
    public BrobizzCar(Vehicle vehicle) : base(vehicle)
    {
        if (!BrobizzUtility.CheckIfVehicleIsAllowedType(Vehicle, typeof(Vehicle_classes.Car)))
        {
            throw new VehicleIsNotCarException("Vehicle argument must be of the type Car.");
        }
    }

    /// <inheritdoc/>
    public override double GetNewDiscountPrice()
    {
        return 178;
    }
}
