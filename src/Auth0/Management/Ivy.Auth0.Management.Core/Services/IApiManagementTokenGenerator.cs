using System.Threading.Tasks;

namespace Ivy.Auth0.Management.Core.Services
{
    public interface IApiManagementTokenGenerator
    {
        Task<string> GetApiAuthTokenAsync();
    }
}
