namespace OresundbronLibrary.Vehicle_classes;

public class MC : Bridge.MC
{
    public MC(string licensePlate, DateTime date) : base(licensePlate, date) { }

    public override double Price()
    {
        return 235;
    }

    public override string VehicleType()
    {
        return "Oresund MC";
    }
}
