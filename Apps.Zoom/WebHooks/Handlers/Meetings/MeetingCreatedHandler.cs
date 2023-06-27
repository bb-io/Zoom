using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Meetings
{
    public class MeetingCreatedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "meeting.created";
        public MeetingCreatedHandler() : base(SubscriptionEvent) { }
    }
}
