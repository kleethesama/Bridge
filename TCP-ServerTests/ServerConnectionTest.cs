using TCP_Server;

namespace TCP_ServerTests;

[TestClass]
public sealed class ServerConnectionTest
{
    public const string HostAddress = "127.0.0.1";
    public const ushort Port = 7;
    public const int MaxConnections = 1;
    public const string Message = "Hello!";
    public readonly TimeSpan WaitTime = new(0, 0, 1); // 1 second wait time.

    [TestMethod]
    public void ReceiveDataFromClient()
    {
        // Arrange
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        // Act
        client.SendMessage(Message);
        Thread.Sleep(WaitTime);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(server.LatestMessage), "The received message from client is null or empty.");
        client.Disconnect();
        server.Stop();
        server.Release();
    }

    [TestMethod]
    public void DataIntegrityFromClientToServer()
    {
        // Arrange
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        // Act
        client.SendMessage(Message);
        Thread.Sleep(WaitTime);

        // Assert
        Assert.IsTrue(server.LatestMessage == Message, "The received message from client does not match.");
        client.Disconnect();
        server.Stop();
        server.Release();
    }

    [TestMethod]
    public void MessageFromServerToClient()
    {
        // Arrange
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        // Act
        Thread.Sleep(WaitTime);
        var messageReceiver = server.ClientStreams.Values.ToArray()[0];
        server.SendMessageToClient(messageReceiver, Message);
        var messageFromServer = client.ReceiveMessage();

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(messageFromServer), "The received message from server is null or empty.");
        client.Disconnect();
        server.Stop();
        server.Release();
    }

    [TestMethod]
    public void DataIntegrityFromServerToClient()
    {
        // Arrange
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        // Act
        Thread.Sleep(WaitTime);
        var messageReceiver = server.ClientStreams.Values.ToArray()[0];
        server.SendMessageToClient(messageReceiver, Message);
        var messageFromServer = client.ReceiveMessage();

        // Assert
        Assert.IsTrue(messageFromServer == Message, "The received message from server does not match.");
        client.Disconnect();
        server.Stop();
        server.Release();
    }

    [TestMethod]
    public void UpperCaseMessageFromServerToClient()
    {
        // Arrange
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        // Act
        Thread.Sleep(WaitTime);
        var messageReceiver = server.ClientStreams.Values.ToArray()[0];
        server.SendUpperCaseMessageToClient(messageReceiver, Message);
        var messageFromServer = client.ReceiveMessage();

        // Assert
        Assert.IsTrue(messageFromServer == Message.ToUpper(), "The received message from server is not upper case.");
        client.Disconnect();
        server.Stop();
        server.Release();
    }
}
