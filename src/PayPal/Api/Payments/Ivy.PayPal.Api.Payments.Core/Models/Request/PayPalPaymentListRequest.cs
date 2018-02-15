namespace Ivy.PayPal.Api.Payments.Core.Models.Request
{
    class PayPalPaymentListRequest
    {
        public int? count { get; set; }
        public string start_id { get; set; }
        public int start_index { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string payee_id { get; set; }
        public string sort_by { get; set; }
        public string sort_order { get; set; }
    }
}
