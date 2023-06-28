using Blackbird.Applications.Sdk.Common;
using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Chat;

public record ChatMessage
{
    [JsonPropertyName("sender_email")]
    [Display("Sender email")]
    public string SenderEmail { get; init; }

    [JsonPropertyName("sender_type")]
    [Display("Sender type")]
    public string SenderType { get; init; }

    [JsonPropertyName("message_content")]
    [Display("Message")]
    public string MessageContent { get; init; }

    [JsonPropertyName("message_id")]
    [Display("Message ID")]
    public string MessageId { get; init; }
}