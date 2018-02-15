using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models.Response
{
    public class PayPalPaymentListResponse
    {
        #region Constructor
        
        public PayPalPaymentListResponse()
        {
            payments = new List<PayPalPayment>();
        }
        
        #endregion

        #region Public Attrs

        public IList<PayPalPayment> payments { get; set; }
        public int count { get; set; }
        public string next_id { get; set; }

        #endregion
    }
}
