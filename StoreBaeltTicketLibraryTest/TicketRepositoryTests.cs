using Bridge;
using StoreBaeltTicketLibrary;
using StoreBaeltTicketLibrary.Repository;

namespace StoreBaeltTicketLibraryTest;

[TestClass]
public class TicketRepositoryTests
{
    public string TestLicensePlate = "HF38663";
    public DateTime TestDate = DateTime.Now;

    [TestMethod]
    public void AddVehicleToEmptyList()
    {
        var repo = new TicketRepository();
        var car = new Car(TestLicensePlate, TestDate);
        var brobizz = new BrobizzWeekend(car);

        repo.Add()
    }
}
