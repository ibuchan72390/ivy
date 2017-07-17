using Ivy.Auth0.Authorization.Core.Providers;
using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Core.Base;
using Ivy.Auth0.Core.Providers;
using Ivy.Web.Core.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Ivy.Auth0.Authorization.Services
{
    public class Auth0AuthorizationRequestGenerator : 
        Auth0BaseRequestGenerator, 
        IAuth0AuthorizationRequestGenerator
    {
        #region Variables & Constants

        private readonly IAuth0AuthorizationConfigurationProvider _authConfig;

        #endregion

        #region Constructor

        public Auth0AuthorizationRequestGenerator(
            IJsonSerializationService serializationService,
            IAuth0GenericConfigurationProvider config,
            IAuth0ApiConfigurationProvider apiConfig,
            IAuth0AuthorizationConfigurationProvider authConfig)
            : base(config, apiConfig, serializationService)
        {
            _authConfig = authConfig;
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateApiTokenRequest()
        {
            return GenerateBaseTokenRequest("urn:auth0-authz-api");
        }

        public HttpRequestMessage GenerateAddUserRolesRequest(string authToken, string authId, IEnumerable<string> roleIds)
        {
            var reqUri = GenerateBaseUserRolesUri(authId);

            var req = SetupAuthorizedRequest(reqUri, PatchMethod, authToken);

            AppendStringContent(req, roleIds);

            return req;
        }

        public HttpRequestMessage GenerateDeleteUserRolesRequest(string authToken, string authId, IEnumerable<string> roleIds)
        {
            var reqUri = GenerateBaseUserRolesUri(authId);

            var req = SetupAuthorizedRequest(reqUri, HttpMethod.Delete, authToken);

            AppendStringContent(req, roleIds);

            return req;
        }

        public HttpRequestMessage GenerateGetRolesRequest(string authToken)
        {
            var reqUri = new Uri($"{_authConfig.AuthorizationUrl}/roles");

            return SetupAuthorizedRequest(reqUri, HttpMethod.Get, authToken);
        }

        public HttpRequestMessage GenerateGetUserRolesRequest(string authToken, string authId)
        {
            var reqUri = GenerateBaseUserRolesUri(authId);

            return SetupAuthorizedRequest(reqUri, HttpMethod.Get, authToken);
        }

        #endregion

        #region Helper Methods

        private Uri GenerateBaseUserRolesUri(string authId)
        {
            return new Uri($"{_authConfig.AuthorizationUrl}/users/{authId}/roles");
        }

        #endregion
    }
}
