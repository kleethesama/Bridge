using System.Collections.Immutable;
using TCP_Server.Base_classes;

namespace TCP_Server;

public class SimpleProtocol : TextBasedArgumentProtocol
{
    public Task<string?> ProtocolTask { get; private set; }

    public enum CommandType : short
    {
        Random,
        Add,
        Subtract
    }

    //public SimpleProtocol(byte expectedArgsCount)
    //{
    //    ExpectedArgsCount = expectedArgsCount;
    //}

    //public SimpleProtocol(CommandType commandType, byte expectedArgsCount)
    //{
    //    ExpectedArgsCount = expectedArgsCount;
    //}

    public SimpleProtocol(string command, byte expectedArgsCount)
    {
        ExpectedArgsCount = expectedArgsCount;
        ProtocolTask = RunProtocol(command);
    }

    public async Task<string?> RunProtocol(string command)
    {
        // Parse command from server.
        short commandType = ParseCommandType(command, typeof(CommandType));
        if (!IsCommandValid(commandType)) { return null; }
        var newCommand = (CommandType)commandType;
        SelectCommand(newCommand);

        // Await args from server.
        _ = await WaitForServerMessage();

        // Handle args and perform command execution.
        string[] args = SeperateArgumentsIntoArray(CurrentServerMessage);
        int[] argValues = ParseAllData(args);
        int executionValue = ExecuteCommand(argValues[0], argValues[1]);

        // Return value to server.
        return executionValue.ToString();
    }

    private async Task<bool> WaitForServerMessage()
    {
        while (CurrentServerMessage is null)
        {
            await Task.Yield();
        }
        return true;
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

    public void SelectCommand(CommandType commandType)
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

    public override int ExecuteCommand(int value1, int value2)
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
