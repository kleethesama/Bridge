namespace StoreBaeltTicketLibrary.Exception_classes;

/// <summary>
/// The exception that is thrown when a <c>Vehicle</c> 
/// object is not a <c>Car</c> object.
/// </summary>
public class VehicleIsNotCarException : Exception
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public VehicleIsNotCarException() : base() { }

    public VehicleIsNotCarException(string message) : base(message) { }

    public VehicleIsNotCarException(string message, Exception inner) : base(message, inner) { }
}
