using TCP_Server.Base_classes;

namespace TCP_Server;

public class ServerWithProtocol : Server
{
    private readonly Protocol _protocol;

    public ServerWithProtocol(int port, int maxConnections, Protocol protocol) : base(port, maxConnections)
    {
        _protocol = protocol;
    }

    public ServerWithProtocol(string host, int port, int maxConnections, Protocol protocol) : base (host, port, maxConnections)
    {
        _protocol = protocol;
    }
}
