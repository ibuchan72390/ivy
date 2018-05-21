using System.Net.Http;

namespace Ivy.ReCaptcha.Core.Interfaces.Services
{
    public interface IReCaptchaRequestGenerator
    {
        HttpRequestMessage GenerateValidationRequest(string reCaptchaCode, string remoteIp = null);
    }
}
