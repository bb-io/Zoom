using System.Text.Json.Serialization;

namespace Apps.Zoom.Models.ResponseModels;

public record ErrorResponse
{
    [JsonPropertyName("message")]
    public string Message { get; init; }
}