namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalAmount
    {
        public decimal total { get; set; }
        public string currency { get; set; }
        public PayPalAmountDetail details { get; set; }
    }
}
