using Ivy.PayPal.Api.Core.Interfaces.Providers;
using Ivy.PayPal.Api.Core.Interfaces.Services;

namespace Ivy.PayPal.Api.Services
{
    public class PayPalUrlGenerator :
        IPayPalUrlGenerator
    {
        #region Variables & Constants

        private readonly IPayPalApiConfigProvider _config;

        #endregion

        #region Constructor

        public PayPalUrlGenerator(
            IPayPalApiConfigProvider config)
        {
            _config = config;
        }

        #endregion

        #region Public Methods

        public string GetPayPalUrl()
        {
            if (_config.SandboxMode)
            {
                return "https://api.sandbox.paypal.com/";
            }
            else
            {
                return "https://api.paypal.com/";
            }
        }

        #endregion
    }
}
