using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Chat;

public record ChatMessage
{
    [JsonPropertyName("date_time")]
    public string DateTime { get; init; }
    [JsonPropertyName("sender_email")]
    public string SenderEmail { get; init; }
    [JsonPropertyName("message_content")]
    public string MessageContent { get; init; }
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; }
}