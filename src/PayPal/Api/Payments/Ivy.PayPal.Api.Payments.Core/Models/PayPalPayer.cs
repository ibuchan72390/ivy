using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalPayer
    {
        public string payment_method { get; set; }
        public string status { get; set; }
        public IList<PayPalFundingInstruments> funding_instruments { get; set; }
        public PayPalPayerInfo payer_info { get; set; }
    }
}
