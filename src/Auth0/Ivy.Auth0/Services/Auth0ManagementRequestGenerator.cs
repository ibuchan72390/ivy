using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Json;
using Ivy.Web.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

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

        private readonly IJsonManipulationService _jsonManipulator;

        #endregion

        #region Constructor

        public Auth0ManagementRequestGenerator(
            IAuth0QueryStringUriGenerator queryStringGenerator,
            IJsonSerializationService serializationService,
            IAuth0ConfigurationProvider configProvider,
            IUserProvider userProvider,
            IJsonManipulationService jsonManipulator)
        {
            _queryStringGenerator = queryStringGenerator;
            _serializationService = serializationService;
            _configProvider = configProvider;
            _userProvider = userProvider;

            _jsonManipulator = jsonManipulator;
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

            //AppendStringContent(req, request);
            var json = _serializationService.Serialize(request);

            // If Phone is null or empty, we need to simply zero it out of the JSON
            if (request.phone_number == null || request.phone_number == "")
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "phone_number");
            }
            else
            {
                // If we save a phone number, it 100% MUST match this regex: ^\\+[0-9]{1,15}$
                // If this regex does not match, then we will definitely fail
                Match validPhone = Regex.Match(request.phone_number, "^\\+[0-9]{1,15}$");

                if (!validPhone.Success)
                {
                    throw new Exception("Invalid phone number received! Must match regex - ^\\+[0-9]{1,15}$" + 
                        $" / Phone: {request.phone_number}");
                }
            }

            // If Username functionality isn't enabled, it must be removed from the JSON
            if (!_configProvider.UseUsername)
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "username");
            }

            req.Content = new StringContent(json, Encoding.UTF8, "application/json");

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

            AppendStringContent(req, request);

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
            req.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");
        }

        private void AppendAcceptJsonHeader(HttpRequestMessage req)
        {
            req.Headers.Add("accept", "application/json");
        }

        #endregion
    }
}
