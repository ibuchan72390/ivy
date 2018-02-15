using System.Collections.Generic;

namespace Ivy.PayPal.Api.Payments.Core.Models
{
    public class PayPalAuthorization
    {
        #region Constructor
        
        public PayPalAuthorization()
        {
            links = new List<PayPalLinkDescription>();
        }

        #endregion

        #region Public Attrs

        public string id { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public PayPalAmount amount { get; set; }
        public string parent_payment { get; set; }
        public string valid_until { get; set; }
        public IList<PayPalLinkDescription> links { get; set; }

        #endregion
    }
}
