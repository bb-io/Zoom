using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Meetings
{
    public class ChatMessageSentHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "meeting.chat_message_sent";
        public ChatMessageSentHandler() : base(SubscriptionEvent) { }
    }
}
