using Ivy.ReCaptcha.Core.Interfaces.Providers;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using System;
using System.Net.Http;

namespace Ivy.ReCaptcha.Services
{
    public class ReCaptchaRequestGenerator :
        IReCaptchaRequestGenerator
    {
        #region Variables & Constants

        private readonly IReCaptchaRequestContentGenerator _contentGen;

        private readonly IReCaptchaConfigurationProvider _config;

        #endregion

        #region Constructor

        public ReCaptchaRequestGenerator(
            IReCaptchaRequestContentGenerator contentGen,
            IReCaptchaConfigurationProvider config)
        {
            _contentGen = contentGen;
            _config = config;
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateValidationRequest(string reCaptchaCode, string remoteIp = null)
        {
            var req = new HttpRequestMessage();

            req.Method = HttpMethod.Post;

            req.RequestUri = new Uri(_config.ValidationUrl);

            var postData = _contentGen.GenerateValidationKeyPairs(reCaptchaCode, remoteIp);

            req.Content = new FormUrlEncodedContent(postData);

            return req;
        }

        #endregion
    }
}
