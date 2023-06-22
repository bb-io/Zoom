namespace Apps.Zoom.WebHooks.Payloads.Base;

public record ZoomHookPayload<T>
{
    public T Object { get; init; }
}