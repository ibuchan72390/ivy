using System.Net.Http;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0ManagementRequestGenerator
    {
        HttpRequestMessage GenerateManagementApiTokenRequest();

        HttpRequestMessage GenerateVerifyEmailRequest(string managementToken);
    }
}
