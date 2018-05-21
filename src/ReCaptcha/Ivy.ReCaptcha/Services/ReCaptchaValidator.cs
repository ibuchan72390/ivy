using Ivy.ReCaptcha.Core.Interfaces.Models;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.ReCaptcha.Services
{
    public class ReCaptchaValidator : IReCaptchaValidator
    {
        #region Variables & Constants

        private readonly IReCaptchaRequestGenerator _requestGen;

        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public ReCaptchaValidator(
            IReCaptchaRequestGenerator requestGen,
            IApiHelper apiHelper)
        {
            _requestGen = requestGen;
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task<TResponse> ValidateAsync<TResponse>(string reCaptchaCode, string remoteIp = null)
            where TResponse : IReCaptchaResponse
        {
            var req = _requestGen.GenerateValidationRequest(reCaptchaCode, remoteIp);

            return await _apiHelper.GetApiDataAsync<TResponse>(req);
        }

        #endregion
    }
}
