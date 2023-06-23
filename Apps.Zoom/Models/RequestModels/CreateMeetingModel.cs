using System.Text.Json.Serialization;

namespace Apps.Zoom.Models.RequestModels;

public record CreateMeetingModel
{
    [JsonPropertyName("start_time")]
    public string StartDate { get; init; }
    
    [JsonPropertyName("topic")]
    public string MeetingName { get; init; }
}