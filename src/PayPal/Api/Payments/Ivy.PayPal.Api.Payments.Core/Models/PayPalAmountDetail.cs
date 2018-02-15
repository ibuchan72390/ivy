namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalAmountDetail
    {
        public double subtotal { get; set; }
        public double tax { get; set; }
        public double shipping { get; set; }
        public double handling_fee { get; set; }
        public double shipping_discount { get; set; }
        public double insurance { get; set; }
        public double gift_wrap { get; set; }
    }
}
