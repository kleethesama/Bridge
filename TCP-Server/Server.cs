using System.Net;
using System.Net.Sockets;

namespace TCP_Server;

public class Server
{
    public bool Running { get; private set; }
    public int MaxConnections { get; private set; }
    public IPAddress IPAddress { get; private set; }
    public int Port { get; private set; }
    private TcpListener Listener { get; set; }
    private Task<TcpClient>? TaskClientConnecter { get; set; }
    private List<Task<string?>> TaskReaders { get; set; }

    // All client connections for sending/receiving.
    // Clients remain even if connection is closed.
    private Dictionary<TcpClient, NetworkStream> ClientStreams { get; set; }

    public Server(string? host, int port, int maxConnections)
    {
        Running = false;
        IPAddress = host != null ? IPAddress.Parse(host) : IPAddress.Loopback;
        Port = port;
        MaxConnections = maxConnections;
        Listener = new TcpListener(IPAddress, Port);
        TaskReaders = [];
        ClientStreams = [];
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
            if (TaskClientConnecter.IsCompleted && ClientStreams.Count < MaxConnections)
            {
                var newClient = TaskClientConnecter.Result;
                if (!ClientStreams.ContainsKey(newClient))
                {
                    var newClientStream = newClient.GetStream();
                    ClientStreams.Add(newClient, newClientStream);
                    TaskReaders.Add(ReceiveMessageFromClientAsync(newClientStream));
                    Console.WriteLine($"Added Client - IP Address is: {GetClientIPAddress(newClientStream)}" +
                        $"\nCurrent amount of clients: {ClientStreams.Count}");
                }
                TaskClientConnecter = Listener.AcceptTcpClientAsync();
                Console.WriteLine("Listening for another new client...");
            }
            foreach (var stream in ClientStreams.Values)
            {
                if (stream.Socket.Connected)
                {
                    // Server will throw exception if a closed connection (from the client) isn't closed on the server.
                    // Use this for checking if a client is still connected:
                    // https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connected?view=net-8.0

                    for (int i = 0; i < TaskReaders.Count; i++)
                    {
                        try
                        {
                            if (TaskReaders[i] != null)
                            {
                                if (TaskReaders[i].IsCompleted)
                                {
                                    if (!String.IsNullOrEmpty(TaskReaders[i].Result))
                                    {
                                        SendMessageToClient(stream, $"Received your message: {TaskReaders[i].Result}");
                                    }
                                    TaskReaders[i] = ReceiveMessageFromClientAsync(stream);
                                }
                            }
                            else
                            {
                                TaskReaders[i] = ReceiveMessageFromClientAsync(stream);
                            }
                        }
                        catch (AggregateException e)
                        {
                            if (e.InnerException != null)
                            {

                            }
                        }
                    }
                }
            }
        }
        // If the loop somehow breaks, the server is no longer running.
        Stop();
    }

    public void Stop()
    {
        Running = false;
        Listener.Stop();
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
