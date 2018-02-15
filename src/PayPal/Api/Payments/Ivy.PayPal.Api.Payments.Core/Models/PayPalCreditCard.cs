using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalCreditCard
    {
        #region Constructor

        public PayPalCreditCard()
        {
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string number { get; set; }
        public string type { get; set; }
        public int expire_month { get; set; }
        public int expire_year { get; set; }
        public string cvv2 { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public PayPalBillingAddress billing_address { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
