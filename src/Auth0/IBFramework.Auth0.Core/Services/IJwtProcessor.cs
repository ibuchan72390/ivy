using System.Security.Claims;
using System.Threading.Tasks;

namespace IBFramework.Auth0.Core.Services
{
    public interface IJwtProcessor
    {
        Task<ClaimsPrincipal> DecodeClaimsPrincipalAsync(string jwt);
    }
}
