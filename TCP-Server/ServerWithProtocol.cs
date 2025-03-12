using System.Net.Sockets;

namespace TCP_Server;

// Server that uses a protocol when communicating with clients.
public class ServerWithProtocol : Server
{
    private readonly Dictionary<TcpClient, SimpleProtocol> _clientProtocols = [];
    private Task? _mainLoopTask;

    public ServerWithProtocol(int port, int maxConnections) : base(port, maxConnections) { }

    public ServerWithProtocol(string host, int port, int maxConnections) : base (host, port, maxConnections) { }

    public void Run()
    {
        ServerAsync();
        ReactionLoop();
    }

    // Starts running server and the protocol.
    private async void ServerAsync()
    {
        var voidMethod = new Action(Start);
        _mainLoopTask = new Task(voidMethod);
        _mainLoopTask.Start();
        await _mainLoopTask;
    }

    // The server reactions to a client via protocol.
    private void ReactionLoop()
    {
        while (_mainLoopTask != null)
        {
            if (_mainLoopTask.IsCompleted || _mainLoopTask.IsCanceled || _mainLoopTask.IsFaulted)
            {
                break;
            }
            CheckMessagesFromClients();
            ProcessClientMessagesThroughProtocol();
        }
    }

    private void CheckMessagesFromClients()
    {
        lock (ReceivedClientData)
        {
            foreach (var kvp in ReceivedClientData)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    // If client is not already being handled, add to dict.
                    if (!_clientProtocols.ContainsKey(kvp.Key))
                    {
                        // Start a new protocol to handle client with.
                        var newProtocol = new SimpleJSONProtocol(2);
                        newProtocol.StartProtocolRun(kvp.Value);
                        _clientProtocols.Add(kvp.Key, newProtocol);
                    }
                    else
                    {
                        _clientProtocols[kvp.Key].ArgsMessage = kvp.Value;
                    }
                    // Reset the data received from client.
                    ReceivedClientData[kvp.Key] = string.Empty;
                }
            }
        }
    }

    // Passing messages from clients to the protocol for processing.
    private void ProcessClientMessagesThroughProtocol()
    {
        foreach (var kvp in _clientProtocols)
        {
            if (kvp.Value.ProtocolTask.IsCompleted)
            {
                // If protocol has finished processing the client's inputs
                // then send back result to client.
                SendMessageToClient(kvp.Key.GetStream(),
                    kvp.Value.ProtocolTask.Result);
                _clientProtocols.Remove(kvp.Key);
            }
            else if (kvp.Value.MessageWaiter is not null
                 && !kvp.Value.MessageWaiter.IsWaitMessagePushed
                 &&  kvp.Value.MessageWaiter.WaitMessageForServer is not null)
            {
                // If protocol has not finished processing the client's inputs
                // and protocol expects further input from client
                // then inform client of this.
                SendMessageToClient(kvp.Key.GetStream(),
                    kvp.Value.MessageWaiter.WaitMessageForServer);
                kvp.Value.MessageWaiter.IsWaitMessagePushed = true;
            }
        }
    }
}
