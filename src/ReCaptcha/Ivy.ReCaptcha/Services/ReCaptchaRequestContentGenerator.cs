using System.Collections.Generic;
using Ivy.ReCaptcha.Core.Interfaces.Providers;
using Ivy.ReCaptcha.Core.Interfaces.Services;

namespace Ivy.ReCaptcha.Services
{
    public class ReCaptchaRequestContentGenerator :
        IReCaptchaRequestContentGenerator
    {
        #region Variables & Constants

        private readonly IReCaptchaConfigurationProvider _config;

        #endregion

        #region Constructor

        public ReCaptchaRequestContentGenerator(
            IReCaptchaConfigurationProvider config)
        {
            _config = config;
        }

        #endregion

        #region Public Methods

        public IList<KeyValuePair<string, string>> GenerateValidationKeyPairs(
            string reCaptchaCode, string remoteIp = null)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", _config.Secret),
                new KeyValuePair<string, string>("response", reCaptchaCode),
            };

            if (remoteIp != null)
            {
                postData.Add(new KeyValuePair<string, string>("remoteip", remoteIp));
            }

            return postData;
        }

        #endregion
    }
}
