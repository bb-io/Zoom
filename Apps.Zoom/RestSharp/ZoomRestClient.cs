using System.Text.Json;
using Apps.Zoom.Models.ResponseModels;
using RestSharp;

namespace Apps.Zoom.RestSharp;

public class ZoomRestClient : RestClient
{
    public async Task<T> ExecuteWithErrorHandling<T>(ZoomRequest request)
    {
        var response = await ExecuteWithErrorHandling(request);
        return JsonSerializer.Deserialize<T>(response.Content);
    }    
    
    public async Task<RestResponse> ExecuteWithErrorHandling(ZoomRequest request)
    {
        var response = await ExecuteAsync(request);

        if (response.IsSuccessful)
            return response;

        throw GetWrongRequestException(response.Content);
    }

    private Exception GetWrongRequestException(string content)
    {
        var errorModel = JsonSerializer.Deserialize<ErrorResponse>(content);
        return new Exception(errorModel.Message);
    }
}