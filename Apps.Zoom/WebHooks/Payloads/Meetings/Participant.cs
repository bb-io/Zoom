using Blackbird.Applications.Sdk.Common;
using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Meetings;

public record Participant
{
    [JsonPropertyName("user_id")]
    [Display("Participant user ID")]
    public string UserId { get; init; }

    [JsonPropertyName("id")]
    [Display("Participant ID")]
    public string Id { get; init; }

    [JsonPropertyName("participant_uuid")]
    [Display("Participant UUID")]
    public string ParticipantUuid { get; init; }

    [JsonPropertyName("user_name")]
    [Display("Participant username")]
    public string UserName { get; init; }
    
    [JsonPropertyName("email")]
    [Display("Participant email")]
    public string Email { get; init; }

    [JsonPropertyName("date_time")]
    [Display("Date")]
    public DateTime DateTime { get; init; }


}