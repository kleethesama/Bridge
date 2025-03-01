using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class MCTests
{
    public string LicensePlate = "AF12712";
    public DateTime Date = DateTime.Now;

    [TestMethod]
    public void FixedMCPrice()
    {
        var mc = new MC(LicensePlate, Date);

        double mcPrice = mc.Price();

        Assert.IsTrue(mcPrice == 120);
    }

    [TestMethod]
    public void VehicleTypeIsMC()
    {
        var mc = new MC(LicensePlate, Date);

        string mcType = mc.VehicleType();

        Assert.IsTrue(mcType == "MC");
    }
}