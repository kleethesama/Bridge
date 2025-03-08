using System.Net;
using System.Net.Sockets;
using TCP_Server.Base_classes;

namespace TCP_Server;

public class Server
{
    public bool Running { get; private set; } = false;
    public int MaxConnections { get; private set; }

    public IPAddress IPAddress { get; private set; }
    public int Port { get; private set; }
    private TcpListener Listener { get; set; }
    private Task<TcpClient>? TaskClientConnecter { get; set; }
    private List<Task<string?>> ClientDataTaskReaders { get; } = [];

    // All client connections for sending/receiving.
    // Clients remain even if connection is closed.
    private Dictionary<TcpClient, NetworkStream> ClientStreams { get; } = [];

    private readonly Protocol _protocol;

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
            ListenForNewClient();
            HandleAllCurrentClients();
        }
        // If the loop somehow breaks, the server is no longer running.
        Stop();
    }

    public void Stop()
    {
        Running = false;
        Listener.Stop();
    }

    private void ListenForNewClient()
    {
        if (TaskClientConnecter.IsCompleted && ClientStreams.Count < MaxConnections)
        {
            var newClient = TaskClientConnecter.Result;
            if (!ClientStreams.ContainsKey(newClient))
            {
                var newClientStream = newClient.GetStream();
                ClientStreams.Add(newClient, newClientStream);
                ClientDataTaskReaders.Add(ReceiveMessageFromClientAsync(newClientStream));
                Console.WriteLine($"Added Client - IP Address is: {GetClientIPAddress(newClientStream)}" +
                    $"\nCurrent amount of clients: {ClientStreams.Count}");
            }
            TaskClientConnecter = Listener.AcceptTcpClientAsync();
            Console.WriteLine("Listening for another new client...");
        }
    }

    private void ReadDataFromClient(NetworkStream stream, int index)
    {
        //try
        //{
            if (ClientDataTaskReaders[index] != null)
            {
                if (ClientDataTaskReaders[index].IsCompletedSuccessfully)
                {
                    if (!String.IsNullOrEmpty(ClientDataTaskReaders[index].Result))
                    {
                        SendMessageToClient(stream, $"Received your message: {ClientDataTaskReaders[index].Result}");
                    }
                    ClientDataTaskReaders[index] = ReceiveMessageFromClientAsync(stream);
                }
            }
            else
            {
                ClientDataTaskReaders[index] = ReceiveMessageFromClientAsync(stream);
            }
        //}
        //catch (AggregateException e)
        //{
        //    if (e.InnerException != null)
        //    {

        //    }
        //}
    }

    private void HandleAllCurrentClients()
    {
        foreach (var stream in ClientStreams.Values)
        {
            if (stream.Socket.Connected)
            {
                // Server will throw exception if a closed connection (from the client) isn't closed on the server.
                // Use this for checking if a client is still connected:
                // https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connected?view=net-8.0
                for (int i = 0; i < ClientDataTaskReaders.Count; i++)
                {
                    ReadDataFromClient(stream, i);
                }
            }
        }
    }

    public void SetMaxConnections(int maxConnections)
    {
        MaxConnections = maxConnections;
    }

    private static string? GetClientIPAddress(NetworkStream stream)
    {
        // Source: https://stackoverflow.com/questions/60953041/tcplistener-tcpclient-get-ipaddress
        var remoteIpEndPoint = stream.Socket.RemoteEndPoint as IPEndPoint;
        return remoteIpEndPoint?.Address.ToString();
    }

    private static async Task<string?> ReceiveMessageFromClientAsync(NetworkStream stream)
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
