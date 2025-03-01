using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class CarTests
{
    public string TestLicensePlate = "HF38663";
    public DateTime TestDate = DateTime.Now;

    [TestMethod]
    public void FixedCarPrice()
    {
        var car = new Car(TestLicensePlate, TestDate);

        double carPrice = car.Price();

        Assert.IsTrue(carPrice == 230, "The price does not match the value 230.");
    }

    [TestMethod]
    public void VehicleTypeIsCar()
    {
        var car = new Car(TestLicensePlate, TestDate);

        string carType = car.VehicleType();

        Assert.IsTrue(carType == "Car", """The vehicle type does not match "Car".""");
    }

    [TestMethod]
    public void SetLicensePlateMaxCharactersThrowException()
    {
        var exceptionCaught = false;
        try
        {
            var car = new Car(TestLicensePlate + "9", TestDate);
        }
        catch (ArgumentException ex)
        {
            if (ex.ParamName == "LicensePlate")
            {
                exceptionCaught = true;
            }
        }
        Assert.IsTrue(exceptionCaught,
            "The exception for maximum characters in a license plate was not thrown.");
    }
}
