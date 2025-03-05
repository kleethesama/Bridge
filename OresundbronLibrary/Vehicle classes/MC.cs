namespace OresundbronLibrary.Vehicle_classes;

/// <inheritdoc/>
public class MC : Bridge.MC
{
    /// <inheritdoc/>
    public MC(string licensePlate, DateTime date) : base(licensePlate, date) { }

    /// <inheritdoc/>
    public override double Price()
    {
        return 235;
    }

    /// <inheritdoc/>
    public override string VehicleType()
    {
        return "Oresund MC";
    }
}
