using System.Collections.Immutable;
using TCP_Server.Exceptions;

namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    public delegate int Command(int value1, int value2); // Having the parameter be an int[] might've been better.
    public Command? CommandFunc { get; protected set; } // Used for picking a function to execute.
    public string? ArgsMessage { get; set; } // Arguments received from server.
    public byte ExpectedArgsCount { get; protected set; }
    public Task<string>? ProtocolTask { get; protected set; } // The thread object for running the protocol loop.

    protected abstract void SelectCommand(short commandType);
    protected abstract Task<string> RunProtocol(string command);

    protected int ExecuteCommand(int value1, int value2)
    {
        if (CommandFunc is null)
        {
            throw new CommandNotSelectedException("A command was not selected before execution.");
        }
        return CommandFunc(value1, value2);
    }

    public void StartProtocolRun(string command)
    {
        ProtocolTask = RunProtocol(command);
    }

    // Finds all whitespace indicies for a string that contains arguments.
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
                    Console.WriteLine("There were an incorrect amount of arguments.");
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

    // Puts all arguments from the arguments string into an array.
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

    protected static int ParseArgument(string arg, out bool succesfulParse)
    {
        ArgumentNullException.ThrowIfNull(arg, nameof(arg));
        succesfulParse = int.TryParse(arg, out int result);
        return result;
    }

    protected static int[] ParseAllArguments(string[] args, out bool allParsingSucceeded)
    {
        ArgumentNullException.ThrowIfNull(args, nameof(args));
        var parsedArgs = new int[args.Length];
        allParsingSucceeded = true;
        for (int i = 0; i < parsedArgs.Length; i++)
        {
            parsedArgs[i] = ParseArgument(args[i], out allParsingSucceeded);
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
