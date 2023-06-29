using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Base;

public record ZoomHookPayload<T>
{
    [JsonPropertyName("object")]
    public T Object { get; init; }
}