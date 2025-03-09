using TCP_Server;

namespace TCP_ServerTests;

[TestClass]
public sealed class ServerClientTest
{
    public const string HostAddress = "127.0.0.1";
    public const ushort Port = 7;
    public const int MaxConnections = 1;
    public const string Message = "Hello!";
    public readonly TimeSpan OneSecond = new(0, 0, 1); // 1 second wait time.

    //[TestMethod]
    //[Timeout(TestTimeout.Infinite)]
    //public void StartRunningServer()
    //{
    //    var server = new Server(HostAddress, Port, MaxConnections);
    //    server.Start();
    //}

    [TestMethod]
    [Timeout(5000)]
    public void ReceiveExactDataFromServerToClient()
    {
        var server = new Server(HostAddress, Port, MaxConnections);
        server.Start();
        var client = new Client(HostAddress, Port);

        client.SendMessage(Message);
        Thread.Sleep(OneSecond);
        var clientMessages = server.ReceivedClientData.Values.ToArray();

        Assert.IsTrue(clientMessages[0] == Message, "The received message from client does not match.");
        client.Disconnect();
        server.StopListening();
        server.ReleaseAll();
    }

    //[TestMethod]
    //[Timeout(5000)]
    //public void ReceiveExactDataFromClientToServer()
    //{
    //    var server = new Server(HostAddress, Port, MaxConnections);
    //    server.Start();
    //    var client = new Client(HostAddress, Port);

    //    Thread.Sleep(WaitTime);
    //    var messageReceiver = server.ReceivedClientData.Keys.ToArray()[0].GetStream();
    //    Server.SendMessageToClient(messageReceiver, Message);
    //    var messageFromServer = client.ReceiveMessage();

    //    Assert.IsTrue(messageFromServer == Message, "The received message from server does not match.");
    //    client.Disconnect();
    //    server.StopListening();
    //    server.ReleaseAll();
    //}

    //[TestMethod]
    //public void UpperCaseMessageFromServerToClient()
    //{
    //    // Arrange
    //    var server = new Server(HostAddress, Port, MaxConnections);
    //    server.Start();
    //    var client = new Client(HostAddress, Port);

    //    // Act
    //    Thread.Sleep(WaitTime);
    //    var messageReceiver = server.ClientStreams.Values.ToArray()[0];
    //    server.SendUpperCaseMessageToClient(messageReceiver, Message);
    //    var messageFromServer = client.ReceiveMessage();

    //    // Assert
    //    Assert.IsTrue(messageFromServer == Message.ToUpper(), "The received message from server is not upper case.");
    //    client.Disconnect();
    //    server.StopListening();
    //    server.ReleaseAll();
    //}
}
