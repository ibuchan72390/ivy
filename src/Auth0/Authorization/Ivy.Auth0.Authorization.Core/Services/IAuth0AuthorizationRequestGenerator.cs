using System.Net.Http;

namespace Ivy.Auth0.Authorization.Core.Services
{
    public interface IAuth0AuthorizationRequestGenerator
    {
        HttpRequestMessage GenerateAuthorizationApiTokenRequest();

        HttpRequestMessage GenerateGetRolesRequest();

        HttpRequestMessage GenerateGetUserRolesRequest(string authId);

        HttpRequestMessage GenerateAddUserRoleRequest(string authId, string roleId);

        HttpRequestMessage GenerateDeleteUserRoleRequest(string authId, string roleId);
    }
}
