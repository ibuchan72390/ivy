namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalPayerInfo
    {
        public string email { get; set; }
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string suffix { get; set; }
        public string payer_id { get; set; }
        public string phone { get; set; }
        public string phone_type { get; set; }
        public string birth_date { get; set; }
        public string tax_id { get; set; }
        public string tax_id_type { get; set; }
        public string country_code { get; set; }
        public PayPalBillingAddress billing_address { get; set; }
        public PayPalShippingAddress shipping_address { get; set; }
    }
}
