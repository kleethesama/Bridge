using System.Collections.Immutable;
using System.Net.Sockets;
using TCP_Server.Base_classes;

namespace TCP_Server;

public class SimpleProtocol : Protocol
{
    private enum CommandType : int
    {
        Random,
        Add,
        Subtract
    }

    public SimpleProtocol(TcpClient client)
    {
        Client = client;
    }

    private static CommandType ParseCommandType(string command)
    {
        ImmutableList<string> list = Enum.GetNames(typeof(CommandType)).ToImmutableList();
        int commandIndex = list.FindIndex(
            e => String.Compare(command, e, StringComparison.OrdinalIgnoreCase) == 0);
        if (commandIndex == -1)
        {
            throw new ArgumentException("Could not find the given command type.",
                nameof(command));
        }
        return (CommandType)commandIndex;
    }

    public override void SelectCommand(string command)
    {
        switch (ParseCommandType(command))
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

    public override bool ExecuteCommand(int value1, int value2)
    {
        if (CommandFunc is null)
        {
            Console.WriteLine("No command has been selected. Please, select a command."); // Send this to client.
            return false;
        }
        CommandFunc(value1, value2);
        return true;
    }

    private static int Random(int minValue, int maxValue)
    {
        var randomizer = new Random();
        return randomizer.Next(minValue, maxValue);
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
