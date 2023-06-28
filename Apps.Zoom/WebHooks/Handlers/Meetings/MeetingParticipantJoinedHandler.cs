using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Meetings
{
    public class MeetingParticipantJoinedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "meeting.participant_joined";
        public MeetingParticipantJoinedHandler() : base(SubscriptionEvent) { }
    }
}
