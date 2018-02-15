namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalPayee
    {
        public string email { get; set; }
        public string merchant_id { get; set; }
        public PayPalPayeeMetadata payee_display_metadata { get; set; }
    }
}
