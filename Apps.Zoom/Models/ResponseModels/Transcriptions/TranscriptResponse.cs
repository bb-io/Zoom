using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apps.Zoom.Models.ResponseModels.Transcriptions
{
    public class Object
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("host_id")]
        public string HostId { get; set; }

        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("host_email")]
        public string HostEmail { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("share_url")]
        public string ShareUrl { get; set; }

        [JsonPropertyName("total_size")]
        public int TotalSize { get; set; }

        [JsonPropertyName("recording_count")]
        public int RecordingCount { get; set; }

        [JsonPropertyName("recording_files")]
        public List<RecordingFile> RecordingFiles { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("object")]
        public Object Object { get; set; }
    }

    public class RecordingFile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meeting_id")]
        public string MeetingId { get; set; }

        [JsonPropertyName("recording_start")]
        public DateTime RecordingStart { get; set; }

        [JsonPropertyName("recording_end")]
        public DateTime RecordingEnd { get; set; }

        [JsonPropertyName("file_type")]
        public string FileType { get; set; }

        [JsonPropertyName("file_size")]
        public int FileSize { get; set; }

        [JsonPropertyName("play_url")]
        public string PlayUrl { get; set; }

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("recording_type")]
        public string RecordingType { get; set; }
    }

    public class TranscriptResponse
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("event_ts")]
        public long EventTs { get; set; }

        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }

        [JsonPropertyName("download_token")]
        public string DownloadToken { get; set; }
    }
}
