using Ivy.Auth0.Core.Base;
using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Web.Core.Json;
using System;
using System.Net.Http;

namespace Ivy.Auth0.Management.Services
{
    /*
     * For anybody who is curious, I'm specifically not placing the ApiTokenLoad in here
     * because I don't want to have the token load being used on the same class as the
     * token request generation.  This simplifies the testing process for mocking and 
     * allows for a better separation of concerns internally.
     */

    public class Auth0ManagementRequestGenerator : 
        Auth0BaseRequestGenerator, 
        IAuth0ManagementRequestGenerator
    {
        #region Variables & Constants

        private readonly IAuth0QueryStringUriGenerator _queryStringGenerator;

        private readonly IAuth0JsonGenerator _jsonGenerator;

        private readonly IAuth0ClientConfigurationProvider _clientConfig;

        #endregion

        #region Constructor

        public Auth0ManagementRequestGenerator(
            IAuth0QueryStringUriGenerator queryStringGenerator,
            IJsonSerializationService serializationService,
            IAuth0GenericConfigurationProvider config,
            IAuth0ApiConfigurationProvider apiConfig,
            IAuth0ClientConfigurationProvider clientConfig,
            IAuth0JsonGenerator jsonGenerator)
            : base(config, apiConfig, serializationService)
        {
            _queryStringGenerator = queryStringGenerator;
            _clientConfig = clientConfig;

            _jsonGenerator = jsonGenerator;
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateApiTokenRequest()
        {
            return GenerateBaseTokenRequest($"https://{_config.Domain}/api/v2/");
        }

        public HttpRequestMessage GenerateVerifyEmailRequest(string managementToken, string userId)
        {
            var verifyEmail = new Auth0VerifyEmailModel
            {
                user_id = userId,
                client_id = _clientConfig.SpaClientId
            };

            var uri = new Uri($"https://{_config.Domain}/api/v2/jobs/verification-email");

            var req = SetupAuthorizedRequest(uri, HttpMethod.Post, managementToken);

            AppendStringContent(req, verifyEmail);

            return req;
        }

        public HttpRequestMessage GenerateListUsersRequest(string managementToken, Auth0ListUsersRequest request)
        {
            var baseUriString = GetBaseUsersUri();

            // Need to set this guy to the connection we use for the application
            // Screw what the user puts, we'll override it regardless to what is correct
            request.Connection = _config.Connection;

            var reqUri = _queryStringGenerator.GenerateGetUsersQueryString(baseUriString, request);

            return SetupAuthorizedRequest(reqUri, HttpMethod.Get, managementToken);
        }

        public HttpRequestMessage GenerateCreateUserRequest(string managementToken, Auth0CreateUserRequest request)
        {
            var uri = new Uri(GetBaseUsersUri());

            // Need to set this guy to the connection we use for the application
            // Screw what the user puts, we'll override it regardless to what is correct
            request.connection = _config.Connection;

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
            request.connection = _config.Connection;
            request.client_id = _clientConfig.SpaClientId;

            var req = SetupAuthorizedRequest(uri, PatchMethod, managementToken);

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

        protected string GetBaseUsersUri()
        {
            return $"https://{_config.Domain}/api/v2/users";
        }

        protected string GetUserIdUri(string user_id)
        {
            return $"{GetBaseUsersUri()}/{user_id}";
        }

        #endregion
    }
}
