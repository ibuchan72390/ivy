using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalCapture
    {
        #region Constructor

        public PayPalCapture()
        {
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public PayPalAmount amount { get; set; }
        public bool is_final_capture { get; set; }
        public string state { get; set; }
        public string reason_code { get; set; }
        public string parent_payment { get; set; }
        public string invoice_number { get; set; }
        public PayPalCurrency transaction_fee { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
