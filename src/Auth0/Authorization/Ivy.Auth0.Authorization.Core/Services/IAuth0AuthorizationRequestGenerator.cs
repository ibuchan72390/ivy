using Ivy.Auth0.Core.Sevices;
using System.Collections.Generic;
using System.Net.Http;

namespace Ivy.Auth0.Authorization.Core.Services
{
    public interface IAuth0AuthorizationRequestGenerator : IApiTokenRequestGenerator
    {
        HttpRequestMessage GenerateGetRolesRequest(string authToken);

        HttpRequestMessage GenerateGetUserRolesRequest(string authToken, string authId);

        HttpRequestMessage GenerateAddUserRolesRequest(string authToken, string authId, IEnumerable<string> roleId);

        HttpRequestMessage GenerateDeleteUserRolesRequest(string authToken, string authId, IEnumerable<string> roleId);
    }
}
