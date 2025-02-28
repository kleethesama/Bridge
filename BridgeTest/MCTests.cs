using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class MCTests
{
    [TestMethod]
    public void FixedMCPrice()
    {
        var mc = new MC();

        double mcPrice = mc.Price();

        Assert.IsTrue(mcPrice == 120);
    }

    [TestMethod]
    public void VehicleTypeIsMC()
    {
        var mc = new MC();

        string mcType = mc.VehicleType();

        Assert.IsTrue(mcType == "MC");
    }
}