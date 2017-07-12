using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Core.Services;
using Ivy.Web.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ivy.Auth0.Services
{
    /*
     * For anybody who is curious, I'm specifically not placing the ApiTokenLoad in here
     * because I don't want to have the token load being used on the same class as the
     * token request generation.  This simplifies the testing process for mocking and 
     * allows for a better separation of concerns internally.
     */

    public class Auth0ManagementRequestGenerator : IAuth0ManagementRequestGenerator
    {
        #region Variables & Constants

        private readonly IAuth0QueryStringUriGenerator _queryStringGenerator;
        private readonly IJsonSerializationService _serializationService;
        private readonly IAuth0ConfigurationProvider _configProvider;
        private readonly IUserProvider _userProvider;

        private readonly IAuth0JsonGenerator _jsonGenerator;

        #endregion

        #region Constructor

        public Auth0ManagementRequestGenerator(
            IAuth0QueryStringUriGenerator queryStringGenerator,
            IJsonSerializationService serializationService,
            IAuth0ConfigurationProvider configProvider,
            IUserProvider userProvider,
            IAuth0JsonGenerator jsonGenerator)
        {
            _queryStringGenerator = queryStringGenerator;
            _serializationService = serializationService;
            _configProvider = configProvider;
            _userProvider = userProvider;

            _jsonGenerator = jsonGenerator;
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

            AppendAcceptJsonHeader(req);

            // Setup Content
            var tokenReqBody = new Auth0ApiTokenRequest
            {
                client_id = _configProvider.ApiClientId,
                client_secret = _configProvider.ApiClientSecret,

                audience = $"https://{_configProvider.Domain}/api/v2/",
                grant_type = "client_credentials"
            };

            AppendStringContent(req, tokenReqBody);

            return req;
        }

        public HttpRequestMessage GenerateVerifyEmailRequest(string managementToken)
        {
            var verifyEmail = new Auth0VerifyEmailModel
            {
                user_id = _userProvider.AuthenticationId,
                client_id = _configProvider.SpaClientId
            };

            var uri = new Uri($"https://{_configProvider.Domain}/api/v2/jobs/verification-email");

            var req = SetupAuthorizedRequest(uri, HttpMethod.Post, managementToken);

            AppendStringContent(req, verifyEmail);

            return req;
        }

        public HttpRequestMessage GenerateListUsersRequest(string managementToken, Auth0ListUsersRequest request)
        {
            var baseUriString = GetBaseUsersUri();

            // Need to set this guy to the connection we use for the application
            // Screw what the user puts, we'll override it regardless to what is correct
            request.Connection = _configProvider.Connection;

            var reqUri = _queryStringGenerator.GenerateGetUsersQueryString(baseUriString, request);

            return SetupAuthorizedRequest(reqUri, HttpMethod.Get, managementToken);
        }

        public HttpRequestMessage GenerateCreateUserRequest(string managementToken, Auth0CreateUserRequest request)
        {
            var uri = new Uri(GetBaseUsersUri());

            // Need to set this guy to the connection we use for the application
            // Screw what the user puts, we'll override it regardless to what is correct
            request.connection = _configProvider.Connection;

            var req = SetupAuthorizedRequest(uri, HttpMethod.Post, managementToken);

            var json = _jsonGenerator.ConfigureCreateUserJson(request);

            AppendStringContent(req, json);

            return req;
        }

        public HttpRequestMessage GenerateGetUserRequest(string managementToken, string userId)
        {
            var uri = new Uri(GetUserIdUri(userId));

            return SetupAuthorizedRequest(uri, HttpMethod.Get, managementToken);
        }

        public HttpRequestMessage GenerateUpdateUserRequest(string managementToken, Auth0UpdateUserRequest request)
        {
            var uri = new Uri(GetUserIdUri(request.user_id));

            // Need to set this guy to the connection we use for the application
            // Screw what the user puts, we'll override it regardless to what is correct
            request.connection = _configProvider.Connection;
            request.client_id = _configProvider.SpaClientId;

            var req = SetupAuthorizedRequest(uri, new HttpMethod("PATCH"), managementToken);

            var json = _jsonGenerator.ConfigureUpdateUserJson(request);

            AppendStringContent(req, json);

            return req;
        }

        public HttpRequestMessage GenerateDeleteUserRequest(string managementToken, string userId)
        {
            var uri = new Uri(GetUserIdUri(userId));

            return SetupAuthorizedRequest(uri, HttpMethod.Delete, managementToken);
        }

        #endregion

        #region Helper Methods

        private string GetBaseUsersUri()
        {
            return $"https://{_configProvider.Domain}/api/v2/users";
        }

        private string GetUserIdUri(string user_id)
        {
            return $"{GetBaseUsersUri()}/{user_id}";
        }

        private HttpRequestMessage SetupAuthorizedRequest(Uri uri, HttpMethod method, string managementToken)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = method
            };

            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", managementToken);
            AppendAcceptJsonHeader(req);

            return req;
        }

        private void AppendStringContent<T>(HttpRequestMessage req, T data)
        {
            var stringContent = _serializationService.Serialize(data);
            AppendStringContent(req, stringContent);
        }

        private void AppendStringContent(HttpRequestMessage req, string data)
        {
            req.Content = new StringContent(data, Encoding.UTF8, "application/json");
        }

        private void AppendAcceptJsonHeader(HttpRequestMessage req)
        {
            req.Headers.Add("accept", "application/json");
        }

        #endregion
    }
}
