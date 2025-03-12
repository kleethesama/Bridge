namespace TCP_Server;

// I want to inject it into the protocol, but am short on time.
public class ProtocolCommandContainer
{
    public enum CommandType : short
    {
        Random,
        Add,
        Subtract
    }

    public static int Random(int minValue, int maxValue)
    {
        var randomizer = new Random();
        return randomizer.Next(minValue, maxValue + 1); // +1 to include maxValue in range.
    }

    public static int Add(int value1, int value2)
    {
        return value1 + value2;
    }

    public static int Subtract(int value1, int value2)
    {
        return value1 - value2;
    }
}
