using System.Net.Sockets;

namespace TCP_Server;

public class ServerWithProtocol : Server
{
    private readonly SimpleProtocol _protocol;
    private Task? _mainLoopTask;

    public ServerWithProtocol(int port, int maxConnections, SimpleProtocol protocol) : base(port, maxConnections)
    {
        _protocol = protocol;
    }

    public ServerWithProtocol(string host, int port, int maxConnections, SimpleProtocol protocol) : base (host, port, maxConnections)
    {
        _protocol = protocol;
    }

    public void Run()
    {
        ServerAsync();
        ReactionLoop();
    }

    private async void ServerAsync()
    {
        var voidMethod = new Action(Start);
        _mainLoopTask = new Task(voidMethod);
        _mainLoopTask.Start();
        await _mainLoopTask;
    }

    private void ReactionLoop()
    {
        while (_mainLoopTask != null)
        {
            if (_mainLoopTask.IsCompleted || _mainLoopTask.IsCanceled || _mainLoopTask.IsFaulted)
            {
                break;
            }
            lock (ReceivedClientData)
            {
                //var receivedClientDataCopy = ReceivedClientData.ToDictionary(
                //entry => entry.Key, entry => entry.Value);
                foreach (var kvp in ReceivedClientData)
                {
                    if (!string.IsNullOrEmpty(kvp.Value))
                    {
                        _protocol.SelectCommand(kvp.Value);
                        if (_protocol.CommandReceived)
                        {
                            Console.WriteLine($"Command received: {kvp.Value}");
                        }
                        ReceivedClientData[kvp.Key] = string.Empty;
                    }
                }
            }
        }
    }
}
