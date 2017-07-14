using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Sevices
{
    public interface IApiTokenGenerator
    {
        Task<string> GetApiTokenAsync();
    }
}
