namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalTransactionItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal tax { get; set; }
        public string sku { get; set; }
        public string currency { get; set; }
    }
}
