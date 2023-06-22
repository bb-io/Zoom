using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Recordings;

public record RecordingFile
{
    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; init; }
}