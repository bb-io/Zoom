using RestSharp;

namespace Apps.Zoom
{
    public static class MathijsLogger
    {
        private const string _id = "f2b689f2-0ec2-4e42-a72a-f07193c25902";

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
