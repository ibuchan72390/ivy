using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalTransactionItemList
    {
        #region Constructor

        public PayPalTransactionItemList()
        {
            items = new List<PayPalTransactionItem>();
        }

        #endregion

        #region Public Attrs

        public IList<PayPalTransactionItem> items { get; set; }
        public PayPalShippingAddress shipping_address { get; set; }

        #endregion
    }
}
