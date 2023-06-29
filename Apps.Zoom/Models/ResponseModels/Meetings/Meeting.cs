using Blackbird.Applications.Sdk.Common;
using System.Text.Json.Serialization;

namespace Apps.Zoom.Models.ResponseModels.Meetings;

public record Meeting
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("topic")]
    public string Topic { get; init; }
    
    [JsonPropertyName("join_url")]
    [Display("Join URL")]
    public string JoinUrl { get; init; }
    
    [JsonPropertyName("password")]
    public string Password { get; init; }
}