using System.Threading.Tasks;

namespace IBFramework.Auth0.Core.Services
{
    public interface IAuth0ManagementService
    {
        Task ResendVerificationEmailAsync();
    }
}
