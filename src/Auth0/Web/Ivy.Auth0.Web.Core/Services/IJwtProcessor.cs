using System.Security.Claims;
using System.Threading.Tasks;

namespace Ivy.Auth0.Web.Core.Services
{
    public interface IJwtProcessor
    {
        Task<ClaimsPrincipal> DecodeClaimsPrincipalAsync(string jwt);
    }
}
