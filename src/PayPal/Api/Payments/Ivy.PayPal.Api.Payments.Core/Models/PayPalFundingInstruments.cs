namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalFundingInstruments
    {
        public PayPalCreditCard credit_card { get; set; }
        public PayPalCreditCardToken credit_card_token { get; set; }
    }
}
