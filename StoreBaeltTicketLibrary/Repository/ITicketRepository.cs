using Bridge;

namespace StoreBaeltTicketLibrary.Repository;

public interface ITicketRepository
{
    public void Add(Vehicle vehicle);
    public IEnumerable<Vehicle> Get(string licensePlate);
    public IEnumerable<Vehicle> GetAll();
}
