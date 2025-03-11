﻿using System.Net.Sockets;

namespace TCP_Server;

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
                foreach (var kvp in ReceivedClientData)
                {
                    if (!string.IsNullOrEmpty(kvp.Value))
                    {
                        if (!_clientProtocols.ContainsKey(kvp.Key))
                        {
                            var newProtocol = new SimpleProtocol();
                            newProtocol.StartProtocolRun(kvp.Value);
                            _clientProtocols.Add(kvp.Key, newProtocol);
                        }
                        else
                        {
                            _clientProtocols[kvp.Key].ArgsMessage = kvp.Value;
                        }
                        ReceivedClientData[kvp.Key] = string.Empty;
                    }
                }
            }
            var _clientProtocolsCopy = _clientProtocols.ToDictionary(entry => entry.Key,
                                                                     entry => entry.Value);
            foreach (var clientProtocol in _clientProtocolsCopy)
            {
                if (clientProtocol.Value.ProtocolTask.IsCompleted)
                {
                    SendMessageToClient(clientProtocol.Key.GetStream(),
                        clientProtocol.Value.ProtocolTask.Result);
                    _clientProtocols.Remove(clientProtocol.Key);
                }
            }
        }
    }
}
