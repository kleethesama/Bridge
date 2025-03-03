using Bridge;
using Bridge.Price_classes;

namespace BridgeTest;

[TestClass]
public class PriceTests
{
    [TestMethod]
    public void ApplyBrobizzDiscountToCar()
    {
        var vehicle = new Car("HF38663", DateTime.Now);
        var brobizz = new Brobizz(vehicle);

        double originalPrice = brobizz.Vehicle.Price();
        double newPrice = Vehicle.Price(brobizz); // Price overload

        // Doing a floating-point operation to check for floating-point error.
        Assert.AreEqual(newPrice, originalPrice * (1.0 - 0.1), 1e-6);
    }
}
