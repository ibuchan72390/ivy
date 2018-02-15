using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalSale
    {
        #region Constructor

        public PayPalSale()
        {
            payment_hold_reasons = new List<PayPalPaymentHoldReason>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public string purchase_unit_reference_id { get; set; }
        public PayPalAmount amount { get; set; }
        public string payment_mode { get; set; }
        public string state { get; set; }
        public string reason_code { get; set; }
        public string protection_eligibility { get; set; }
        public string protection_eligibility_type { get; set; }
        public string clearing_time { get; set; }
        public string payment_hold_status { get; set; }
        public IList<PayPalPaymentHoldReason> payment_hold_reasons { get; set; }
        public PayPalCurrency transaction_fee { get; set; }
        public PayPalCurrency receivable_amount { get; set; }

        #endregion
    }
}
