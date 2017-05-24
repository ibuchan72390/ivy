using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0ContextProcessor
    {
        Task ProcessContextAsync(HttpContext context);
    }
}
