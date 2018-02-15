using Ivy.PayPal.Api.Payments.Core.Interfaces.Services;
using Ivy.PayPal.Api.Payments.Core.Models.Response;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.PayPal.Api.Payments.Services
{
    public class PayPalPaymentsService :
        IPayPalPaymentsService
    {
        #region Variables & Constants

        private readonly IPayPalPaymentsRequestGenerator _requestGen;
        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public PayPalPaymentsService(
            IPayPalPaymentsRequestGenerator requestGen,
            IApiHelper apiHelper)
        {
            _requestGen = requestGen;
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task<PayPalPaymentShowResponse> GetPaymentDetailsAsync(string paymentId)
        {
            var req = await _requestGen.GenerateShowPaymentRequestAsync(paymentId);

            return await _apiHelper.GetApiDataAsync<PayPalPaymentShowResponse>(req);
        }

        #endregion
    }
}
