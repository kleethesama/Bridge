using System.Text.Json;
using TCP_Server.JsonObjects;

namespace TCP_Server;

public class SimpleJSONProtocol : SimpleProtocol
{
    public ProtocolCommand? ProtocolCommand { get; private set; }

    public SimpleJSONProtocol(byte expectedArgsCount) : base(expectedArgsCount) { }

    protected override async Task<string> RunProtocol(string jsonString)
    {
        //
        return await Task.Run(() =>
        {
            var ProtocolCommand = DeserializeToCommandObject(jsonString);
            if (ProtocolCommand is null)
            {
                return "There is an error with the format.";
            }

            // Parse command from server.
            short commandType = ParseCommandType(ProtocolCommand.Method, typeof(CommandType));
            if (!IsCommandValid(commandType))
            {
                return "Method is not valid. Please, try again.";
            }
            SelectCommand(commandType);

            // Handle args and perform command execution.
            int[] argValues = [ProtocolCommand.Arg1, ProtocolCommand.Arg2];
            int executionValue = ExecuteCommand(argValues[0], argValues[1]);

            // Return value to server.
            return executionValue.ToString();
        });
        //// Parse command from server.
        //short commandType = ParseCommandType(ProtocolCommand.Method, typeof(CommandType));
        //if (!IsCommandValid(commandType))
        //{
        //    return "Command is not valid. Please, try again.";
        //}
        //SelectCommand(commandType);

        //// Handle args and perform command execution.
        //int[] argValues = [ProtocolCommand.Arg1, ProtocolCommand.Arg2];
        //int executionValue = ExecuteCommand(argValues[0], argValues[1]);

        //// Return value to server.
        //return executionValue.ToString();
    }

    private static ProtocolCommand? DeserializeToCommandObject(string jsonString)
    {
        return JsonSerializer.Deserialize<ProtocolCommand>(jsonString);
    }
}
