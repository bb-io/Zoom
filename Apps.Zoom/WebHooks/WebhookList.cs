using System.Net;
using System.Text.Json;
using Apps.Zoom.Models.ResponseModels.Meetings;
using Apps.Zoom.WebHooks.Handlers.Meetings;
using Apps.Zoom.WebHooks.Payloads.Base;
using Apps.Zoom.WebHooks.Payloads.Chat;
using Apps.Zoom.WebHooks.Payloads.Meetings;
using Apps.Zoom.WebHooks.Payloads.Recordings;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Zoom.WebHooks;

[WebhookList]
public class WebhookList
{
    #region Recordings

    [Webhook("On recording completed",
        Description = "On recording of a meeting or webinar becomes available to view or download")]
    public Task<WebhookResponse<RecordingCompleted>> OnRecordingCompleted(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<RecordingCompleted>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<RecordingCompleted>
            {
                HttpResponseMessage = new(HttpStatusCode.OK),
                Result = response.Payload.Object
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }

    #endregion

    #region Meetings

    [Webhook("On meeting deleted", typeof(MeetingDeletedHandler),
        Description = "Triggered every time a meeting scheduled by one of your app users or account users, is deleted.")]
    public Task<WebhookResponse<Meeting>> OnMeetingDeleted(WebhookRequest webhookRequest)
        => GetMeetingResponse<Meeting>(webhookRequest);

    [Webhook("On meeting ended", typeof(MeetingEndedHandler),
        Description = "Triggered every time a meeting host ends the meeting.")]
    public Task<WebhookResponse<EndedMeeting>> OnMeetingEnded(WebhookRequest webhookRequest)
        => GetMeetingResponse<EndedMeeting>(webhookRequest);

    [Webhook("On meeting started", typeof(MeetingStartedHandler),
        Description = "Triggered every time a meeting host starts a meeting.")]
    public Task<WebhookResponse<StartedMeeting>> OnMeetingStarted(WebhookRequest webhookRequest)
        => GetMeetingResponse<StartedMeeting>(webhookRequest);

    [Webhook("On meeting created", typeof(MeetingCreatedHandler),
    Description = "Triggered every time a meeting is created by one of your app users or account users.")]
    public Task<WebhookResponse<Meeting>> OnMeetingCreated(WebhookRequest webhookRequest)
    => GetMeetingResponse<Meeting>(webhookRequest);

    [Webhook("On meeting participant joined",
        Description = "On an attendee joins a meeting")]
    public Task<WebhookResponse<Participant>> OnParticipantJoined(WebhookRequest webhookRequest)
        => GetParticipantResponse(webhookRequest);

    [Webhook("On meeting participant left",
        Description = "On an attendee leaves a meeting")]
    public Task<WebhookResponse<Participant>> OnParticipantLeft(WebhookRequest webhookRequest)
        => GetParticipantResponse(webhookRequest);

    [Webhook("On chat message sent",
        Description = "On a user sends a public or private chat message during a meeting")]
    public Task<WebhookResponse<ChatMessage>> OnChatMessageSent(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<ChatMessageResponse>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<ChatMessage>
            {
                HttpResponseMessage = new(HttpStatusCode.OK),
                Result = response.Payload.Object.ChatMessage
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }

    #endregion

    #region Utils

    public Task<WebhookResponse<T>> GetMeetingResponse<T>(WebhookRequest webhookRequest) where T : class
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<T>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<T>
            {
                HttpResponseMessage = new(HttpStatusCode.OK),
                Result = response.Payload.Object
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }

    public Task<WebhookResponse<Participant>> GetParticipantResponse(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<ParticipantResponse>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<Participant>
            {
                HttpResponseMessage = new(HttpStatusCode.OK),
                Result = response.Payload.Object.Participant
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }

    #endregion
}