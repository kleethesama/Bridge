using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Server;

public class Client
{
    private readonly TcpClient _socket;

    public Client(string server, int port)
    {
        _socket = new TcpClient(server, port);
    }

    // Receive response from the server.
    public string ReceiveMessage()
    {
        // Buffer to store the response bytes.
        var data = new Byte[256];

        // String to store the response ASCII representation.
        string responseData = string.Empty;

        // Read the first batch of the TcpServer response bytes.
        Int32 bytes = _socket.GetStream().Read(data, 0, data.Length);
        responseData = Encoding.ASCII.GetString(data, 0, bytes);
        Console.WriteLine($"Client (me) received this reply from server: {responseData}");
        return responseData;
    }

    public void SendMessage(string message)
    {
        // Translate the passed message into ASCII and store it as a Byte array.
        Byte[] data = Encoding.ASCII.GetBytes(message);

        // Send the message to the connected TcpServer.
        using var stream = _socket.GetStream();
        stream.Write(data, 0, data.Length);
        stream.Flush();

        Console.WriteLine($"Client (me) sent this to server: {message}");
    }

    public void Disconnect()
    {
        _socket.Close();
    }
}
