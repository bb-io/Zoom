﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Handlers.Other
{
    public class RecordingCompletedHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "recording.completed";
        public RecordingCompletedHandler() : base(SubscriptionEvent) { }
    }
}
