namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalAmountDetail
    {
        public decimal subtotal { get; set; }
        public decimal tax { get; set; }
        public decimal shipping { get; set; }
        public decimal handling_fee { get; set; }
        public decimal shipping_discount { get; set; }
        public decimal insurance { get; set; }
        public decimal gift_wrap { get; set; }
    }
}
