using System.Collections.Immutable;
using TCP_Server.Base_classes;

namespace TCP_Server;

public class SimpleProtocol : TextBasedArgumentProtocol
{
    public Task<string>? ProtocolTask { get; private set; }

    public enum CommandType : short
    {
        Random,
        Add,
        Subtract
    }

    public SimpleProtocol()
    {
        ExpectedArgsCount = 2;
    }

    public void StartProtocolRun(string command)
    {
        ProtocolTask = RunProtocol(command);
    }

    public bool IsProtocolRunning()
    {
        if (ProtocolTask is null)
        {
            return false;
        }
        return ProtocolTask.Status == TaskStatus.Running;
    }

    private async Task<string> RunProtocol(string command)
    {
        // Parse command from server.
        short commandType = ParseCommandType(command, typeof(CommandType));
        if (!IsCommandValid(commandType))
        {
            return "Command is not valid. Please, try again.";
        }
        var newCommand = (CommandType)commandType;
        SelectCommand(newCommand);

        // Await args from server.
        await WaitForServerMessage();
        if (ArgsMessage is null)
        {
            return "Timeout. Waited too long for arguments to be receieved.";
        }

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

    private async Task WaitForServerMessage()
    {
        while (ArgsMessage is null)
        {
            await Task.Yield();
        }
    }

    private static bool IsCommandValid(short commandType)
    {
        return commandType != -1;
    }

    protected override short ParseCommandType(string command, Type enumType)
    {
        ImmutableList<string> list = Enum.GetNames(enumType).ToImmutableList();
        int commandIndex = list.FindIndex(
            e => String.Compare(command, e, StringComparison.OrdinalIgnoreCase) == 0);
        short commandNumber = (short)commandIndex;
        return commandNumber;
    }

    private void SelectCommand(CommandType commandType)
    {
        switch (commandType)
        {
            case CommandType.Random:
                CommandFunc = Random;
                break;

            case CommandType.Add:
                CommandFunc = Add;
                break;

            case CommandType.Subtract:
                CommandFunc = Subtract;
                break;
        }
    }

    protected override int ExecuteCommand(int value1, int value2)
    {
        return CommandFunc(value1, value2);
    }

    private static int Random(int minValue, int maxValue)
    {
        var randomizer = new Random();
        return randomizer.Next(minValue, maxValue + 1); // +1 to include maxValue in range.
    }

    private static int Add(int value1, int value2)
    {
        return value1 + value2;
    }

    private static int Subtract(int value1, int value2)
    {
        return value1 - value2;
    }
}
