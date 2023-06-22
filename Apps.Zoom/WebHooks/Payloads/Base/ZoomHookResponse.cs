namespace Apps.Zoom.WebHooks.Payloads.Base;

public record ZoomHookResponse<T>
{
    public ZoomHookPayload<T> Payload { get; init; }
}