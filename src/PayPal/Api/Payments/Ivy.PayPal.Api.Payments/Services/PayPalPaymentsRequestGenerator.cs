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

        const string urlBase = "https://api.sandbox.paypal.com/v1/payments/payment/";

        #endregion

        #region Constructor

        public PayPalPaymentsRequestGenerator(
            IPayPalApiTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
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
            var req = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(urlBase + refUrl)
            };

            var token = await _tokenGenerator.GetApiTokenAsync();
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            return req;
        }
        
        #endregion
    }
}
