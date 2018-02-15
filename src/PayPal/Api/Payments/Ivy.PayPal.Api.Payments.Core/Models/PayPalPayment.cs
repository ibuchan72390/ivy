using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalPayment
    {
        #region Constructor

        public PayPalPayment()
        {
            transactions = new List<PayPalTransaction>();
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public string intent { get; set; }
        public PayPalPayer payer { get; set; }
        public IList<PayPalTransaction> transactions { get; set; }
        public string state { get; set; }
        public string experience_profile_id { get; set; }
        public string note_to_payer { get; set; }
        public PayPalRedirectUrls redirect_urls { get; set; }
        public string failure_reason { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
