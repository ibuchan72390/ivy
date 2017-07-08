using Ivy.Auth0.Core.Models.Requests;
using System.Net.Http;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0ManagementRequestGenerator
    {
        HttpRequestMessage GenerateManagementApiTokenRequest();

        HttpRequestMessage GenerateVerifyEmailRequest(string managementToken);

        HttpRequestMessage GenerateListUsersRequest(string managementToken, Auth0ListUsersRequest request);

        HttpRequestMessage GenerateCreateUserRequest(string managementToken, Auth0CreateUserRequest request);

        HttpRequestMessage GenerateGetUserRequest(string managementToken, string userId);

        HttpRequestMessage GenerateUpdateUserRequest(string managementToken, Auth0UpdateUserRequest request);

        HttpRequestMessage GenerateDeleteUserRequest(string managementToken, string userId);
    }
}
