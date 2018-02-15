namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalTransactionRelatedResources
    {
        public PayPalSale sale { get; set; }
        public PayPalAuthorization authorization { get; set; }
        public PayPalOrder order { get; set; }
        public PayPalCapture capture { get; set; }
        public PayPalRefund refund { get; set; }
    }
}
