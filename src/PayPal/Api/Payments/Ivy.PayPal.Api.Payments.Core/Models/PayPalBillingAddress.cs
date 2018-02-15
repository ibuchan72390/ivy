namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalBillingAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
        public string normalization_status { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }
}
