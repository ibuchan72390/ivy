using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalTransaction
    {
        #region Constructor

        public PayPalTransaction()
        {
            related_resources = new List<PayPalTransactionRelatedResources>();
        }

        #endregion

        #region Public Attrs

        public string reference_id { get; set; }
        public PayPalAmount amount { get; set; }
        public PayPalPayee payee { get; set; }
        public string description { get; set; }
        public string note_to_payee { get; set; }
        public string custom { get; set; }
        public string invoice_number { get; set; }
        public string purchase_order { get; set; }
        public string soft_descriptor { get; set; }
        public string notify_url { get; set; }
        public string order_url { get; set; }
        public PayPalTransactionPaymentOptions payment_options { get; set; }
        public IList<PayPalTransactionRelatedResources> related_resources { get; set; }
        public PayPalTransactionItemList item_list { get; set; }

        #endregion
    }
}
