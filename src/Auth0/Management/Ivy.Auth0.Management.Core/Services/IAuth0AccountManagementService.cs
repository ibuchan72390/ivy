using System.Threading.Tasks;

namespace Ivy.Auth0.Management.Core.Services
{
    public interface IAuth0AccountManagementService
    {
        Task ResendVerificationEmailAsync(string userId);
    }
}
