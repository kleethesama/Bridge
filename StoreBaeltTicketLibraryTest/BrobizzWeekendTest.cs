using Bridge;
using StoreBaeltTicketLibrary;
using StoreBaeltTicketLibrary.Exception_classes;

namespace StoreBaeltTicketLibraryTest;

[TestClass]
public sealed class BrobizzWeekendTest
{
    public string TestLicensePlate = "HF38663";

    [TestMethod]
    public void ApplyBrobizzWeekendDiscountToMotorcycleExceptionThrown()
    {
        bool exceptionCaught = false;
        DateTime date = DateTime.Parse("01-03-2025"); // Saturday
        var car = new MC(TestLicensePlate, date); // Instantiating motorcycle object.

        try
        {
            var brobizzWeekend = new BrobizzWeekend(car);
        }
        catch (VehicleIsNotCarException)
        {
            exceptionCaught = true;
        }

        Assert.IsTrue(exceptionCaught,
            "The exception for the vehicle not being on a car was not thrown.");
    }

    [TestMethod]
    public void ApplyBrobizzWeekendDiscountToCar_NotWeekendExceptionThrown()
    {
        bool exceptionCaught = false;
        DateTime date = DateTime.Parse("03-03-2025"); // Monday
        var car = new Car(TestLicensePlate, date);

        try
        {
            var brobizzWeekend = new BrobizzWeekend(car);
        }
        catch (DateIsNotWeekendException)
        {
            exceptionCaught = true;
        }

        Assert.IsTrue(exceptionCaught,
            "The exception for the date not being on a weekend day was not thrown.");
    }

    [TestMethod]
    public void ApplyBrobizzWeekendDiscountToCar()
    {
        DateTime date = DateTime.Parse("01-03-2025"); // Saturday
        var car = new Car(TestLicensePlate, date); // Instantiating motorcycle object.
        var brobizzWeekend = new BrobizzWeekend(car);

        double newPrice = brobizzWeekend.GetNewDiscountPrice();

        Assert.AreEqual(230 * (1.0 - 0.15), newPrice, 1e-6,
            "The weekend discount price does not match the actual calculated price.");
    }
}
