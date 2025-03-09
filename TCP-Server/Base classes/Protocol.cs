namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    public bool CommandReceived { get; protected set; } = false;
    public byte ExpectedArgsCount { get; protected set; }
    public delegate int Command(int value1, int value2);
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Command CommandFunc { get; protected set; }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected abstract ushort ParseCommandType(string command);
    public abstract void SelectCommand(string command);
    public abstract int ExecuteCommand(int value1, int value2);

    protected int[] FindArgumentSeperationPositions(string data)
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
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentException($"Expected {ExpectedArgsCount} arguments. " +
                        $"{argCounter} or more were given.", nameof(data), e);
                }
            }
        }
        if (argCounter != ExpectedArgsCount)
        {
            throw new ArgumentException($"Expected {ExpectedArgsCount} arguments. " +
                $"Only {argCounter} were given.", nameof(data));
        }
        return argPoses;
    }

    protected string[] SeperateArgumentsIntoArray(string data)
    {
        var args = new string[ExpectedArgsCount];
        var argPoses = FindArgumentSeperationPositions(data);
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

    protected static int ParseDataArgument(string arg)
    {
        ArgumentNullException.ThrowIfNull(arg, nameof(arg));
        bool succesfulParse = int.TryParse(arg, out int result);
        if (succesfulParse)
        {
            return result;
        }
        throw new ArgumentException("Could not parse the client's argument", nameof(arg));
    }

    protected int[] ParseAllDataArguments(string data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var args = SeperateArgumentsIntoArray(data);
        var parsedArgs = new int[args.Length];
        for (int i = 0; i < parsedArgs.Length; i++)
        {
            parsedArgs[i] = ParseDataArgument(args[i]);
        }
        return parsedArgs;
    }
}
