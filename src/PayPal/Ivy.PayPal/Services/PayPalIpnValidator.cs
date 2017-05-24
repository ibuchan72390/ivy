using Ivy.PayPal.Core.Interfaces.Services;
using System;
using System.Threading.Tasks;
using Ivy.PayPal.Core.Interfaces.Models;
using Microsoft.Extensions.Logging;
using Ivy.Web.Core.Client;
using Ivy.PayPal.Core.Providers;

namespace Ivy.PayPal.Services
{
    public class PayPalIpnValidator : IPayPalIpnValidator
    {
        #region Varialbes & Constants

        private readonly IHttpClientHelper _clientHelper;
        private readonly IPayPalRequestGenerator _requestGenerator;

        private readonly IPayPalConfigProvider _provider;
        private readonly ILogger<IPayPalIpnValidator> _logger;

        #endregion

        #region Constructor

        public PayPalIpnValidator(
            IHttpClientHelper clientHelper,
            IPayPalRequestGenerator requestGenerator,
            IPayPalConfigProvider provider,
            ILogger<IPayPalIpnValidator> logger)
        {
            _clientHelper = clientHelper;
            _requestGenerator = requestGenerator;

            _provider = provider;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<string> GetValidationResultAsync(string bodyStr, IPayPalIpnResponse model)
        {
            // This could be used to attack our IPN callback controller
            // Remove before going live in production
            try
            {
                var useSandbox = model.Test_Ipn == 1;

                if (useSandbox != _provider.IsSandbox)
                {
                    int sandBoxInt = _provider.IsSandbox ? 1 : 0;

                    throw new Exception("Receiving a response with an `IsSandbox` declaration that does not match the configuration. " +
                        $"Received: {model.Test_Ipn} / Configuration: {sandBoxInt}");
                }

                var request = _requestGenerator.GenerateValidationRequest(useSandbox);

                _logger.LogInformation($"Sending PayPal IPN Post-Back for {model.Txn_Id}");

                var result = await _clientHelper.SendAsync(request);

                return await result.Content.ReadAsStringAsync();

                //var request = WebRequest.Create(new Uri(paypalUrl)) as HttpWebRequest;
                //request.Method = "POST";

                //byte[] data = Encoding.UTF8.GetBytes(bodyStr);
                //using (var requestStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
                //{
                //    await requestStream.WriteAsync(data, 0, data.Length);
                //}

                //WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
                //var responseStream = responseObject.GetResponseStream();
                //var sr = new StreamReader(responseStream);
                //return await sr.ReadToEndAsync();

            }
            catch (Exception e)
            {
                // May want to add some form of retry logic here in case there is a failure on the PayPal side
                _logger.LogCritical($"Failed to authorize payment! Exception received! Message: {e.Message}");

                throw;
            }
        }

        #endregion
    }
}
