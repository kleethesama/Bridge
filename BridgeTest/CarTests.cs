using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class CarTests
{
    [TestMethod]
    public void FixedCarPrice()
    {
        var car = new Car();

        double carPrice = car.Price();

        Assert.IsTrue(carPrice == 230);
    }

    [TestMethod]
    public void VehicleTypeIsCar()
    {
        var car = new Car();

        string carType = car.VehicleType();

        Assert.IsTrue(carType == "Car");
    }
}
