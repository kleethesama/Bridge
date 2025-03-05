using Bridge;
using StoreBaeltTicketLibrary.Utility_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace OresundbronLibrary.Price_classes;

public class BrobizzCar : Bridge.Price_classes.Brobizz
{
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
