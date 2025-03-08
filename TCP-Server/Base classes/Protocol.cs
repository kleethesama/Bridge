using System.Collections.Immutable;
using System.Net.Sockets;

namespace TCP_Server.Base_classes;

public abstract class Protocol
{
    protected TcpClient Client { get; set; }
    public bool CommandReceived { get; protected set; } = false;
    public string[] ValidCommands { get; protected set; }
    public byte AllowedArgsCount { get; protected set; }
    public delegate int Command(int value1, int value2);

    public abstract void SelectCommand(string command);

    public abstract void ExecuteCommand();

    public bool IsValidCommand(string command)
    {
        if (string.IsNullOrEmpty(command)) { return false; }
        foreach (var cmd in ValidCommands)
        {
            if (String.Compare(command, cmd, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
        }
        return false;
    }

    protected int GetCommandIndex(string command)
    {
        ImmutableList<string> list = ValidCommands.ToImmutableList();
        int commandIndex = list.FindIndex(
            e => String.Compare(command, e, StringComparison.OrdinalIgnoreCase) == 0);
        if (commandIndex == -1)
        {
            throw new ArgumentException("Could not find the given command in the ValidCommands array.",
                nameof(command));
        }
        return commandIndex;
    }

    public static int ParseDataArgument(string data, int index)
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

    public int[] ParseAllDataArguments(string data)
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
