using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Services
{
    public interface IApiAuthTokenGenerator
    {
        Task<string> GetApiAuthTokenAsync();
    }
}
