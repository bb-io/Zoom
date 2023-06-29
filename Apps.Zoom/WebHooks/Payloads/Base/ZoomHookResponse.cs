using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Base;

public record ZoomHookResponse<T>
{
    [JsonPropertyName("payload")]
    public ZoomHookPayload<T> Payload { get; init; }
}