namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    public bool CommandReceived { get; protected set; } = false;
    public delegate int Command(int value1, int value2);
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Command CommandFunc { get; protected set; }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected abstract short ParseCommandType(string command, Type type);
    //protected abstract void SelectCommand(string command);
    protected abstract int ExecuteCommand(int value1, int value2);

    protected static int ParseDataArgument(string data, out bool succesfulParse)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        succesfulParse = int.TryParse(data, out int result);
        return result;
    }

    protected static int[] ParseAllData(string[] allData, out bool allParsingSucceeded)
    {
        ArgumentNullException.ThrowIfNull(allData, nameof(allData));
        var parsedArgs = new int[allData.Length];
        allParsingSucceeded = true;
        for (int i = 0; i < parsedArgs.Length; i++)
        {
            parsedArgs[i] = ParseDataArgument(allData[i], out allParsingSucceeded);
            if (!allParsingSucceeded)
            {
                allParsingSucceeded = false;
                break;
            }
        }
        return parsedArgs;
    }
}
