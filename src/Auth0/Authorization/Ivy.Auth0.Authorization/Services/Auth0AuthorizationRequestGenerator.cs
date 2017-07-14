using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Core.Base;
using Ivy.Auth0.Core.Providers;
using Ivy.Web.Core.Json;
using System;
using System.Net.Http;

namespace Ivy.Auth0.Authorization.Services
{
    public class Auth0AuthorizationRequestGenerator : Auth0BaseRequestGenerator, IAuth0AuthorizationRequestGenerator
    {
        #region Constructor

        public Auth0AuthorizationRequestGenerator(
            IJsonSerializationService serializationService,
            IAuth0ApiConfigurationProvider apiConfig,
            IAuth0GenericConfigurationProvider config)
            : base(config, apiConfig, serializationService)
        {
        }

        #endregion

        public HttpRequestMessage GenerateAddUserRoleRequest(string authId, string roleId)
        {
            return GenerateBaseTokenRequest("urn:auth0-authz-api");
        }

        public HttpRequestMessage GenerateAuthorizationApiTokenRequest()
        {
            throw new NotImplementedException();
        }

        public HttpRequestMessage GenerateDeleteUserRoleRequest(string authId, string roleId)
        {
            throw new NotImplementedException();
        }

        public HttpRequestMessage GenerateGetRolesRequest()
        {
            throw new NotImplementedException();
        }

        public HttpRequestMessage GenerateGetUserRolesRequest(string authId)
        {
            throw new NotImplementedException();
        }
    }
}
