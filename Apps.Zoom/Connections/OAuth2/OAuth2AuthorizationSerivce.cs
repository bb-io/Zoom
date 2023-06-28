using Apps.Zoom.Constants;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Microsoft.AspNetCore.WebUtilities;

namespace Apps.Zoom.Connections.OAuth2;

public class OAuth2AuthorizationSerivce : IOAuth2AuthorizeService
{
    public string GetAuthorizationUrl(Dictionary<string, string> values)
    {
        var authorizeUrl = UrlConstants.ZoomOauthUrl + ApiEndpoints.AuthorizeEndpoint;
        
        var parameters = new Dictionary<string, string>
        {
            { "response_type", "code" },
            { "client_id", ApplicationConstants.ClientId },
            { "redirect_uri", ApplicationConstants.RedirectUri },
            { "state", values["state"] }
        };
        
        return QueryHelpers.AddQueryString(authorizeUrl, parameters);
    }
}