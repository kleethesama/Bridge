using System.Text.Json.Serialization;

namespace TCP_Server.JsonObjects;

// Used for binding Json data from client
// to be used for the protocol.
public class JsonProtocolObj
{
    [JsonPropertyName("method")]
    public string? Method { get; set; }
    [JsonPropertyName("arg1")]
    public int Arg1 { get; set; }
    [JsonPropertyName("arg2")]
    public int Arg2 { get; set; }
}
