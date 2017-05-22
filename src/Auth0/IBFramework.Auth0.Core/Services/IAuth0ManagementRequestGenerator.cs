using System.Net.Http;

namespace IBFramework.Auth0.Core.Services
{
    public interface IAuth0ManagementRequestGenerator
    {
        HttpRequestMessage GenerateManagementApiTokenRequest();

        HttpRequestMessage GenerateVerifyEmailRequest(string managementToken);
    }
}
