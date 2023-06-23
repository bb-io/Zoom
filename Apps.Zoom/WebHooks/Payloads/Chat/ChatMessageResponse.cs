using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Chat;

public record ChatMessageResponse
{
    [JsonPropertyName("chat_message")] public ChatMessage ChatMessage { get; init; }
}