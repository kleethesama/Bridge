namespace StoreBaeltTicketLibrary.Exception_classes;

public class VehicleIsNotCarException : Exception
{
    public VehicleIsNotCarException() : base() { }

    public VehicleIsNotCarException(string message) : base(message) { }

    public VehicleIsNotCarException(string message, Exception inner) : base(message, inner) { }
}
