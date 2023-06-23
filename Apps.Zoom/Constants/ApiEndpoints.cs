namespace Apps.Zoom.Constants;

public class ApiEndpoints
{
    public const string AuthorizeEndpoint = "/authorize";
    public const string TokenEndpoint = "/token";
    public const string RevokeTokenEndpoint = "/revoke";
    public const string MeEndpoint = "/users/me";
    public const string MeetingsEndpoint =  "/meetings";
    public const string UserMeetingsEndpoint = MeEndpoint + MeetingsEndpoint;
}