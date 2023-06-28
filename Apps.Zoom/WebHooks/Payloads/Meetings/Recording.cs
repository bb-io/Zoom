using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks.Payloads.Meetings
{
    public class Recording
    {
        [Display("Meeting ID")]
        public string MeetingId { get; set; }

        [Display("File")]
        public byte[] File { get; set; }

        [Display("Play URL")]
        public string PlayUrl { get; set; }

        [Display("Passcode")]
        public string Passcode { get; set; }
    }
}
