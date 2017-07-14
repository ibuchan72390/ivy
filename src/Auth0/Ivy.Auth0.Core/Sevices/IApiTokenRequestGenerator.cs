using System.Net.Http;

namespace Ivy.Auth0.Core.Sevices
{
    public interface IApiTokenRequestGenerator
    {
        HttpRequestMessage GenerateApiTokenRequest();
    }
}
