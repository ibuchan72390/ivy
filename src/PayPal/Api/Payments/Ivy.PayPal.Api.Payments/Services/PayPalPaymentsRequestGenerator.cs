using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Payments.Core.Interfaces.Services;

namespace Ivy.PayPal.Api.Payments.Services
{
    public class PayPalPaymentsRequestGenerator : IPayPalPaymentsRequestGenerator
    {
        #region Variables & Constants

        private readonly IPayPalApiTokenGenerator _tokenGenerator;
        private readonly IPayPalUrlGenerator _urlGenerator;

        private const string paymentRefUrl = "v1/payments/payment/";

        #endregion

        #region Constructor

        public PayPalPaymentsRequestGenerator(
            IPayPalApiTokenGenerator tokenGenerator,
            IPayPalUrlGenerator urlGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _urlGenerator = urlGenerator;
        }

        #endregion

        #region Public Methods

        public async Task<HttpRequestMessage> GenerateShowPaymentRequestAsync(string paymentId)
        {
            return await GenerateBaseRequestAsync(HttpMethod.Get, paymentId);
        }

        #endregion

        #region Helper Methods
        
        private async Task<HttpRequestMessage> GenerateBaseRequestAsync(HttpMethod method, string refUrl)
        {
            var url = _urlGenerator.GetPayPalUrl() + paymentRefUrl + refUrl;

            var req = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };

            var token = await _tokenGenerator.GetApiTokenAsync();
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            return req;
        }
        
        #endregion
    }
}
