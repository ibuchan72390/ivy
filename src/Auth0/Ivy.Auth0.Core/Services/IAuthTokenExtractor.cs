using Microsoft.AspNetCore.Http;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuthTokenExtractor
    {
        string ExtractAuthToken(HttpRequest request);
    }
}
