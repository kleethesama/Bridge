using System.Collections.Immutable;

namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    public delegate int Command(int value1, int value2);
    public Command? CommandFunc { get; protected set; }
    public string? ArgsMessage { get; set; }
    public byte ExpectedArgsCount { get; protected set; }
    public bool WaitingForArgs { get; set; } = false;
    public Task<string>? ProtocolTask { get; protected set; }

    protected abstract void SelectCommand(short commandType);
    protected abstract Task<string> RunProtocol(string command);

    protected int ExecuteCommand(int value1, int value2)
    {
        return CommandFunc(value1, value2);
    }

    public void StartProtocolRun(string command)
    {
        ProtocolTask = RunProtocol(command);
    }

    protected int[]? FindArgumentSeperationPositions(string data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var argPoses = new int[ExpectedArgsCount];
        argPoses[0] = 0;
        int argCounter = 1;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == ' ')
            {
                try
                {
                    argPoses[argCounter++] = i + 1;
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
        }
        if (argCounter != ExpectedArgsCount)
        {
            return null;
        }
        return argPoses;
    }

    protected string[]? SeperateArgumentsIntoArray(string data)
    {
        var args = new string[ExpectedArgsCount];
        var argPoses = FindArgumentSeperationPositions(data);
        if (argPoses is null)
        {
            return null;
        }
        for (int i = 0; i < ExpectedArgsCount; i++)
        {
            if (ExpectedArgsCount - 1 != i)
            {
                args[i] = data.Substring(argPoses[i], argPoses[i + 1] - 1);
            }
            else
            {
                args[i] = data.Substring(argPoses[i], data.Length - argPoses[i]);
            }
        }
        return args;
    }

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

    protected static short ParseCommandType(string command, Type enumType)
    {
        ImmutableList<string> list = Enum.GetNames(enumType).ToImmutableList();
        int commandIndex = list.FindIndex(
            e => String.Compare(command, e, StringComparison.OrdinalIgnoreCase) == 0);
        short commandNumber = (short)commandIndex;
        return commandNumber;
    }

    protected static bool IsCommandValid(short commandType)
    {
        return commandType != -1;
    }
}
