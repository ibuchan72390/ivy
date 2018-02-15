namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalCreditCardToken
    {
        public string credit_card_id { get; set; }
        public string payer_id { get; set; }
        public string last4 { get; set; }
        public string type { get; set; }
        public int expire_month { get; set; }
        public int expire_year { get; set; }
    }
}
