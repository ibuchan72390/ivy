using System.Threading.Tasks;

/*
 * We may need to wrap this in a manager service that is capable of
 * monitoring and refreshing each token accordingly.  It seems that 
 * we should only be requesting a new one on token refresh.
 */
namespace Ivy.Google.Core.Interfaces.Services
{
    public interface IGoogleAccessTokenGenerator
    {
        Task<string> GetOAuthTokenAsync(string[] scopes);
    }
}
