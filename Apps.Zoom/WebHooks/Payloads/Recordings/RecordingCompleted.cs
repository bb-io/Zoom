using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Recordings;

public record RecordingCompleted
{
    [JsonPropertyName("recording_files")]
    public List<RecordingFile> RecordingFiles { get; init; }
    
    [JsonPropertyName("file_type")]
    public string FileType { get; init; }
}