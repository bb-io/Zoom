using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Zoom.WebHooks
{
    public class BridgeService
    {
        internal string AccountId { get; set; }

        public BridgeService(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var token = authenticationCredentialsProviders.First(p => p.KeyName == "access_token").Value;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            MathijsLogger.LogJson(jsonToken.Claims);
            var accountId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "accountId")?.Value;

            if (accountId == null) throw new Exception("Could not extract accountId from JWT token");
            AccountId = accountId;

        }
        public void Subscribe(string _event, string url)
        {
            var client = new RestClient(ApplicationConstants.BridgeUrl);
            var request = new RestRequest($"/{AccountId}/{_event}", Method.Post);
            request.AddBody(url);
            client.Execute(request);
        }

        public void Unsubscribe(string _event)
        {
            var client = new RestClient(ApplicationConstants.BridgeUrl);
            var request = new RestRequest($"/{AccountId}/{_event}", Method.Delete);
            client.Execute(request);
        }
    }
}
