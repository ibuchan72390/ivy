using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalOrder
    {
        #region Constructor

        public PayPalOrder()
        {
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public string reference_id { get; set; }
        public PayPalAmount amount { get; set; }
        public string payment_mode { get; set; }
        public string state { get; set; }
        public string reason_code { get; set; }
        public string pending_reason { get; set; }
        public string protection_eligibility { get; set; }
        public string protection_eligibility_type { get; set; }
        public string parent_payment { get; set; }
        public PayPalFmfDetails fmf_details { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
