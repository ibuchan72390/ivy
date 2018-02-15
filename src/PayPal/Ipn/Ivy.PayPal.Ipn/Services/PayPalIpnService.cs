using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using Ivy.PayPal.Ipn.Core.Interfaces.Transformer;
using Ivy.PayPal.Ipn.Core.Interfaces.Models;

namespace Ivy.PayPal.Ipn.Services
{
    public class PayPalIpnService : IPayPalIpnService
    {
        #region Variables & Constants

        private readonly IPayPalIpnResponseTransformer _transformer;
        private readonly IPayPalIpnValidator _validator;

        private readonly ILogger<IPayPalIpnService> _logger;

        #endregion

        #region Constructor

        public PayPalIpnService(
            IPayPalIpnResponseTransformer transformer,
            IPayPalIpnValidator validator,
            ILogger<IPayPalIpnService> logger)
        {
            _transformer = transformer;
            _validator = validator;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<bool> VerifyIpnAsync(HttpRequest request, IPayPalIpnResponse model)
        {
            var verificationResponse = string.Empty;

            var bodyStr = _transformer.Transform(request);


            /*
             * Get the verification response
             */
            verificationResponse = await _validator.GetValidationResultAsync(bodyStr, model);

            _logger.LogInformation($"Received verification response: {verificationResponse}");

            if (verificationResponse.Equals("VERIFIED"))
            {
                return true;
            }
            else if (verificationResponse.Equals("INVALID"))
            {
                return false;
            }
            else
            {
                throw LogAndThrowVerificationException("PayPalControler.ProcessVerificationResponse " +
                    $"received an unexpected response type from PayPal. Received: {verificationResponse}");
            }
            
        }

        #endregion

        #region Helper Methods

        private Exception LogAndThrowVerificationException(string message, Exception innerException = null)
        {
            /*
             * These errors are getting logged at the critical level
             * If there's an issue with PayPal, I need to know ASAP
             * this is mission critical piece of the application
             * customers WILL complain if they give us money and don't get a class
             */
            _logger.LogCritical(message);

            if (innerException != null)
            {
                return new Exception(message);
            }
            else
            {
                return new Exception(message, innerException);
            }
        }

        #endregion
    }
}
