namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalShowPaymentResult
    {
        public string id { get; set; }
        public string intent { get; set; }
        public string create_time { get; set; }
        public PayPalPayer payer { get; set; }
    }
}
