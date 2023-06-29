using System.Text;
using System.Text.Json;
using Apps.Zoom.Constants;
using Apps.Zoom.Models;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using RestSharp;

namespace Apps.Zoom.Connections.OAuth2;

public class OAuth2TokenService : IOAuth2TokenService
{
    #region Fields

    private readonly RestClient _restClient;

    #endregion

    #region Constructors

    public OAuth2TokenService()
    {
        _restClient = new();
    }

    #endregion

    #region TokenMethods

    public async Task<Dictionary<string, string>> RequestToken(string state, string code, Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        var requestUrl = UrlConstants.ZoomOauthUrl + ApiEndpoints.TokenEndpoint;
        var devCreds = new DeveloperCredentials(ApplicationConstants.ClientId, ApplicationConstants.ClientSecret);
        
        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "redirect_uri", ApplicationConstants.RedirectUri },
            { "code", code }
        };
        return await GetAuthData(requestUrl, devCreds, bodyParameters, cancellationToken);
    }

    public Task<Dictionary<string, string>> RefreshToken(Dictionary<string, string> values,
        CancellationToken cancellationToken)
    {
        var requestUrl = UrlConstants.ZoomOauthUrl + ApiEndpoints.TokenEndpoint;
        var devCreds = new DeveloperCredentials(ApplicationConstants.ClientId, ApplicationConstants.ClientSecret);

        var bodyParameters = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", values["refresh_token"] },
        };

        return GetAuthData(requestUrl, devCreds, bodyParameters, cancellationToken);
    }

    public Task RevokeToken(Dictionary<string, string> values)
    {
        var requestUrl = UrlConstants.ZoomOauthUrl + ApiEndpoints.RevokeTokenEndpoint;
        var devCreds = new DeveloperCredentials(ApplicationConstants.ClientId, ApplicationConstants.ClientSecret);

        var bodyParameters = new Dictionary<string, string>
        {
            { "token", values["access_token"] },
        };

        return SendTokenRequest(requestUrl, devCreds, bodyParameters, CancellationToken.None);
    }

    public bool IsRefreshToken(Dictionary<string, string> values) => true;

    #endregion

    #region Utils

    private Task<RestResponse> SendTokenRequest(string requestUrl,
        DeveloperCredentials developerCredentials,
        Dictionary<string, string> bodyParameters, CancellationToken cancellationToken)
    {
        var request = new RestRequest(requestUrl, Method.Post);
        request.AddHeader("Authorization", GetBasicAuthHeader(developerCredentials));
        bodyParameters.ToList().ForEach(x => request.AddParameter(x.Key, x.Value));

        return _restClient.ExecuteAsync(request, cancellationToken);
    }

    private async Task<Dictionary<string, string>> GetAuthData(string requestUrl,
        DeveloperCredentials developerCredentials,
        Dictionary<string, string> bodyParameters, CancellationToken cancellationToken)
    {
        var response = await SendTokenRequest(requestUrl, developerCredentials, bodyParameters, cancellationToken);
        var content = response.Content;

        var deserializedData = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

        return deserializedData
            .ToDictionary(r => r.Key, r => r.Value.ToString());
    }

    private string GetBasicAuthHeader(DeveloperCredentials developerCredentials)
    {
        var appCredentials = Encoding.ASCII.GetBytes(
            $"{developerCredentials.ClientId}:{developerCredentials.ClientSecret}");

        return $"Basic {Convert.ToBase64String(appCredentials)}";
    }

    #endregion
}