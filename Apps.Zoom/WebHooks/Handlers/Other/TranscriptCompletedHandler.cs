using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Other
{
    public class TranscriptCompletedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "recording.transcript_completed";
        public TranscriptCompletedHandler() : base(SubscriptionEvent) { }
    }
}
