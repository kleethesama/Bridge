using Bridge;

namespace BridgeTest;

[TestClass]
public sealed class MCTests
{
    public string TestLicensePlate = "AF12712";
    public DateTime TestDate = DateTime.Now;

    [TestMethod]
    public void FixedMCPrice()
    {
        var mc = new MC(TestLicensePlate, TestDate);

        double mcPrice = mc.Price();

        Assert.IsTrue(mcPrice == 120, "The price does not match the value 120.");
    }

    [TestMethod]
    public void VehicleTypeIsMC()
    {
        var mc = new MC(TestLicensePlate, TestDate);

        string mcType = mc.VehicleType();

        Assert.IsTrue(mcType == "MC", """The vehicle type does not match "MC".""");
    }

    [TestMethod]
    public void SetLicensePlateMaxCharactersThrowException()
    {
        var exceptionCaught = false;
        try
        {
            var mc = new MC(TestLicensePlate + "9", TestDate);
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