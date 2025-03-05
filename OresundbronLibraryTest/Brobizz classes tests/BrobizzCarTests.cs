using OresundbronLibrary.Price_classes;
using OresundbronLibrary.Vehicle_classes;
using StoreBaeltTicketLibrary.Exception_classes;

namespace OresundbronLibraryTest;

[TestClass]
public class BrobizzCarTests
{
    public string TestLicensePlate = "HF38663";
    public DateTime TestDate = DateTime.Now;

    [TestMethod]
    public void VehicleIsNotCarType_ExceptionThrown()
    {
        bool exceptionCaught = false;
        var mc = new MC(TestLicensePlate, TestDate);

        try
        {
            var brobizzCar = new BrobizzCar(mc);
        }
        catch (VehicleIsNotCarException)
        {
            exceptionCaught = true;
        }

        Assert.IsTrue(exceptionCaught,
            "The exception for the vehicle not being a car was not thrown. " +
            $"The vehicle is of the type {mc.GetType()}");
    }

    [TestMethod]
    public void CarWithNoBrobizz_DiscountMustNotBeApplied()
    {
        var car = new Car(TestLicensePlate, TestDate);

        Assert.AreEqual(460, car.Price(), 1e-6,
            $"Car without discount price does not match the actual price listed. It should be {460}.");
    }

    [TestMethod]
    public void CarWithBrobizz_DiscountMustBeApplied()
    {
        var car = new Car(TestLicensePlate, TestDate);
        var brobizz = new BrobizzCar(car);

        Assert.AreEqual(178, brobizz.GetNewDiscountPrice(), 1e-6,
            $"Car discount price does not match the actual price listed. It should be {178}.");
    }
}
