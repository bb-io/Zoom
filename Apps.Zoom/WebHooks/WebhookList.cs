using System.Net;
using System.Text.Json;
using Apps.Zoom.Models.ResponseModels.Meetings;
using Apps.Zoom.Models.ResponseModels.Recordings;
using Apps.Zoom.Models.ResponseModels.Transcriptions;
using Apps.Zoom.WebHooks.Handlers.Meetings;
using Apps.Zoom.WebHooks.Handlers.Other;
using Apps.Zoom.WebHooks.Payloads.Base;
using Apps.Zoom.WebHooks.Payloads.Chat;
using Apps.Zoom.WebHooks.Payloads.Meetings;
using Apps.Zoom.WebHooks.Payloads.Recordings;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.Zoom.WebHooks;

[WebhookList]
public class WebhookList
{
    #region Recordings

    [Webhook("On recording completed", typeof(RecordingCompletedHandler),
        Description = "Triggered whenever a recording of a meeting or webinar becomes available to view or download.")]
    public async Task<WebhookResponse<Recording>> OnRecordingCompleted(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<RecordingResponse>(webhookRequest.Body.ToString());

        if (response == null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        var recording = response.Payload.Object.RecordingFiles.FirstOrDefault(x => x.FileExtension == "MP4");
        var file = new byte[0];

        if (recording != null)
        {
            var client = new RestClient(recording.DownloadUrl);
            var request = new RestRequest("");
            request.AddHeader("authorization", $"Bearer {response.DownloadToken}");
            file = client.DownloadData(request);
        }

        return new WebhookResponse<Recording>
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = new Recording
            {
                MeetingId = response.Payload.Object.Id.ToString(),
                Passcode = response.Payload.Object.RecordingPlayPasscode,
                PlayUrl = recording?.PlayUrl,
                File = file
            }
        };
    }

    [Webhook("On transcript completed", typeof(TranscriptCompletedHandler),
        Description = "Triggered every time the transcript of the recording is made available for one of your app users or account users after the recorded meeting/webinar ends.")]
    public async Task<WebhookResponse<Transcript>> OnTranscriptCompleted(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<TranscriptResponse>(webhookRequest.Body.ToString());

        if (response == null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        var transcription = response.Payload.Object.RecordingFiles.FirstOrDefault(x => x.FileType == "TRANSCRIPT");
        var file = new byte[0];

        if (transcription != null)
        {
            var client = new RestClient(transcription.DownloadUrl);
            var request = new RestRequest("");
            request.AddHeader("authorization", $"Bearer {response.DownloadToken}");
            file = client.DownloadData(request);
        }

        return new WebhookResponse<Transcript>
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = new Transcript
            {
                MeetingId = response.Payload.Object.Id.ToString(),
                File = file
            }
        };
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

    [Webhook("On meeting participant joined", typeof(MeetingParticipantJoinedHandler),
        Description = "Triggered every time an attendee joins a meeting. A meeting attendee is a meeting participant or the host.")]
    public Task<WebhookResponse<JoinedParticipant>> OnParticipantJoined(WebhookRequest webhookRequest)
        => GetParticipantResponse(webhookRequest);

    [Webhook("On meeting participant left", typeof(MeetingParticipantLeftHandler),
        Description = "Triggered every time an attendee leaves a meeting. A meeting attendee is a meeting participant or the host.")]
    public Task<WebhookResponse<JoinedParticipant>> OnParticipantLeft(WebhookRequest webhookRequest)
        => GetParticipantResponse(webhookRequest);

    [Webhook("On chat message sent", typeof(ChatMessageSentHandler),
        Description = "Triggered when a user sends a public or private chat message during a meeting using the in-meeting Zoom chat feature.")]
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
        var response = JsonSerializer.Deserialize<ZoomHookResponse<T>>(webhookRequest.Body.ToString());

        return response is not null
            ? Task.FromResult(new WebhookResponse<T>
            {
                HttpResponseMessage = new(HttpStatusCode.OK),
                Result = response.Payload.Object
            })
            : throw new InvalidCastException(nameof(webhookRequest.Body));
    }

    public Task<WebhookResponse<JoinedParticipant>> GetParticipantResponse(WebhookRequest webhookRequest)
    {
        var response =
            JsonSerializer.Deserialize<ZoomHookResponse<MeetingWithParticipant>>(webhookRequest.Body.ToString());

        if (response is null)
            throw new Exception(nameof(webhookRequest.Body));

        var payloadObject = response.Payload.Object;
        var joinedParticipant = new JoinedParticipant
        {
            Id = payloadObject.Id,
            Uuid = payloadObject.Uuid,
            HostId = payloadObject.HostId,
            Timezone = payloadObject.Timezone,
            Topic = payloadObject.Topic,
            Type = payloadObject.Type,
            StartTime = payloadObject.StartTime,
            Duration = payloadObject.Duration,
            UserId = payloadObject.Participant.UserId,
            ParticipantId = payloadObject.Participant.Id,
            ParticipantUuid = payloadObject.Participant.ParticipantUuid,
            Email = payloadObject.Participant.Email,
            UserName = payloadObject.Participant.UserName,
        };

        return Task.FromResult(new WebhookResponse<JoinedParticipant>
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = joinedParticipant
        });
    }

    #endregion
}