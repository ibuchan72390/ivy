using System.Collections.Generic;

namespace Ivy.ReCaptcha.Core.Interfaces.Services
{
    public interface IReCaptchaRequestContentGenerator
    {
        IList<KeyValuePair<string, string>> GenerateValidationKeyPairs(
            string reCaptchaCode, string remoteIp = null);
    }
}
