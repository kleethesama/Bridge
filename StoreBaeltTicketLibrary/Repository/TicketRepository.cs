using Bridge;

namespace StoreBaeltTicketLibrary.Repository;

public class TicketRepository : ITicketRepository
{
    public static List<Vehicle> Vehicles;

    public TicketRepository()
    {
        Vehicles = [];
    }

    public TicketRepository(List<Vehicle> vehicles)
    {
        Vehicles = vehicles;
    }

    public void Add(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
    }

    public IEnumerable<Vehicle>? Get(string licensePlate)
    {
        var vehicleSearch = Vehicles.FindAll(e => e.LicensePlate == licensePlate);
        if (vehicleSearch.Count > 0)
        {
            return vehicleSearch;
        }
        return null;
    }

    public IEnumerable<Vehicle>? GetAll()
    {
        if (Vehicles.Count > 0)
        {
            return Vehicles;
        }
        return null;
    }
}
