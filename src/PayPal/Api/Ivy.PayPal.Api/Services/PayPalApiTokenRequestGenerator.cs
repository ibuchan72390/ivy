using Ivy.PayPal.Api.Core.Interfaces.Providers;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ivy.PayPal.Api.Services
{
    public class PayPalApiTokenRequestGenerator :
        IPayPalApiTokenRequestGenerator
    {
        #region Variables & Constants

        const string tokenApiRefUrl = "v1/oauth2/token";

        private readonly IPayPalApiConfigProvider _configProvider;

        private readonly IPayPalUrlGenerator _urlGenerator;

        #endregion

        #region Constructor

        public PayPalApiTokenRequestGenerator(
            IPayPalApiConfigProvider configProvider,
            IPayPalUrlGenerator urlGenerator)
        {
            _configProvider = configProvider;
            _urlGenerator = urlGenerator;
        }

        #endregion

        #region Public Methods

        // Process to generate the proper API Token request
        // https://developer.paypal.com/docs/api/overview/#get-an-access-token
        public HttpRequestMessage GenerateApiTokenRequest()
        {
            var tokenApi = _urlGenerator.GetPayPalUrl() + tokenApiRefUrl;

            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(tokenApi),
            };

            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            req.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

            var authHeaderBytes = ASCIIEncoding.ASCII.GetBytes($"{_configProvider.ClientId}:{_configProvider.ClientSecret}");
            string authHeaderValue = Convert.ToBase64String(authHeaderBytes);
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            var postDict = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };
            req.Content = new FormUrlEncodedContent(postDict);

            return req;
        }

        #endregion
    }
}
