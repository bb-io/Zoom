using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Meetings
{
    public class MeetingParticipantJoined : BaseWebhookHandler
    {
        const string SubscriptionEvent = "meeting.participant_joined";
        public MeetingParticipantJoined() : base(SubscriptionEvent) { }
    }
}
