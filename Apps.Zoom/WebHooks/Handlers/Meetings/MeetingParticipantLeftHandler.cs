using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Meetings
{
    public class MeetingParticipantLeftHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "meeting.participant_left";
        public MeetingParticipantLeftHandler() : base(SubscriptionEvent) { }
    }
}
