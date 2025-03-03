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

        double newPrice = Vehicle.Price(brobizz); // Price overload

        // Doing a floating-point operation to check for floating-point error.
        Assert.AreEqual(230 * (1.0 - 0.1), newPrice, 1e-6);
    }
}
