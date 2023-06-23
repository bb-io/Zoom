using System.Text.Json.Serialization;
using Apps.Zoom.Models.ResponseModels.Base;

namespace Apps.Zoom.Models.ResponseModels.Meetings;

public record MeetingsResponse : PaginatedResponse
{
    [JsonPropertyName("meetings")] public List<Meeting> Meetings { get; init; }
}