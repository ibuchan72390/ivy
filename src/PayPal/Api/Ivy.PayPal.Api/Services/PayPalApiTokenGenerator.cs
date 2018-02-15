using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Core.Models;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.PayPal.Api.Services
{
    public class PayPalApiTokenGenerator : IPayPalApiTokenGenerator
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;

        private readonly IPayPalApiTokenRequestGenerator _reqGen;

        #endregion

        #region Constructor

        public PayPalApiTokenGenerator(
            IApiHelper apiHelper,
            IPayPalApiTokenRequestGenerator reqGen)
        {
            _apiHelper = apiHelper;

            _reqGen = reqGen;
        }

        #endregion

        #region Public Methods

        /*
         * We may need to implement a token cache here of some form...
         * https://developer.paypal.com/docs/rate-limiting/
         */
        public async Task<PayPalTokenResponse> GetApiTokenAsync()
        {
            var req = _reqGen.GenerateApiTokenRequest();

            return await _apiHelper.GetApiDataAsync<PayPalTokenResponse>(req);
        }

        #endregion
    }
}
