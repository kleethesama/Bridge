using System.Text.Json;
using System.Text.Json.Serialization;
using TCP_Server.JsonObjects;

namespace TCP_Server;

public class SimpleJSONProtocol : SimpleProtocol
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    public ProtocolCommand? ProtocolCommand { get; private set; }

    public SimpleJSONProtocol(byte expectedArgsCount) : base(expectedArgsCount)
    {
        _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };
    }

    protected override async Task<string> RunProtocol(string jsonString)
    {
        return await Task.Run(() =>
        {
            try
            {
                ProtocolCommand = DeserializeToCommandObject(jsonString);
            }
            catch (JsonException ex)
            {
                return $"There is an error with the format: {ex.Message}";
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
    }

    private ProtocolCommand? DeserializeToCommandObject(string jsonString)
    {
        return JsonSerializer.Deserialize<ProtocolCommand>(jsonString, _jsonSerializerOptions);
    }
}
