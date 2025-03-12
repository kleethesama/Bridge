using System.Text.Json.Serialization;

namespace TCP_Server.JsonObjects;

public class JsonProtocolObj
{
    [JsonPropertyName("method")]
    public string? Method { get; set; }
    [JsonPropertyName("arg1")]
    public int Arg1 { get; set; }
    [JsonPropertyName("arg2")]
    public int Arg2 { get; set; }
}
