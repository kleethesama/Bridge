using Bridge;
using StoreBaeltTicketLibrary.Utility_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace OresundbronLibrary.Price_classes;

public class BrobizzMC : Bridge.Price_classes.Brobizz
{
    public BrobizzMC(Vehicle vehicle) : base(vehicle)
    {
        if (!BrobizzUtility.CheckIfVehicleIsAllowedType(Vehicle, typeof(Vehicle_classes.MC)))
        {
            throw new VehicleIsNotCarException("Vehicle argument must be of the type MC.");
        }
    }

    /// <inheritdoc/>
    public override double GetNewDiscountPrice()
    {
        return 92;
    }
}
