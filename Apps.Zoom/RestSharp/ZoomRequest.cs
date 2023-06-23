using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Zoom.RestSharp;

public class ZoomRequest : RestRequest
{
    public ZoomRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
    {
        var token = authenticationCredentialsProviders.First(p => p.KeyName == "access_token").Value;
        this.AddHeader("Authorization", $"Bearer {token}");
    }
}