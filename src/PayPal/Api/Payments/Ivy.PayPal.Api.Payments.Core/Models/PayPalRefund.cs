using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalRefund
    {
        #region Constructor

        public PayPalRefund()
        {
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public PayPalAmount amount { get; set; }
        public string state { get; set; }
        public string reason { get; set; }
        public string invoice_number { get; set; }
        public string sale_id { get; set; }
        public string capture_id { get; set; }
        public string parent_payment { get; set; }
        public string description { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
