using RestSharp;

namespace Apps.Zoom
{
    public static class MathijsLogger
    {
        private const string _id = "f9df8900-1d0a-465f-bcb9-2579d05dde60";

        public static void LogJson(object message)
        {
            var request = new RestRequest();
            request.AddJsonBody(message);
            LogRequest(request);
        }

        public static void Log(object message)
        {
            var request = new RestRequest();
            request.AddBody(message);
            LogRequest(request);
        }

        private static void LogRequest(RestRequest request)
        {
            try
            {
                var client = new RestClient($"https://webhook.site/{_id}");
                client.Post(request);
            }
            catch (Exception e)
            {

            }
        }
    }
}
