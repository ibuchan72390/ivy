using System.Threading.Tasks;

namespace IBFramework.Auth0.Core.Services
{
    public interface IApiAuthTokenGenerator
    {
        Task<string> GetApiAuthTokenAsync();
    }
}
