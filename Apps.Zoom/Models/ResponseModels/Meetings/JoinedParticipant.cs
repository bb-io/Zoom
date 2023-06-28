using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apps.Zoom.Models.ResponseModels.Meetings
{
    public class JoinedParticipant
    {
        [Display("Meeting ID")]
        public string Id { get; init; }

        [Display("Meeting UUID")]
        public string Uuid { get; init; }

        [Display("Host ID")]
        public string HostId { get; init; }

        public string Topic { get; init; }

        [Display("Meeting type")]
        public int Type { get; init; }

        [Display("Start time")]
        public DateTime StartTime { get; init; }

        public string Timezone { get; init; }

        public int Duration { get; init; }

        [Display("Participant user ID")]
        public string UserId { get; init; }

        [Display("Participant ID")]
        public string ParticipantId { get; init; }

        [Display("Participant UUID")]
        public string ParticipantUuid { get; init; }

        [Display("Participant username")]
        public string UserName { get; init; }

        [Display("Participant email")]
        public string Email { get; init; }
    }
}
