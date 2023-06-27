using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apps.Zoom.Models.ResponseModels.Meetings
{
    public class EndedMeeting
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; init; }

        [JsonPropertyName("host_id")]
        [Display("Host ID")]
        public string HostId { get; init; }

        [JsonPropertyName("topic")]
        public string Topic { get; init; }

        [JsonPropertyName("type")]
        public int Type { get; init; }

        [JsonPropertyName("start_time")]
        [Display("Start time")]
        public DateTime StartTime { get; init; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; init; }

        [JsonPropertyName("duration")]
        public int Duration { get; init; }

        [JsonPropertyName("end_time")]
        [Display("End time")]
        public DateTime EndTime { get; init; }
    }
}
