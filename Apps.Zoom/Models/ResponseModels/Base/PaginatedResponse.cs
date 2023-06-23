using System.Text.Json.Serialization;

namespace Apps.Zoom.Models.ResponseModels.Base;

public record PaginatedResponse
{
    [JsonPropertyName("next_page_token")]
    public string NextToken { get; init; }
}