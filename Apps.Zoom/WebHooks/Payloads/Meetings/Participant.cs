using Blackbird.Applications.Sdk.Common;
using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Meetings;

public record Participant
{
    [JsonPropertyName("user_id")]
    public string UserId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("participant_uuid")]
    public string ParticipantUuid { get; init; }

    [JsonPropertyName("user_name")]
    public string UserName { get; init; }
    
    [JsonPropertyName("email")]
    public string Email { get; init; }

    [JsonPropertyName("date_time")]
    public DateTime DateTime { get; init; }


}