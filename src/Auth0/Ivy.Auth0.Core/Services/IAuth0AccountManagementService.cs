using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0AccountManagementService
    {
        Task ResendVerificationEmailAsync();
    }
}
