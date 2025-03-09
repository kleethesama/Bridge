using TCP_Server;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = "50245 512";
        var protocol = new SimpleProtocol("ranDOM", 2);
        //var result = protocol.ExecuteCommand(1, 2);
        //var argPoses = protocol.FindArgumentSeperationPositions(data);
        //var result = protocol.ParseAllDataArguments(data);
        //var result = protocol.ParseAllDataArguments("50253252 6665252");

        //foreach (var position in result)
        //{
        //    Console.WriteLine(position);
        //}

        //var myServer = new Server("127.0.0.1", 7, 3);
        //myServer.Start();
    }
}