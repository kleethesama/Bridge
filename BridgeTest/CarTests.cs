using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class CarTests
{
    public string LicensePlate = "HF38663";
    public DateTime Date = DateTime.Now;

    [TestMethod]
    public void FixedCarPrice()
    {
        var car = new Car(LicensePlate, Date);

        double carPrice = car.Price();

        Assert.IsTrue(carPrice == 230);
    }

    [TestMethod]
    public void VehicleTypeIsCar()
    {
        var car = new Car(LicensePlate, Date);

        string carType = car.VehicleType();

        Assert.IsTrue(carType == "Car");
    }
}
