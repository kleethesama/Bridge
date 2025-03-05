using Bridge;

namespace StoreBaeltTicketLibrary.Utility_classes;

/// <summary>
/// A helper class that contains useful methods 
/// for checking certain conditions that helps 
/// constrain object initialization.
/// </summary>
public static class BrobizzUtility
{
    /// <summary>
    /// Used to check if a <c>Vehicle</c> object is of a certain type, 
    /// like a <c>Car</c> or <c>MC</c>.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="allowedVehicleType"></param>
    /// <returns>
    /// <see langword="true"/> if the <c>vehicle</c> 
    /// argument is an <c>allowedVehicleType</c> type, 
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool CheckIfVehicleIsAllowedType(Vehicle vehicle, Type allowedVehicleType)
    {
        return vehicle.GetType() == allowedVehicleType;
    }

    /// <summary>
    /// Used to check if a <c>Vehicle</c> object's <c>Date</c> 
    /// property is a weekend day or not.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <returns>
    /// <see langword="true"/> if the <c>Vehicle</c> property's 
    /// <c>Date</c> property is a Saturday or Sunday, 
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool CheckIfDateIsWeekend(Vehicle vehicle)
    {
        return vehicle.Date.DayOfWeek == DayOfWeek.Saturday
            || vehicle.Date.DayOfWeek == DayOfWeek.Sunday;
    }
}
