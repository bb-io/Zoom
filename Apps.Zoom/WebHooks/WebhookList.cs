using System.Text.Json;
using Apps.Zoom.WebHooks.Payloads.Base;
using Apps.Zoom.WebHooks.Payloads.Recordings;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Zoom.WebHooks;

[WebhookList]
public class WebhookList
{
    [Webhook("On recording transcript completed", Description = "On recording transcript files have completed")]
    public Task<WebhookResponse<RecordingTranscriptCompleted>> OnRecordingTranscriptCompleted(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<RecordingTranscriptCompleted>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<RecordingTranscriptCompleted>
            {
                HttpResponseMessage = null,
                Result = response.Payload.Object
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }
}