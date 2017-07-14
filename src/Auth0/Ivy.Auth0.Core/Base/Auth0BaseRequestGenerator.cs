using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Providers;
using Ivy.Web.Core.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ivy.Auth0.Core.Base
{
    public class Auth0BaseRequestGenerator
    {
        #region Variables & Constants

        protected readonly IAuth0GenericConfigurationProvider _config;
        protected readonly IAuth0ApiConfigurationProvider _apiConfig;
        protected readonly IJsonSerializationService _serializationService;

        #endregion

        #region Constructor

        public Auth0BaseRequestGenerator(
            IAuth0GenericConfigurationProvider config,
            IAuth0ApiConfigurationProvider apiConfig,
            IJsonSerializationService serializationService)
        {
            _config = config;
            _apiConfig = apiConfig;
            _serializationService = serializationService;
        }

        #endregion

        #region Helper Methods

        protected HttpRequestMessage GenerateBaseTokenRequest(string audience)
        {
            // Setup Request Base
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://{_config.Domain}/oauth/token"),
                Method = HttpMethod.Post
            };

            AppendAcceptJsonHeader(req);

            // Setup Content
            var tokenReqBody = new Auth0ApiTokenRequest
            {
                client_id = _apiConfig.ApiClientId,
                client_secret = _apiConfig.ApiClientSecret,

                audience = audience,
                grant_type = "client_credentials"
            };

            AppendStringContent(req, tokenReqBody);

            return req;
        }

        protected HttpRequestMessage SetupAuthorizedRequest(Uri uri, HttpMethod method, string token)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = method
            };

            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            AppendAcceptJsonHeader(req);

            return req;
        }

        protected void AppendStringContent<T>(HttpRequestMessage req, T data)
        {
            var stringContent = _serializationService.Serialize(data);
            AppendStringContent(req, stringContent);
        }

        protected void AppendStringContent(HttpRequestMessage req, string data)
        {
            req.Content = new StringContent(data, Encoding.UTF8, "application/json");
        }

        protected void AppendAcceptJsonHeader(HttpRequestMessage req)
        {
            req.Headers.Add("accept", "application/json");
        }


        private HttpMethod _patchMethod;
        protected HttpMethod PatchMethod
        {
            get
            {
                if (_patchMethod == null)
                {
                    _patchMethod = new HttpMethod("PATCH");
                }

                return _patchMethod;
            }
        }

        #endregion
    }
}
