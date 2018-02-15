using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ivy.Web.Core.Client;
using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using Ivy.PayPal.Ipn.Core.Interfaces.Models;

namespace Ivy.PayPal.Ipn.Services
{
    public class PayPalIpnValidator : IPayPalIpnValidator
    {
        #region Varialbes & Constants

        private readonly IHttpClientHelper _clientHelper;
        private readonly IPayPalRequestGenerator _requestGenerator;

        private readonly ILogger<IPayPalIpnValidator> _logger;

        #endregion

        #region Constructor

        public PayPalIpnValidator(
            IHttpClientHelper clientHelper,
            IPayPalRequestGenerator requestGenerator,
            ILogger<IPayPalIpnValidator> logger)
        {
            _clientHelper = clientHelper;
            _requestGenerator = requestGenerator;

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

                var request = _requestGenerator.GenerateValidationRequest(bodyStr, useSandbox);

                _logger.LogInformation($"Sending PayPal IPN Post-Back for {model.Txn_Id}");

                var result = await _clientHelper.SendAsync(request);

                return await result.Content.ReadAsStringAsync();
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
