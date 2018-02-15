using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models.Response
{
    public class PayPalPaymentCreateResponse
    {
        #region Constructor

        public PayPalPaymentCreateResponse()
        {
            transactions = new List<PayPalTransaction>();
        }

        #endregion

        #region Public Attributes

        public string intent { get; set; }
        public PayPalPayer payer { get; set; }
        public IList<PayPalTransaction> transactions { get; set; }
        public string note_to_payer { get; set; }
        public PayPalRedirectUrls redirect_urls { get; set; }

        #endregion
    }
}
