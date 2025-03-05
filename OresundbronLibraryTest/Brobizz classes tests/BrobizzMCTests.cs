using OresundbronLibrary.Price_classes;
using OresundbronLibrary.Vehicle_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace OresundbronLibraryTest;

[TestClass]
public class BrobizzMCTests
{
    public string TestLicensePlate = "HF38663";
    public DateTime TestDate = DateTime.Now;

    [TestMethod]
    public void VehicleIsNotMCType_ExceptionThrown()
    {
        bool exceptionCaught = false;
        var car = new Car(TestLicensePlate, TestDate);

        try
        {
            var brobizzCar = new BrobizzMC(car);
        }
        catch (VehicleIsNotCarException)
        {
            exceptionCaught = true;
        }

        Assert.IsTrue(exceptionCaught,
            "The exception for the vehicle not being a car was not thrown. " +
            $"The vehicle is of the type {car.GetType()}");
    }

    [TestMethod]
    public void MCWithNoBrobizz_DiscountMustNotBeApplied()
    {
        var mc = new MC(TestLicensePlate, TestDate);

        Assert.AreEqual(235, mc.Price(), 1e-6,
            $"MC without discount price does not match the actual price listed. It should be {235}.");
    }

    [TestMethod]
    public void MCWithBrobizz_DiscountMustBeApplied()
    {
        var mc = new MC(TestLicensePlate, TestDate);
        var brobizz = new BrobizzMC(mc);

        Assert.AreEqual(92, brobizz.GetNewDiscountPrice(), 1e-6,
            $"MC discount price does not match the actual price listed. It should be {92}.");
    }
}
