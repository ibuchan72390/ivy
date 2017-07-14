using Microsoft.AspNetCore.Http;

namespace Ivy.Auth0.Web.Core.Services
{
    public interface IAuthTokenExtractor
    {
        string ExtractAuthToken(HttpRequest request);
    }
}
