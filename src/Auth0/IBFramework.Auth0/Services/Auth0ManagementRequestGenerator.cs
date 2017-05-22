using IBFramework.Auth0.Core.Providers;
using IBFramework.Auth0.Core.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IBFramework.Auth0.Services
{
    public class Auth0ManagementRequestGenerator : IAuth0ManagementRequestGenerator
    {
        #region Variables & Constants

        private readonly IAuth0ConfigurationProvider _configProvider;
        private readonly IUserProvider _userProvider;

        #endregion

        #region Constructor

        public Auth0ManagementRequestGenerator(
            IAuth0ConfigurationProvider configProvider,
            IUserProvider userProvider)
        {
            _configProvider = configProvider;
            _userProvider = userProvider;
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateManagementApiTokenRequest()
        {
            // Setup Request Base
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://{_configProvider.Domain}/oauth/token"),
                Method = HttpMethod.Post
            };

            req.Headers.Add("accept", "application/json");

            // Setup Content
            var tokenReqBody = new Auth0ApiTokenRequest
            {
                client_id = _configProvider.ApiClientId,
                client_secret = _configProvider.ApiClientSecret,

                audience = $"https://{_configProvider.Domain}/api/v2/",
                grant_type = "client_credentials"
            };

            var tokenBodyString = JsonConvert.SerializeObject(tokenReqBody);

            req.Content = new StringContent(tokenBodyString, Encoding.UTF8, "application/json");

            return req;
        }

        public HttpRequestMessage GenerateVerifyEmailRequest(string managementToken)
        {
            // Setup our Email Verification Request
            var verifyEmail = new Auth0VerifyEmailModel
            {
                user_id = _userProvider.AuthenticationId,
                client_id = _configProvider.SpaClientId
            };

            var stringContent = JsonConvert.SerializeObject(verifyEmail);

            var req = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://{_configProvider.Domain}/api/v2/jobs/verification-email"),
                Content = new StringContent(stringContent, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
            };

            req.Headers.Add("accept", "application/json");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", managementToken);

            return req;
        }

        #endregion
    }
}
