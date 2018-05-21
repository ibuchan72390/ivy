using Ivy.ReCaptcha.Core.Interfaces.Models;
using System.Threading.Tasks;

namespace Ivy.ReCaptcha.Core.Interfaces.Services
{
    public interface IReCaptchaValidator
    {
        Task<TResponse> ValidateAsync<TResponse>(string reCaptchaCode, string remoteIp = null)
            where TResponse : IReCaptchaResponse;
    }
}
