using System.Collections.Immutable;

namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    public bool CommandReceived { get; protected set; } = false;
    public byte AllowedArgsCount { get; protected set; }
    public delegate int Command(int value1, int value2);
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Command CommandFunc { get; protected set; }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public abstract void SelectCommand(string command);
    public abstract int ExecuteCommand(int value1, int value2);

    protected static string[] GetCommandNames(Enum @enum)
    {
        return Enum.GetNames(@enum.GetType());
    }

    protected static bool IsValidCommand(string command, Enum @enum)
    {
        if (string.IsNullOrEmpty(command)) { return false; }
        foreach (var cmd in GetCommandNames(@enum))
        {
            if (String.Compare(command, cmd, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
        }
        return false;
    }

    protected static int GetCommandIndex(string command, Enum @enum)
    {
        ImmutableList<string> list = GetCommandNames(@enum).ToImmutableList();
        int commandIndex = list.FindIndex(
            e => String.Compare(command, e, StringComparison.OrdinalIgnoreCase) == 0);
        if (commandIndex == -1)
        {
            throw new ArgumentException("Could not find the given command in the ValidCommands array.",
                nameof(command));
        }
        return commandIndex;
    }

    protected static int ParseDataArgument(string data, int index)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        var rosChar = new ReadOnlySpan<char>(data[index]);
        var succesfulParse = int.TryParse(rosChar, out int result);
        if (succesfulParse)
        {
            return result;
        }
        throw new ArgumentException("Could not parse the client's argument", nameof(data));
    }

    protected int[] ParseAllDataArguments(string data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        string dataNoWhiteSpace = data.Trim();
        int argCount = dataNoWhiteSpace.Length;
        if (argCount > AllowedArgsCount)
        {
            throw new ArgumentOutOfRangeException(nameof(data),
                $"There must not be more than {AllowedArgsCount} arguments given.");
        }
        var args = new int[argCount];
        for (int i = 0; i < argCount; i++)
        {
            args[i] = ParseDataArgument(data, i);
        }
        return args;
    }
}
