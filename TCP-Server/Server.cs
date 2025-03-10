using System.Net;
using System.Net.Sockets;

namespace TCP_Server;

public class Server
{
    public bool Running { get; protected set; } = false;
    public int MaxConnections { get; protected set; }
    public IPAddress IPAddress { get; protected set; }
    public int Port { get; protected set; }
    protected TcpListener Listener { get; set; }
    protected Task<TcpClient>? TaskClientConnecter { get; set; }
    protected Dictionary<TcpClient, Task<string?>> ClientDataTaskReaders { get; } = [];

    // All client connections for sending/receiving.
    // Clients remain even if connection is closed.
    public Dictionary<TcpClient, string> ReceivedClientData { get; protected set; } = [];

    public Server(int port, int maxConnections)
    {
        IPAddress = IPAddress.Loopback;
        Port = port;
        MaxConnections = maxConnections;
        Listener = new TcpListener(IPAddress, Port);
    }

    public Server(string host, int port, int maxConnections)
    {
        IPAddress = IPAddress.Parse(host);
        Port = port;
        MaxConnections = maxConnections;
        Listener = new TcpListener(IPAddress, Port);
    }

    // Main thread
    public void Start()
    {
        Running = true;
        Listener.Start();
        TaskClientConnecter = Listener.AcceptTcpClientAsync();
        Console.WriteLine("Server is listening for a client...");

        // Loop for server getting new clients, reading data from them and replying to them.
        while (Running)
        {
            lock (ReceivedClientData)
            {
                ListenForNewClient();
                HandleAllCurrentClients();
            }
        }

        // If the loop somehow breaks, the server is no longer running.
        StopListening();
    }

    public void StopListening()
    {
        Running = false;
        Listener.Stop();
    }

    public void ReleaseAll()
    {
        foreach (var client in ReceivedClientData)
        {
            client.Key.Close();
        }
        ReceivedClientData = [];
    }

    protected void ListenForNewClient()
    {
        if (TaskClientConnecter.IsCompleted && ReceivedClientData.Count < MaxConnections)
        {
            var newClient = TaskClientConnecter.Result;
            if (ReceivedClientData.TryAdd(newClient, string.Empty))
            {
                var newClientStream = newClient.GetStream();
                ClientDataTaskReaders.Add(newClient, ReceiveMessageFromClientAsync(newClientStream));
                Console.WriteLine($"Added Client - IP Address is: {GetClientIPAddress(newClientStream)}" +
                    $"\nCurrent amount of clients: {ReceivedClientData.Count}");
            }
            TaskClientConnecter = Listener.AcceptTcpClientAsync();
            Console.WriteLine("Listening for another new client...");
        }
    }

    protected void ReadDataFromClient(TcpClient client)
    {
        if (ClientDataTaskReaders[client] != null)
        {
            if (ClientDataTaskReaders[client].IsCompleted)
            {
                if (!String.IsNullOrEmpty(ClientDataTaskReaders[client].Result))
                {
                    ReceivedClientData[client] = ClientDataTaskReaders[client].Result;
                }
                ClientDataTaskReaders[client] = ReceiveMessageFromClientAsync(client.GetStream());
            }
        }
        else
        {
            ClientDataTaskReaders[client] = ReceiveMessageFromClientAsync(client.GetStream());
        }
    }

    protected void HandleAllCurrentClients()
    {
        foreach (var client in ReceivedClientData.Keys)
        {
            if (client.GetStream().Socket.Connected)
            {
                // Server will throw exception if a closed connection (from the client) isn't closed on the server.
                // Use this for checking if a client is still connected:
                // https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connected?view=net-8.0
                for (int i = 0; i < ClientDataTaskReaders.Count; i++)
                {
                    ReadDataFromClient(client);
                }
            }
        }
    }

    public void SetMaxConnections(int maxConnections)
    {
        MaxConnections = maxConnections;
    }

    protected static string? GetClientIPAddress(NetworkStream stream)
    {
        // Source: https://stackoverflow.com/questions/60953041/tcplistener-tcpclient-get-ipaddress
        var remoteIpEndPoint = stream.Socket.RemoteEndPoint as IPEndPoint;
        return remoteIpEndPoint?.Address.ToString();
    }

    protected static async Task<string?> ReceiveMessageFromClientAsync(NetworkStream stream)
    {
        var reader = new StreamReader(stream); // Disposing will close connection with client.
        var line = await reader.ReadLineAsync();
        if (!String.IsNullOrEmpty(line))
        {
            Console.WriteLine($"Received a message from client: {GetClientIPAddress(stream)}\n" +
            $"Their message is: {line}");
        }
        return line;
    }

    public static void SendMessageToClient(NetworkStream stream, string message)
    {
        var writer = new StreamWriter(stream) // Disposing will close connection with client.
        {
            AutoFlush = true
        };
        writer.Write(message);
        Console.WriteLine($"Server sent message: {message}");
    }
}
