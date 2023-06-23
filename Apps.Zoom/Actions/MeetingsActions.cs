using Apps.Zoom.Constants;
using Apps.Zoom.Models.RequestModels;
using Apps.Zoom.Models.ResponseModels.Base;
using Apps.Zoom.Models.ResponseModels.Meetings;
using Apps.Zoom.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using RestSharp;

namespace Apps.Zoom.Actions;

[ActionList]
public class MeetingsActions
{
    private readonly ZoomRestClient _restClient;

    public MeetingsActions()
    {
        _restClient = new();
    }

    #region Get

    [Action("List live meetings", Description = "List all the ongoing meetings")]
    public async Task<List<Meeting>> ListLiveMeetings(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
    {
        var baseUrl = QueryHelpers.AddQueryString(UrlConstants.ZoomApiUrl + ApiEndpoints.UserMeetingsEndpoint,
            new Dictionary<string, string>
            {
                { "type", "live" }
            });

        var resultItems = await Paginate<MeetingsResponse>(baseUrl, authenticationCredentialsProviders);

        return resultItems.SelectMany(x => x.Meetings).ToList();
    }

    #endregion

    #region Post

    [Action("Create a meeting", Description = "Create a meeting for a user")]
    public Task<Meeting> CreateMeeting(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DateTime startDate,
        [ActionParameter] string meetingName)
    {
        var baseUrl = UrlConstants.ZoomApiUrl + ApiEndpoints.UserMeetingsEndpoint;

        var request = new ZoomRequest(baseUrl, Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(new CreateMeetingModel()
        {
            StartDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            MeetingName = meetingName,
        });

        return _restClient.ExecuteWithErrorHandling<Meeting>(request);
    }

    #endregion

    #region Put

    [Action("End a meeting", Description = "End a meeting")]
    public async Task EndMeeting(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] long meetingId)
    {
        var baseUrl = $"{UrlConstants.ZoomApiUrl}{ApiEndpoints.MeetingsEndpoint}/{meetingId}/status";

        var request = new ZoomRequest(baseUrl, Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new ManageMeetingModel("end"));

        await _restClient.ExecuteWithErrorHandling(request);
    }

    #endregion

    #region Utils

    private async Task<List<T>> Paginate<T>(string baseUrl,
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) where T : PaginatedResponse
    {
        var results = new List<T>();
        var nextToken = string.Empty;

        do
        {
            var requestUrl = QueryHelpers.AddQueryString(baseUrl, new Dictionary<string, string>()
            {
                { "next_page_token", nextToken }
            });

            var request = new ZoomRequest(requestUrl, Method.Get, authenticationCredentialsProviders);
            var jsonData = await _restClient.ExecuteWithErrorHandling<T>(request);

            nextToken = jsonData.NextToken;
            results.Add(jsonData);
        } while (!string.IsNullOrEmpty(nextToken));

        return results;
    }

    #endregion
}