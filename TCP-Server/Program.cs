using TCP_Server;

internal class Program
{
    private static void Main(string[] args)
    {
        var myServer = new ServerWithProtocol(7, 3);
        myServer.Run();
    }
}