using System.Net.Sockets;
using TCP_Server.Base_classes;

namespace TCP_Server;

public class SimpleProtocol : Protocol
{
    public Command CommandFunc { get; private set; }

    public SimpleProtocol(TcpClient client)
    {
        Client = client;
        ValidCommands = ["Random", "Add", "Subtract"];
    }

    public override void SelectCommand(string command)
    {
        if (!IsValidCommand(command))
        {
            throw new ArgumentException($"The given command: {command}\nis not valid.", nameof(command));
        }
        switch (GetCommandIndex(command)) // Maybe an enum would be better?
        {
            case 0:
                CommandFunc = Random;
                break;

            case 1:
                CommandFunc = Add;
                break;

            case 2:
                CommandFunc = Subtract;
                break;
        }
    }

    public override void ExecuteCommand()
    {
        if (CommandFunc is null)
        {

        }
    }

    public static int Random(int minValue, int maxValue)
    {
        var randomizer = new Random();
        return randomizer.Next(minValue, maxValue);
    }

    public static int Add(int value1, int value2)
    {
        return value1 + value2;
    }

    public static int Subtract(int value1, int value2)
    {
        return value1 - value2;
    }
}
