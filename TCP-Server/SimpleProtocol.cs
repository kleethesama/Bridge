using TCP_Server.Base_classes;
using TCP_Server.Exceptions;

namespace TCP_Server;

public class SimpleProtocol : Protocol
{
    public enum CommandType : short
    {
        Random,
        Add,
        Subtract
    }

    public SimpleProtocol(byte expectedArgsCount)
    {
        ExpectedArgsCount = expectedArgsCount;
    }

    protected override async Task<string> RunProtocol(string command)
    {
        // Parse command from server.
        short commandType = ParseCommandType(command, typeof(CommandType));
        if (!IsCommandValid(commandType))
        {
            return "Command is not valid. Please, try again.";
        }
        SelectCommand(commandType);

        // Await args from server.
        await WaitForServerMessage();

        // Handle args and perform command execution.
        string[]? args = SeperateArgumentsIntoArray(ArgsMessage);
        if (args is null)
        {
            return $"Expected {ExpectedArgsCount} arguments for the command {CommandFunc.Method.Name}.";
        }
        int[] argValues = ParseAllData(args, out bool IsDataParsedSuccesfully);
        int executionValue;
        if (IsDataParsedSuccesfully)
        {
            executionValue = ExecuteCommand(argValues[0], argValues[1]);
        }
        else
        {
            return $"These are not valid arguments: {ArgsMessage}";
        }

        // Return value to server.
        return executionValue.ToString();
    }

    protected override void SelectCommand(short commandType)
    {
        CommandFunc = (CommandType)commandType switch
        {
            CommandType.Random => Random,
            CommandType.Add => Add,
            CommandType.Subtract => Subtract,
            _ => throw new CommandNotSelectedException("A command was not selected."),
        };
    }

    protected static int Random(int minValue, int maxValue)
    {
        var randomizer = new Random();
        return randomizer.Next(minValue, maxValue + 1); // +1 to include maxValue in range.
    }

    protected static int Add(int value1, int value2)
    {
        return value1 + value2;
    }

    protected static int Subtract(int value1, int value2)
    {
        return value1 - value2;
    }
}
