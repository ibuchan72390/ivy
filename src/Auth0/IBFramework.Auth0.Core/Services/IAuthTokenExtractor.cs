using Microsoft.AspNetCore.Http;

namespace IBFramework.Auth0.Core.Services
{
    public interface IAuthTokenExtractor
    {
        string ExtractAuthToken(HttpRequest request);
    }
}
