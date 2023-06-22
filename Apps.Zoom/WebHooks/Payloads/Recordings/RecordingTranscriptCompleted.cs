using System.Text.Json.Serialization;

namespace Apps.Zoom.WebHooks.Payloads.Recordings;

public record RecordingTranscriptCompleted
{
    [JsonPropertyName("recording_files")]
    public List<RecordingFile> RecordingFiles { get; init; }
}